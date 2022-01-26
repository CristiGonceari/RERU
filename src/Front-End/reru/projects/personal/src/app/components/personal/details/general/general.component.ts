import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddAccessModalComponent } from 'projects/personal/src/app/utils/modals/add-access-modal/add-access-modal.component';
import { AddDocumentModalComponent } from 'projects/personal/src/app/utils/modals/add-document-modal/add-document-modal.component';
import { AskGenerateOrderModalComponent } from 'projects/personal/src/app/utils/modals/ask-generate-order-modal/ask-generate-order-modal.component';
import { ConfirmResetPasswordModalComponent } from 'projects/personal/src/app/utils/modals/confirm-reset-password-modal/confirm-reset-password-modal.component';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';
import { ContactModel } from 'projects/personal/src/app/utils/models/contact.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { UserProfileService } from 'projects/personal/src/app/utils/services/user-profile.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { ContractorParser } from '../../add/add.parser';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.scss']
})
export class GeneralComponent implements OnInit {
  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  @Input() contractor: Contractor;
  
  originalContractor: Contractor;
  contacts: ContactModel[];
  generalForm: FormGroup;
  bloodTypes: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  isLoading: boolean = true;
  selectedItem: SelectItem;
  isLoadingAccessButton: boolean;
  constructor(private fb: FormBuilder,
    private contractorService: ContractorService,
    private notificationService: NotificationsService,
    private referenceService: ReferenceService,
    private modalService: NgbModal,
    private userProfileService: UserProfileService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.originalContractor = { ...this.contractor };
    this.subscribeForParams();
    this.retrieveDropdowns();
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.isLoading = false;
      this.contractor = response.data;
      this.contacts = response.data.contacts;
      this.initForm(this.contractor);
    });
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.order) {
        this.openGenerateOrderModal();
      }
    })
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 1 }).subscribe(response => {
      this.bloodTypes = response.data;
      if (!this.contractor) {
        this.getUser(this.contractor.id);
      } else {
        this.initForm(this.contractor);
      }
    });
  }

  initForm(contractor: Contractor = <Contractor>{}): void {
    this.generalForm = this.fb.group({
      id: this.fb.control(contractor.id),
      firstName: this.fb.control(contractor.firstName, [Validators.required]),
      lastName: this.fb.control(contractor.lastName, [Validators.required]),
      fatherName: this.fb.control(contractor.fatherName, [Validators.required]),
      birthDate: this.fb.control(contractor.birthDate, [Validators.required]),
      bloodTypeId: this.fb.control(contractor.bloodTypeId, [Validators.required]),
      sex: this.fb.control(contractor.sex, [Validators.required])
    });
    const bloodType = this.bloodTypes.find(el => +el.value === contractor.bloodTypeId);
    this.selectedItem = bloodType;
    this.isLoading = false;
    this.isLoadingAccessButton = false;
  }

  searchBloodType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.bloodTypes
        : this.bloodTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectBloodType(bloodType: SelectItem): void {
    this.generalForm.get('bloodTypeId').patchValue(bloodType ? +bloodType.value : null);
  }

  formatter = (x: SelectItem) => x.label;

  submit(): void {
    this.isLoading = true;
    const request = ContractorParser.parseContractor(this.generalForm.getRawValue());
    this.contractorService.update(request).subscribe(response => {
      this.isLoading = false;
      this.contractorService.fetchContractor.next();
      this.notificationService.success('Success', 'Contractor updated!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  resetContractor(): void {
    this.isLoading = true;
    this.initForm(this.originalContractor);
  }

  openAccessModal(): void {
    this.getUser(this.contractor.id);
    const modalRef = this.modalService.open(AddAccessModalComponent, { centered: true, backdrop: 'static' });
    modalRef.componentInstance.contractorId = this.originalContractor.id;
    modalRef.result.then((response) => this.createAccess(response), () => { this.getUser(this.contractor.id); })
  }

  createAccess(data): void {
    this.isLoadingAccessButton = true;
    this.userProfileService.addAccess({ email: data.email, contractorId: this.contractor.id, moduleRoles: data.moduleRoles }).subscribe(() => {
      this.getUser(this.contractor.id);
      this.notificationService.success('Success', 'User access added!', NotificationUtil.getDefaultConfig());
      this.getUser(this.contractor.id);
    }, () => {
      this.isLoadingAccessButton = false;
    });
  }

  openResetPasswordModal(): void {
    const modalRef = this.modalService.open(ConfirmResetPasswordModalComponent, { centered: true, backdrop: 'static' });
    modalRef.result.then(() => this.resetPassword(), () => { });
  }

  resetPassword(): void {
    this.userProfileService.resetPassword(this.contractor.id).subscribe(() => {
      this.notificationService.success('Success', 'User profile added!', NotificationUtil.getDefaultConfig());
    });
  }

  openGenerateOrderModal(): void {
    const modalRef = this.modalService.open(AskGenerateOrderModalComponent, { centered: true, backdrop: 'static' });
    modalRef.result.then(() => this.router.navigate(['../../order', this.contractor.id], { relativeTo: this.route }), () => { });
  }

  openUploadProfilePhoto(): void {
    const modalRef = this.modalService.open(AddDocumentModalComponent, { centered: true, backdrop: 'static' });
    modalRef.result.then(response => this.uploadPhoto(response), () => { });
  }

  uploadPhoto(data): void {
    const form = new FormData();
    form.append('contractorId', `${this.contractor.id}`);
    form.append('avatar', data.file);
    this.contractorService.uploadPhoto(form).subscribe(() => {
      this.contractorService.fetchContractor.next();
      this.notificationService.success('Success', 'Photo updated!', NotificationUtil.getDefaultConfig());
    });
  }
}
