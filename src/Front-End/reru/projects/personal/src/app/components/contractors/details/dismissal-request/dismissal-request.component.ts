import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { DismissalRequestByHrService } from 'projects/personal/src/app/utils/services/dismissal-request-by-hr.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ActivatedRoute } from '@angular/router';
import { RequestProfileModel } from 'projects/personal/src/app/utils/models/request-profile.model';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { AddRequestByHrModalComponent } from 'projects/personal/src/app/utils/modals/add-request-by-hr-modal/add-request-by-hr-modal.component';
import { InitializerUserProfileService } from 'projects/personal/src/app/utils/services/initializer-user-profile.service';

@Component({
  selector: 'app-dismissal-request',
  templateUrl: './dismissal-request.component.html',
  styleUrls: ['./dismissal-request.component.scss']
})
export class DismissalRequestComponent implements OnInit {

  @Input() contractor: Contractor;
  hasContractorId: number;
  contractorId: number;
  dismissal: RequestProfileModel[] = [];
  requests: any[] = [];

  pagedSummary: PagedSummary = new PagedSummary();

  isLoading: boolean = false;

  constructor(private modalService: NgbModal,
              private notificationService: NotificationsService,
              private dismissalRequestByHrService: DismissalRequestByHrService,
              private fileService: FileService,
              private initializerUserProfileService: InitializerUserProfileService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.list();
    this.subscribeForParams();
    this.getUserContractorId();
  }

  id: number;

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
      if (response.id) {
        this.contractorId = response.id;
        this.list({contractorId: response.id});
        return;
      }
    });
  }

  getUserContractorId(){
    this.initializerUserProfileService.get().subscribe(res => {
      if(res.data) {
        this.hasContractorId = res.data.contractorId;
      }
    });
  }

  openAddRequestModal(): void {
    const modalRef = this.modalService.open(AddRequestByHrModalComponent, { centered: true, backdrop: 'static', size: 'md'});
    console.log('sending you contractor', this.contractor);
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.componentInstance.contractorId = this.contractorId;
    modalRef.result.then((response: RequestProfileModel) => this.addVacation(response), () => {});
  }

  addVacation(data: RequestProfileModel): void {
    this.isLoading = true;
    this.dismissalRequestByHrService.create(data).subscribe(response => {
      this.list();
      this.notificationService.success('Success', 'Request added!', NotificationUtil.getDefaultConfig());
    }, () => {}, () => {
      this.isLoading = false;
    });
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      contractorId: this.contractorId,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.dismissalRequestByHrService.get(request).subscribe((response: any) => {
      this.requests = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {}, () => {this.isLoading = false});
  }

  download(id: number): void {
    this.fileService.download(id);
  }
}
