import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { OrganigramService } from '../../../utils/services/organigram.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DeleteOrganigramModalComponent } from '../../../utils/modals/delete-organigram-modal/delete-organigram-modal.component';
import { ObjectUtil } from '../../../utils/util/object.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';


@Component({
  selector: 'app-organigram-table',
  templateUrl: './organigram-table.component.html',
  styleUrls: ['./organigram-table.component.scss']
})
export class OrganigramTableComponent implements OnInit {
  isLoading: boolean = true;
  organigrams: any[] = [];
  pagedSummary: PagedSummary = new PagedSummary();

  notification = {
    success: 'Success',
    error: 'Error',
    successDelete: 'Organigram was deleted',
    errorDelete: 'Organigram was not deleted',
  };

  constructor(private organigramService: OrganigramService,
              private notificationService: NotificationsService,
              public translate: I18nService,
              private modalService: NgbModal) { }

  ngOnInit(): void {
    this.list();
    this.translateData();

    this.subscribeForTranslateChanges();
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

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('organigram.succes-delete-organigram'),
      this.translate.get('organigram.error-delete-organigram'),

    ]).subscribe(([success, error, successDelete, errorDelete]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.successDelete = successDelete;
      this.notification.errorDelete = errorDelete;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }
  
  openOrganigramDeleteModal(id: number) {
    const modalRef = this.modalService.open(DeleteOrganigramModalComponent);
    modalRef.result.then(() => this.deleteOrganigram(id), () => {});
  }

  deleteOrganigram(id: number): void {
    this.organigramService.delete(id).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.success, this.notification.successDelete, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.notificationService.error(this.notification.error, this.notification.errorDelete, NotificationUtil.getDefaultMidConfig());
    })
  }
}
