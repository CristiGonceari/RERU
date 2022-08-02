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
import { UserProfileGeneralDataService } from '../../../utils/services/user-profile-general-data.service';
import { ArrayType } from '@angular/compiler';

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
  userDatas;

  registrationFluxStep;
  registrationFluxStepId;

  generealDatasLoading: boolean = true;
  isLoading: boolean = true;

  userId;
  stepId;
  contractorId;

  sexEnum: SelectItem[] = [{ label: "", value: "" }];
  stateLanguageLevelEnum: SelectItem[] = [{ label: "", value: "" }];


  constructor( private referenceService: ReferenceService,
    private userProfile: UserProfileService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private user: UserService,
    private registrationFluxService: RegistrationFluxStepService,
    public notificationService: NotificationsService,
    private ds: DataService,
    private userProfileGeneralDataService: UserProfileGeneralDataService
    ) { }

  ngOnInit(): void {
    this.initForm();
    this.userId =parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

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

      this.userDatas = res.data;
      this.contractorId = res.data.contractorId;
      this.generealDatasLoading = false;

      this.getExistentStep(this.stepId, this.contractorId)
    })

    this.userProfile.getCandidateGeneralDatas(request).subscribe(res => {

     
      this.generalDatas = res.data;
      this.initForm(this.generalDatas);
      this.isLoading = false;
      
    })
  }

  getSelectedValue(){
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
 
  parseEditUserProfileDetails(contractorId, generalData, id){
    return {
      id: id,
      contractorId: contractorId,
      workPhone: generalData.workPhone,
      homePhone: generalData.homePhone,
      sex: parseInt(generalData.sex),
      stateLanguageLevel: parseInt(generalData.statelanguageLevel),
      candidateNationalityId: parseInt(generalData.nationalityTypeId),
      candidateCitizenshipId: parseInt(generalData.citizenshipTypeId)
    }
  }

  updateUserProfileGeneralData(){
    this.user.editCandidateDetails(this.parseEditUserProfileDetails(this.contractorId, this.userForm.value, this.generalDatas.id)).subscribe(res =>{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , res.success, this.contractorId);
      this.notificationService.success('Success', 'Contractor details added!', NotificationUtil.getDefaultMidConfig());

    }, error => {
      this.notificationService.error('Failure', 'Contractor details was not added!', NotificationUtil.getDefaultMidConfig());
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId , error.success, this.contractorId);
    })
  }

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
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
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
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
 
}
