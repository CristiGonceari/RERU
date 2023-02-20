import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ChangeCurrentPositionModalComponent } from '../../../utils/modals/change-current-position-modal/change-current-position-modal.component';
import { ChangeNameModalComponent } from '../../../utils/modals/change-name-modal/change-name-modal.component';
import { Contractor } from '../../../utils/models/contractor.model';
import { ContractorService } from '../../../utils/services/contractor.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PositionService } from '../../../utils/services/position.service';
import { AddContractorContactModalComponent } from '../../../utils/modals/add-contractor-contact-modal/add-contractor-contact-modal.component';
import { ConfirmationDismissModalComponent } from '../../../utils/modals/confirmation-dismiss-modal/confirmation-dismiss-modal.component';
import { ConfirmationDeleteContractorComponent } from '../../../utils/modals/confirmation-delete-contractor/confirmation-delete-contractor.component';
import { ObjectUtil } from '../../../utils/util/object.util';
import { TransferNewPositionModalComponent } from '../../../utils/modals/transfer-new-position-modal/transfer-new-position-modal.component';
import { AddOldPositionModalComponent } from '../../../utils/modals/add-old-position-modal/add-old-position-modal.component';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { UserStatus } from '../../../utils/models/user-status.enum';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
@Component({
  selector: 'app-contractors-table',
  templateUrl: './contractors-table.component.html',
  styleUrls: ['./contractors-table.component.scss']
})
export class ContractorsTableComponent implements OnInit {
  contractors: Contractor[];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };

  isLoading: boolean = true;
  employerStates: number;
  keyword: string;
  filters: any = {};
  userStatus: number;
  notification = {
    contractor: 'Contractor',
    warning: 'Warning',
    error: 'Error',
    success: 'Success',
    contractorUpdate: 'Contractor updated!',
    contractorDelete: 'Contractor deleted!',
    contractorDismiss: 'Contractor dismissed!',
    validationFail: 'Validation failed!',
    serverError: 'Server error occured!'
  }

  constructor(private contractorService: ContractorService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    private positionService: PositionService,
    public translate: I18nService) { }

  ngOnInit(): void {
    this.list();
    
    this.translateData();
    this.subscribeForLanguageChange();
  }

  list(data: any = {}): void {
    if (data.hasOwnProperty('keyword')) this.keyword = data.keyword;
    data = ObjectUtil.preParseObject({
      ...data,
      page: data.page || this.pagedSummary.currentPage,
      userStatus: data.userStatus || this.userStatus || UserStatus.All,
      employerStates: data.employerStates || this.employerStates || 0,
      keyword: data.keyword || this.keyword,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
      ...this.filters
    });
    this.isLoading = true;
    this.contractorService.list(data).subscribe(response => {
      if (response.success) {
        this.contractors = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  setKeyword(value: string): void {
    this.keyword = value;
  }

  openChangeNameModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(ChangeNameModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then((contractor: Contractor) => this.updateContractorName(contractor), () => { });
  }

  translateData(): void {
		forkJoin([
			this.translate.get('notification.title.contractor'),
      this.translate.get('notification.title.warning'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.title.success'),
			this.translate.get('notification.body.success.contractor-update'),
      this.translate.get('notification.body.success.contractor-delete'),
      this.translate.get('notification.body.success.contractor-dismiss'),
      this.translate.get('notification.body.success.validation-fail'),
      this.translate.get('notification.body.success.server-error'),
		]).subscribe(
			([ contractor, warning, error, success, contractorUpdate, contractorDelete,
          contractorDismiss, validationFail, serverError]) => {
				this.notification.contractor = contractor;
        this.notification.warning = warning;
        this.notification.error = error;
        this.notification.success = success;
				this.notification.contractorUpdate = contractorUpdate;
				this.notification.contractorDelete = contractorDelete;
        this.notification.contractorDismiss = contractorDismiss;
        this.notification.validationFail = validationFail;
        this.notification.serverError = serverError;
			}
		);
	}

	subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  updateContractorName(contractor: Contractor): void {
    this.isLoading = true;
    this.contractorService.updateName(contractor).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.contractor, this.notification.contractorUpdate, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list();
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  openConfirmationDeleteModal(contractor: Contractor): void {
    const modalRef: any = this.modalService.open(ConfirmationDeleteContractorComponent, { centered: true });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then(() => this.delete(contractor.id), () => { });
  }

  delete(id: number): void {
    this.isLoading = true;
    this.contractorService.delete(id).subscribe(response => {
      this.list();
      this.notificationService.success(this.notification.success, this.notification.contractorDelete, NotificationUtil.getDefaultMidConfig());
    });
  }

  openChangeCurrentPositionModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(ChangeCurrentPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then((contractor: Contractor) => this.changeCurrentPosition(contractor), () => { });
  }

  changeCurrentPosition(contractor): void {
    this.isLoading = true;
    this.positionService.changeCurrentPosition(contractor).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.contractor, this.notification.contractorUpdate, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list();
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  openAddContactModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(AddContractorContactModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contractorId = contractor.id;
    modalRef.result.then(() => this.list(), () => { });
  }

  openConfirmationDismissModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(ConfirmationDismissModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then(() => this.dismiss(contractor.id), () => { });
  }

  dismiss(id: number): void {
    this.positionService.dismissCurrent({ contractorId: id }).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.contractor, this.notification.contractorDismiss, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list();
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  openTransferToNewPositionModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(TransferNewPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then((contractor: Contractor) => this.transferToNewPosition(contractor), () => { });
  }

  transferToNewPosition(contractor): void {
    this.isLoading = true;
    this.positionService.transferToNewPosition(contractor).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.contractor, this.notification.contractorUpdate, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list();
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  openAddPreviousPositionModal(contractor: Contractor): void {
    const modalRef = this.modalService.open(AddOldPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = contractor;
    modalRef.result.then((data) => this.addPreviousPosition(data), () => { });
  }

  addPreviousPosition(data): void {
    this.isLoading = true;
    this.positionService.addPreviousPosition(data).subscribe(() => {
      this.list();
      this.notificationService.success(this.notification.contractor, this.notification.contractorUpdate, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list();
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  setFilter(field: string, value): void {
    this.filters[field] = value;
  }
}
