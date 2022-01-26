import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddOldPositionModalComponent } from 'projects/personal/src/app/utils/modals/add-old-position-modal/add-old-position-modal.component';
import { ChangeCurrentPositionModalComponent } from 'projects/personal/src/app/utils/modals/change-current-position-modal/change-current-position-modal.component';
import { ConfirmationDismissModalComponent } from 'projects/personal/src/app/utils/modals/confirmation-dismiss-modal/confirmation-dismiss-modal.component';
import { TransferNewPositionModalComponent } from 'projects/personal/src/app/utils/modals/transfer-new-position-modal/transfer-new-position-modal.component';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { PositionService } from 'projects/personal/src/app/utils/services/position.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { PositionModel } from '../../../../utils/models/position.model';
import { saveAs } from 'file-saver';
import { ObjectUtil } from '../../../../utils/util/object.util';

@Component({
  selector: 'app-position-table',
  templateUrl: './position-table.component.html',
  styleUrls: ['./position-table.component.scss']
})
export class PositionTableComponent implements OnInit {
  @Input() contractor: Contractor;
  contractorId: number;
  isLoading: boolean = true;
  positions: PositionModel;
  pagedSummary: PagedSummary = {
    pageSize: 10,
    currentPage: 1,
    totalCount: 0,
    totalPages: 0
  };
  constructor(private positionService: PositionService,
              private notificationService: NotificationsService,
              private route: ActivatedRoute,
              private modalService: NgbModal,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
      if (response.id) {
        this.contractorId = response.id;
        this.list({contractorId: response.id});
        return;
      }
    });
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      contractorId: this.contractorId,
      page: data.page || this.pagedSummary.currentPage || 1,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize
    })
    this.positionService.list(request).subscribe(response => {
      if (response.success) {
        this.positions = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    }, error => {
      if (error.status === 400) {
        this.isLoading = false;
        this.notificationService.warn('Warning', 'Request error occured!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    })
  }

  openChangeCurrentPositionModal(): void {
    const modalRef = this.modalService.open(ChangeCurrentPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = { id: this.contractorId };
    modalRef.result.then((contractor: Contractor) => this.changeCurrentPosition(contractor), () => { });
  }

  changeCurrentPosition(contractor): void {
    this.isLoading = true;
    this.positionService.changeCurrentPosition(contractor).subscribe(() => {
      this.list({contractorId: this.contractorId});
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list({contractorId: this.contractorId});
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  openConfirmationDismissModal(): void {
    const modalRef = this.modalService.open(ConfirmationDismissModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then(() => this.dismiss(), () => { });
  }

  dismiss(): void {
    this.positionService.dismissCurrent({ contractorId: this.contractor.id }).subscribe(() => {
      this.list({contractorId: this.contractorId});
      this.notificationService.success('Contractor', 'Contractor has been dismissed successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list({contractorId: this.contractorId});
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  openTransferToNewPositionModal(): void {
    const modalRef = this.modalService.open(TransferNewPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then((data) => this.transferToNewPosition(data), () => { });
  }

  transferToNewPosition(data): void {
    this.isLoading = true;
    this.positionService.transferToNewPosition(data).subscribe(() => {
      this.list({contractorId: this.contractorId});
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list({contractorId: this.contractorId});
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  openAddPreviousPositionModal(): void {
    const modalRef = this.modalService.open(AddOldPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then((data) => this.addPreviousPosition(data), () => { });
  }

  addPreviousPosition(data): void {
    this.isLoading = true;
    this.positionService.addPreviousPosition(data).subscribe(() => {
      this.list({contractorId: this.contractorId});
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.list({contractorId: this.contractorId});
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  download(item): void {
    this.fileService.get(item.orderId).subscribe(response => {
      const fileName = item.orderName;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    })
  }
}
