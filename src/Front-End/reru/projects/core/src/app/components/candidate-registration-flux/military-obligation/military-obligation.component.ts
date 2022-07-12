import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { MilitaryObligationService } from '../../../utils/services/military-obligation.service';
import { Observable, Subject } from 'rxjs';
import { ReferenceService } from '../../../utils/services/reference.service';
import { MilitaryObligationModel } from '../../../utils/models/military-obligation.model';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-military-obligation',
  templateUrl: './military-obligation.component.html',
  styleUrls: ['./military-obligation.component.scss']
})
export class MilitaryObligationComponent implements OnInit {

  militaryObligationForm: FormGroup;
  userGeneralData;
  userId;
  stepId;

  addOrEditMilitaryObligationButton: boolean;
  isLoadingMilitaryObligation: boolean = true;
  militaryObligationData;
  registrationFluxStep;

  militaryObligationTypeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];

  isDone:boolean;


  constructor(private fb: FormBuilder,
    private userProfile: UserProfileService,
    private route: ActivatedRoute,
    private militaryObligationService: MilitaryObligationService,
    private referenceService: ReferenceService,
    private notificationService: NotificationsService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService


    ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());
    this.getExistentStep(this.stepId);

    this.initForm();
    this.retrieveDropdowns();
    this.getUserGeneralData();
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
}

  initForm(data?): void {
    console.log("data", data);
    
    if(data == null){
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations()])
      });
    }
    else {
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations(data, this.userId)])
      });
    }
    
  }
  
  retrieveDropdowns(): void {
    this.referenceService.getMilitaryObligationTypeEnum().subscribe(res => {
      this.militaryObligationTypeEnum = res.data;
    });
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe( res => {

      this.userGeneralData = res.data;
      const userData = res.data;

      if (userData.militaryObligationsCount != 0) {

        this.getMilitaryObligations(this.userGeneralData.id);
      }
      else {
        this.addOrEditMilitaryObligationButton = false;
        this.isLoadingMilitaryObligation = false;
        this.militaryObligationData = null;
      }
      
    })
  }

  getMilitaryObligations(userId) {
    this.isLoadingMilitaryObligation = true;

    const request = {
      userProfileId: userId
    }

    this.militaryObligationService.list(request).subscribe(res => {
      this.militaryObligationData = res.data.items;
      this.initMilitaryObligationForm(this.militaryObligationData);
      this.isLoadingMilitaryObligation = false;
      this.addOrEditMilitaryObligationButton = true;
    })
  }

  initMilitaryObligationForm(obligation) {
    
    if (obligation != null) {

      for (let i = 0; i < obligation.length; i++) {

        if (i > 0) {

          this.addMilitaryObligation(obligation[i]);
          
        }else{
          this.initForm(obligation[i])
        }
      }
    }
  }

  generateMilitaryObligations(militaryObligation?, userProfileId?) {
    
    return this.fb.group({
      id: this.fb.control((militaryObligation && militaryObligation.id) || null, []),
      militaryObligationType: this.fb.control((militaryObligation && militaryObligation.militaryObligationType) || null, [Validators.required]),
      mobilizationYear: this.fb.control((militaryObligation && militaryObligation.mobilizationYear) || null, [Validators.required]),
      withdrawalYear: this.fb.control((militaryObligation && militaryObligation.withdrawalYear) || null, [Validators.required]),
      efectiv: this.fb.control((militaryObligation && militaryObligation.efectiv) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      militarySpecialty: this.fb.control((militaryObligation && militaryObligation.militarySpecialty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      degree: this.fb.control((militaryObligation && militaryObligation.degree) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      militaryBookletSeries: this.fb.control((militaryObligation && militaryObligation.militaryBookletSeries) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      militaryBookletNumber: this.fb.control((militaryObligation && militaryObligation.militaryBookletNumber) || null, []),
      militaryBookletReleaseDay: this.fb.control((militaryObligation && militaryObligation.militaryBookletReleaseDay) || null, [Validators.required]),
      militaryBookletEminentAuthority: this.fb.control((militaryObligation && militaryObligation.militaryBookletEminentAuthority) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      userProfileId: this.fb.control(userProfileId || null, []),
    });
  }

  createMilitaryObligations(){
    this.buildMilitaryObligationForm().subscribe(response => {
      this.notificationService.success('Success', 'Military obligation relation added!', NotificationUtil.getDefaultMidConfig());
      this.getMilitaryObligations(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Military obligation relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  buildMilitaryObligationForm(): Observable<any> {
    const request = this.parseMilitaryObligations(this.militaryObligationForm.getRawValue().obligations, this.userId);
    return this.militaryObligationService.addMultiple(request);
  }

  parseMilitaryObligations(data: MilitaryObligationModel[], userProfileId: number): MilitaryObligationModel[] {
    return data.map(el => this.parseMilitaryObligation(el, userProfileId));
  }

  parseMilitaryObligation(data: MilitaryObligationModel, userProfileId): MilitaryObligationModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      militaryObligationType: parseInt(data.militaryObligationType),
      mobilizationYear: data.mobilizationYear,
      withdrawalYear: data.withdrawalYear,
      efectiv : data.efectiv,
      militarySpecialty : data.militarySpecialty,
      degree : data.degree,
      militaryBookletSeries : data.militaryBookletSeries,
      militaryBookletNumber : data.militaryBookletNumber,
      militaryBookletReleaseDay : data.militaryBookletReleaseDay,
      militaryBookletEminentAuthority : data.militaryBookletEminentAuthority,
      userProfileId: userProfileId
    })
  }

  addMilitaryObligation(obligation?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (obligation == null) {
      (<FormArray>this.militaryObligationForm.controls.obligations).controls.push(this.generateMilitaryObligations());
    } else {
      (<FormArray>this.militaryObligationForm.controls.obligations).controls.push(
        this.generateMilitaryObligations(
          obligation,
          this.userId
        )
      );
    }
  }

  removeMilitaryObligation(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.militaryObligationForm.controls.obligations).controls.splice(index, 1);
  }

  getExistentStep(step){
    const request = {
      userProfileId : this.userId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep(){
    if(this.militaryObligationData != null){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.userId);
    }
    else{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.userId);
    }
  }

  checkRegistrationStep(stepData, stepId, success, userId){
    const datas= {
      isDone: success,
      stepId: this.stepId
    }
    if(stepData.length == 0){
      this.addCandidateRegistationStep(success, stepId, userId);
      this.ds.sendData(datas);
    }else{
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, userId);
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
    }, error => {
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
