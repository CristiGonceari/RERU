import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { OrganigramService } from '../../../utils/services/organigram.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DeleteOrganigramModalComponent } from '../../../utils/modals/delete-organigram-modal/delete-organigram-modal.component';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-organigram-table',
  templateUrl: './organigram-table.component.html',
  styleUrls: ['./organigram-table.component.scss']
})
export class OrganigramTableComponent implements OnInit {
  isLoading: boolean = true;
  organigrams: any[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private organigramService: OrganigramService,
              private notificationService: NotificationsService,
              private modalService: NgbModal) { }

  ngOnInit(): void {
    this.list();
  }

  list(data :any = {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize
    })
    this.organigramService.list(request).subscribe(response => {
      if (response.success) {
        this.organigrams = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openOrganigramDeleteModal(id: number) {
    const modalRef = this.modalService.open(DeleteOrganigramModalComponent);
    modalRef.result.then(() => this.deleteOrganigram(id), () => {});
  }

  deleteOrganigram(id: number): void {
    this.organigramService.delete(id).subscribe(() => {
      this.list();
      this.notificationService.success('Success', 'Organigram deleted!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error deleting the organigram!', NotificationUtil.getDefaultConfig());
    })
  }
}
