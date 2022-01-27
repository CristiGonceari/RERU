import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddRequestModalComponent } from '../../../utils/modals/add-request-modal/add-request-modal.component';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { FileService } from '../../../utils/services/file.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { RequestsProfileService } from '../../../utils/services/requests-profile.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Contractor } from '../../../utils/models/contractor.model';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss']
})
export class MyRequestsComponent implements OnInit {

  @Input() contractor: Contractor;
  requests: any[] = [];

  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }

  isLoading: boolean = false;

  constructor(private modalService: NgbModal,
              private notificationService: NotificationsService,
              private requestsProfileService: RequestsProfileService,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.list();
  }

  openAddRequestModal(): void {
    const modalRef = this.modalService.open(AddRequestModalComponent, { centered: true, backdrop: 'static', size: 'md'});
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then((response) => this.addVacation(response), () => {});
  }

  addVacation(data: any): void {
    this.isLoading = true;
    this.requestsProfileService.create(data.fromDate).subscribe(response => {
      this.list();
      this.notificationService.success('Success', 'Request added!', NotificationUtil.getDefaultConfig());
    }, () => {}, () => {
      this.isLoading = false;
    });
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      pageSize: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.requestsProfileService.get(request).subscribe((response: any) => {
      this.requests = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {}, () => {this.isLoading = false});
  }

  download(id: number): void {
    this.fileService.download(id);
  }
}
