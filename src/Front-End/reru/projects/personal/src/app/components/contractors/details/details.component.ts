import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contractor } from '../../../utils/models/contractor.model';
import { ContractorService } from '../../../utils/services/contractor.service';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { SexEnum } from '../../../utils/models/sex.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChangeNameModalComponent } from '../../../utils/modals/change-name-modal/change-name-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ChangeCurrentPositionModalComponent } from '../../../utils/modals/change-current-position-modal/change-current-position-modal.component';
import { PositionService } from '../../../utils/services/position.service';
import { AddContractorContactModalComponent } from '../../../utils/modals/add-contractor-contact-modal/add-contractor-contact-modal.component';
import { ConfirmationDismissModalComponent } from '../../../utils/modals/confirmation-dismiss-modal/confirmation-dismiss-modal.component';
import { ConfirmationDeleteContractorComponent } from '../../../utils/modals/confirmation-delete-contractor/confirmation-delete-contractor.component';
import { TransferNewPositionModalComponent } from '../../../utils/modals/transfer-new-position-modal/transfer-new-position-modal.component';
import { AddOldPositionModalComponent } from '../../../utils/modals/add-old-position-modal/add-old-position-modal.component';
import { RegistrationFluxStepEnum } from '../../../utils/models/registrationFluxStep.enum';
import { DataService } from './data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  contractor: Contractor;
  fileId: string;
  isLoading: boolean = true;
  avatarLoading: boolean = true;
  sexEnum = SexEnum;
  steps;
  
  subscription: Subscription;

  dataPassed: any;
  stepEnum = RegistrationFluxStepEnum;

  constructor(
    private route: ActivatedRoute,
    private contractorService: ContractorService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    private positionService: PositionService,
    private ngZone: NgZone,
    private router: Router,
    private ds: DataService,
    ) {
      this.subscription = this.ds.getData().subscribe((x) => {
        this.dataPassed = x;
        
        if(this.dataPassed){
          var stepIndex = this.steps.findIndex(x => x.value == this.dataPassed.stepId );
          this.steps[stepIndex].isDone = this.dataPassed.isDone;
        }
     })
    }

  ngOnInit(): void {
    this.subscribeForChangeParams();
  }

  subscribeForChangeParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.subscribeForFetchContractor(+response.id);
        this.subscribeForContractorSteps(response.id);
      }
    })
  }

  subscribeForContractorSteps(contractorId: number){
    let step = [];

    this.contractorService.getCandidateSteps(contractorId).subscribe(res => {
      step = res.data.checkedSteps;
      step.sort(function(a, b){return a.value - b.value});
      this.steps = step;
    });
  }

  subscribeForFetchContractor(id: number): void {
    this.contractorService.fetchContractor.subscribe(() => this.getUser(id ? id : this.contractor.id));
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.isLoading = false;
      this.contractor = response.data;
      this.contractorService.contractor.next(response.data);
    });
    this.contractorService.getAvatar(id).subscribe((response: ApiResponse<any>)=>{
      this.fileId = response.data.mediaFileId;
      this.avatarLoading = false;
    })
  }

  openChangeNameModal(): void {
    const modalRef = this.modalService.open(ChangeNameModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then((contractor: Contractor) => this.updateContractorName(contractor), () => { });
  }

  updateContractorName(contractor: Contractor): void {
    this.isLoading = true;
    this.contractorService.updateName(contractor).subscribe(() => {
      this.getUser(contractor.id)
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    });
  }

  openChangeCurrentPositionModal(): void {
    const modalRef = this.modalService.open(ChangeCurrentPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = { id: this.contractor.id };
    modalRef.result.then((contractor: Contractor) => this.changeCurrentPosition(contractor), () => { });
  }

  changeCurrentPosition(contractor): void {
    this.isLoading = true;
    this.positionService.changeCurrentPosition(contractor).subscribe(() => {
      this.getUser(this.contractor.id);
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.getUser(this.contractor.id);
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  openAddContactModal(): void {
    const modalRef = this.modalService.open(AddContractorContactModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contractorId = this.contractor.id;
    modalRef.result.then(() => this.getUser(this.contractor.id), () => {  });
  }

  openConfirmationDismissModal(): void {
    const modalRef = this.modalService.open(ConfirmationDismissModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then(() => this.dismiss(), () => { });
  }

  dismiss(): void {
    this.positionService.dismissCurrent({ contractorId: this.contractor.id }).subscribe(() => {
      this.getUser(this.contractor.id);
      this.notificationService.success('Contractor', 'Contractor has been dismissed successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.getUser(this.contractor.id);
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  openConfirmationDeleteModal(): void {
    const modalRef: any = this.modalService.open(ConfirmationDeleteContractorComponent, { centered: true });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then(() => this.delete(this.contractor.id), () => { });
  }

  delete(id: number): void {
    this.isLoading = true;
    this.contractorService.delete(id).subscribe(response => {
      if (response.success) {
        this.ngZone.run(() => this.router.navigate(['../../'], { relativeTo: this.route }));
        this.notificationService.success('Success', 'Contractor has been successfully deleted!', NotificationUtil.getDefaultMidConfig());
      }
    }, (error) => {
      this.isLoading = false;
      this.getUser(this.contractor.id);
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  onOutletLoaded(component) {
    component.contractor = this.contractor;
  }

  openTransferToNewPositionModal(): void {
    const modalRef = this.modalService.open(TransferNewPositionModalComponent, { centered: true, size: 'md' });
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then((contractor: Contractor) => this.transferToNewPosition(contractor), () => { });
  }

  transferToNewPosition(contractor): void {
    this.isLoading = true;
    this.positionService.transferToNewPosition(contractor).subscribe(() => {
      this.getUser(this.contractor.id);
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.getUser(this.contractor.id);
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
      this.getUser(this.contractor.id);
      this.notificationService.success('Contractor', 'Contractor has been updated successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.isLoading = false;
      this.getUser(this.contractor.id);
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation failed!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  redirect(): void {
    switch(true) {
      case !this.contractor.hasBulletin: this.router.navigate(['personal', 'new', this.contractor.id], { queryParams: { step: 1 }});
       break;
      case !this.contractor.hasStudies: this.router.navigate(['personal', 'new', this.contractor.id], { queryParams: { step: 2 }});
      break;
      case !this.contractor.hasAvatar: this.router.navigate(['personal', 'new', this.contractor.id], { queryParams: { step: 3 }}); 
      break;
      case !this.contractor.hasEmploymentRequest: this.router.navigate(['personal', 'new', this.contractor.id,], { queryParams: { step: 4 }});
       break;
      case !this.contractor.hasIdentityDocuments: this.router.navigate(['personal', 'new', this.contractor.id], { queryParams: { step: 5 }});
       break;
      case !this.contractor.hasPositions: this.router.navigate(['personal', 'order', this.contractor.id], { queryParams: { step: 0 }}); 
      break;
      case !this.contractor.hasCim: this.router.navigate(['personal', 'order', this.contractor.id], { queryParams: { step: 1 }}); 
      break;
    }
  }
}
