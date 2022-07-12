import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RegistrationFluxStepEnum } from '../../../utils/models/registrationFluxStep.enum';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ReferenceService } from '../../../utils/services/reference.service';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { UserService } from '../../../utils/services/user.service';
import { ValidatorUtil } from '../../../utils/util/validator.util';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DataService } from '../data.service';

@Component({
  selector: 'app-general-data-form',
  templateUrl: './general-data-form.component.html',
  styleUrls: ['./general-data-form.component.scss']
})
export class GeneralDataFormComponent implements OnInit {

  @Output() counterChange = new EventEmitter<number>();
  
  userForm: FormGroup;
  nationalities;
  citizenships;
  generalDatas;

  registrationFluxStep;
  registrationFluxStepId;

  generealDatasLoading: boolean = true;
  isLoading: boolean = true;

  userId;
  stepId;

  sexEnum: SelectItem[] = [{ label: "", value: "" }];
  stateLanguageLevelEnum: SelectItem[] = [{ label: "", value: "" }];

  constructor( private referenceService: ReferenceService,
    private userProfile: UserProfileService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private userService: UserService,
    private registrationFluxService: RegistrationFluxStepService,
    public notificationService: NotificationsService,
    private ds: DataService
    ) { }

  ngOnInit(): void {
    this.initForm();
    this.userId =parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());
    this.getExistentStep(this.stepId);

    this.getSelectedValue();
    this.getUserGeneralDatas();
    
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  getUserGeneralDatas(){
    const request = this.userId;
    this.userProfile.getCandidateProfile(request).subscribe(res => {
      
      this.generalDatas = res.data;
      this.generealDatasLoading = false;

      if (res.data != null){
        this.initForm(this.generalDatas);
        this.isLoading = false;
      }
    })

  }

  getSelectedValue(){
    this.referenceService.getCandidateSexEnum().subscribe((res) => {
      this.sexEnum = res.data;
    });

    this.referenceService.getCandidateStateLanguageLevelEnum().subscribe((res) => {
      this.stateLanguageLevelEnum = res.data;
    });

    this.referenceService.getCandidateCitizenship().subscribe((res) => {
      this.citizenships = res.data;
    });

    this.referenceService.getCandidateNationalities().subscribe((res) => {
      this.nationalities = res.data;
    })

  }

  initForm(initFormData?: any): void {
    
    this.userForm = this.fb.group({
      homePhone: this.fb.control((initFormData && initFormData.homePhone)  || null, [Validators.required]),
      workPhone: this.fb.control((initFormData && initFormData.workPhone)  || null, [Validators.required]),
      nationalityTypeId: this.fb.control( (initFormData && initFormData.candidateNationalityId) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      citizenshipTypeId: this.fb.control((initFormData && initFormData.candidateCitizenshipId)  || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      sex: this.fb.control((initFormData && initFormData.sex)  || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      statelanguageLevel: this.fb.control( (initFormData && initFormData.stateLanguageLevel)  || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
    });
  }

  hasErrors(field): boolean {
		return this.userForm.touched && this.userForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.userForm.get(field).invalid && this.userForm.get(field).touched && this.userForm.get(field).hasError(error)
		);
	}

  parseUserProfileDetails(userId, generalData){
    return {
      id: userId,
      workPhone: generalData.workPhone,
      homePhone: generalData.homePhone,
      sex: generalData.sex,
      stateLanguageLevel: generalData.statelanguageLevel,
      candidateNationalityId: parseInt(generalData.nationalityTypeId),
      candidateCitizenshipId: parseInt(generalData.citizenshipTypeId)
    }
  }

  updateUserProfile(){
    this.userService.editCandidateDetails(this.parseUserProfileDetails(this.userId, this.userForm.value)).subscribe(res =>{
      this.checkRegistrationStep(this.registrationFluxStep);
      this.notificationService.success('Success', 'Contractor details added!', NotificationUtil.getDefaultMidConfig());

    }, error => {
      this.notificationService.error('Failure', 'Contractor details was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  getExistentStep(step){
    const request = {
      userProfileId : this.userId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
      this.registrationFluxStepId = res.data.id;
    })
  }

  checkRegistrationStep(step){
    const datas= {
      isDone: true,
      stepId: this.stepId
    }

    if (step.length == 0){
      this.addCandidateRegistationStep(true, this.stepId, parseInt(this.userId) );
      this.ds.sendData(datas);
    }
    else{
      this.updateCandidateRegistationStep(step[0].id, true, this.stepId, parseInt(this.userId))
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, userId ){
    const request = {
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, errpr => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, userId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    
    this.registrationFluxService.update(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
 
}
