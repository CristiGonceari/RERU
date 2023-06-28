import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddPhotoModalComponent } from 'projects/personal/src/app/utils/modals/add-photo-modal/add-photo-modal.component';
import { ConfirmResetPasswordModalComponent } from 'projects/personal/src/app/utils/modals/confirm-reset-password-modal/confirm-reset-password-modal.component';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';
import { AvatarModel } from 'projects/personal/src/app/utils/models/avatar.model';
import { ContactModel } from 'projects/personal/src/app/utils/models/contact.model';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { UserProfileService } from 'projects/personal/src/app/utils/services/user-profile.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { forkJoin, merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { ContractorParser } from '../../add/add.parser';
import { DataService } from '../data.service';
import { I18nService } from 'projects/personal/src/app/utils/services/i18n.service';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.scss']
})
export class GeneralComponent implements OnInit {
  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  @Output() getAvatar: EventEmitter<void> = new EventEmitter<void>();
  @Input() contractor: Contractor;
  
  originalContractor: Contractor;
  contacts: ContactModel[];
  avatar: AvatarModel[];
  generalForm: FormGroup;
  bloodTypes: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  isLoading: boolean = true;
  selectedItem: SelectItem;
  isLoadingAccessButton: boolean;
  fileId: string;
  fileType: string = null;
  attachedFile: File;
  contractorId: any;
  mediaFileId: string;
  stepId;

  registrationFluxStep;
  registrationFluxStepId;

  sexEnum: SelectItem[] = [{ label: "Select", value: "null" }];
  stateLanguageLevelEnum: SelectItem[] = [{ label: "", value: "" }];

  nationalities;
  citizenships;

  title: string;
  description: string;

  constructor(private fb: FormBuilder,
    private contractorService: ContractorService,
    private notificationService: NotificationsService,
    private referenceService: ReferenceService,
    private modalService: NgbModal,
    private userProfileService: UserProfileService,
    private router: Router,
    private route: ActivatedRoute,
    private ds: DataService,
    private registrationFluxService: RegistrationFluxStepService,
    private translate: I18nService
    ) { }

  ngOnInit(): void {
    this.originalContractor = { ...this.contractor };
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());
    
    this.initForm();
    this.retrieveDropdowns();
    this.subscribeForParams();
  }

  getUser(id: number): void {
    this.isLoading = true;
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.contractor = response.data;
      this.contacts = response.data.contacts;
      this.avatar = response.data.avatar;
      this.initForm(this.contractor);
      this.isLoading = false;
    });
  }

  subscribeForParams(): void {
    this.isLoading = true;
    this.route.params.subscribe(response => {
      if (response.order) {
        // this.openGenerateOrderModal();
      }

      this.getUser(this.contractorId);
      this.getExistentStep(this.stepId, this.contractor.id);
    })
  }

  retrieveDropdowns(): void {
    this.referenceService.getCandidateSexEnum().subscribe((res) => {
      this.sexEnum = res.data;
    });

    this.referenceService.getCandidateStateLanguageLevelEnum().subscribe((res) => {
      let languageEnum = res.data
      this.stateLanguageLevelEnum = languageEnum.sort(function(a, b){return a.value - b.value});
    });

    this.referenceService.getCandidateCitizenship().subscribe((res) => {
      this.citizenships = res.data;
    });

    this.referenceService.getCandidateNationalities().subscribe((res) => {
      this.nationalities = res.data;
    })
  }

  initForm(contractor: Contractor = <Contractor>{}): void {
    var phonePattern = "^((\\+373-?)|0)?[0-9]{8}$"
    var namePattern = '^[a-zA-ZĂăÎîȘșȚțÂâ]+([- ]?[a-zA-ZĂăÎîȘșȚțÂâ]+)*$';

    this.generalForm = this.fb.group({
      id: this.fb.control(contractor.id),
      firstName: this.fb.control(contractor.firstName, [Validators.required, Validators.pattern(namePattern)]),
      lastName: this.fb.control(contractor.lastName, [Validators.required, Validators.pattern(namePattern)]),
      fatherName: this.fb.control(contractor.fatherName, [Validators.pattern(namePattern)]),
      // idnp: this.fb.control((contractor && contractor.idnp)  || null, [Validators.required]),
      birthDate: this.fb.control(contractor.birthDate, [Validators.required]),
      sex: this.fb.control(contractor.sex, [Validators.required]),
      homePhone: this.fb.control((contractor && contractor.homePhone)  || null, [Validators.pattern(phonePattern)]),
      phoneNumber: this.fb.control((contractor && contractor.phoneNumber)  || null, [Validators.required, Validators.pattern(phonePattern)]),
      workPhone: this.fb.control((contractor && contractor.workPhone)  || null, [Validators.pattern(phonePattern)]),
      candidateNationalityId: this.fb.control( (contractor && contractor.candidateNationalityId) || null, [Validators.required]),
      candidateCitizenshipId: this.fb.control((contractor && contractor.candidateCitizenshipId)  || null, [Validators.required]),
      stateLanguageLevel: this.fb.control( (contractor && contractor.stateLanguageLevel)  || null, [Validators.required]),
    });
    this.isLoadingAccessButton = false;
  }

  formIsNotValid(): boolean {
    return !this.generalForm.valid;
  }

  hasErrors(field): boolean {
		return this.generalForm?.get(field)?.invalid;
	}

  hasError(field: string, error = 'required'): boolean {
		return (
			this.generalForm.get(field).invalid &&
			this.generalForm.get(field).hasError(error)
		);
	}

  // searchBloodType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
  //   const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
  //   const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
  //   const inputFocus$ = this.focus$;

  //   return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
  //     map(term => (term === '' ? this.bloodTypes
  //       : this.bloodTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  // }

  // selectBloodType(bloodType: SelectItem): void {
  //   this.generalForm.get('bloodTypeId').patchValue(bloodType ? +bloodType.value : null);
  // }

  getExistentStep(step, contractorId){
    
    const request = {
      contractorId : contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
      this.registrationFluxStepId = res.data.id;
    })
  }

  formatter = (x: SelectItem) => x.label;

  submit(): void {
    this.isLoading = true;
    const request = ContractorParser.parseContractor(this.generalForm.getRawValue());
    this.contractorService.update(request).subscribe(response => {
      this.isLoading = false;
      this.contractorService.fetchContractor.next();
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , response.success, this.contractor.id);
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('candidate-registration-flux.update-general-data-success'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.isLoading = false;
      forkJoin([
				this.translate.get('modal.error'),
				this.translate.get('candidate-registration-flux.update-general-data-error'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , error.success, this.contractor.id);
    });
  }

  resetContractor(): void {
    this.isLoading = true;
    if (!this.contractor) {
      this.getUser(this.contractor.id);
      this.isLoading = false;
      } else {
      this.initForm(this.contractor);
      this.isLoading = false;
      }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId){
    const datas= {
      isDone: success,
      stepId: this.stepId
    }
    if(stepData.length == 0){
      this.addCandidateRegistationStep(success, stepId, contractorId);
      this.ds.sendData(datas);
    }else{
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId ){
    const request = {
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      //this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      //this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    this.registrationFluxService.update(request).subscribe(res => {
      //this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      //this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
  
  // openAccessModal(): void {
  //   this.getUser(this.contractor.id);
  //   const modalRef = this.modalService.open(AddAccessModalComponent, { centered: true, backdrop: 'static' });
  //   modalRef.componentInstance.contractorId = this.originalContractor.id;
  //   modalRef.result.then((response) => this.createAccess(response), () => { this.getUser(this.contractor.id); })
  // }

  // createAccess(data): void {
  //   this.isLoadingAccessButton = true;
  //   this.userProfileService.addAccess({ email: data.email, contractorId: this.contractor.id, moduleRoles: data.moduleRoles }).subscribe(() => {
  //     this.getUser(this.contractor.id);
  //     this.notificationService.success('Success', 'User access added!', NotificationUtil.getDefaultConfig());
  //     this.getUser(this.contractor.id);
  //   }, () => {
  //     this.isLoadingAccessButton = false;
  //   });
  // }

  openResetPasswordModal(): void {
    const modalRef = this.modalService.open(ConfirmResetPasswordModalComponent, { centered: true, backdrop: 'static' });
    modalRef.result.then(() => this.resetPassword(), () => { });
  }

  resetPassword(): void {
    this.userProfileService.resetPassword(this.contractor.id).subscribe(() => {
      this.notificationService.success('Success', 'User profile added!', NotificationUtil.getDefaultConfig());
    });
  }

  // openGenerateOrderModal(): void {
  //   const modalRef = this.modalService.open(AskGenerateOrderModalComponent, { centered: true, backdrop: 'static' });
  //   modalRef.result.then(() => this.router.navigate(['../../order', this.contractor.id], { relativeTo: this.route }), () => { });
  // }

  openUploadProfilePhoto(): void {
    const modalRef = this.modalService.open(AddPhotoModalComponent, { centered: true, backdrop: 'static' });
    modalRef.componentInstance.contractorId = this.originalContractor.id;
    modalRef.result.then(() => this.getAvatar.emit(), () => { });
  }

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }
}
