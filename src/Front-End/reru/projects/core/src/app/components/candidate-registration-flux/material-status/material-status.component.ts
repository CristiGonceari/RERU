import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Observable, Subject } from 'rxjs';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ReferenceService } from '../../../utils/services/reference.service';
import { MaterialStatusModel } from '../../../utils/models/material-status.model';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { MaterialStatusService } from '../../../utils/services/material-status.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { MaterialStatusEnum } from '../../../utils/models/material-status.enum';
import { KinshipRelationWithUserProfileService } from '../../../utils/services/kinship-relation-with-user-profile.service';
import { KinshipRelationWithUserProfileModel } from '../../../utils/models/kinship-relation-with-user-profile.model';
import { KinshipRelationService } from '../../../utils/services/kinship-relation.service';
import { KinshipRelationCriminalDataService } from '../../../utils/services/kinship-relation-criminal-data.service';
import { KinshipRelationModel } from '../../../utils/models/kinship-relation.model';
import { ValidatorUtil } from '../../../utils/util/validator.util';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { KinshipRelationCriminalDataModel } from '../../../utils/models/kinship-relation-criminal-data.model';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-material-status',
  templateUrl: './material-status.component.html',
  styleUrls: ['./material-status.component.scss']
})
export class MaterialStatusComponent implements OnInit {

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  public Editor = DecoupledEditor;

  isLoadingMaterialStatus: boolean = true;
  isLoadingKinshipRelationWithUserProfile: boolean = true;
  isLoadingKinshipRelation: boolean = true;
  isLoadingKinshipRelationCriminalData: boolean = true;

  materialStatusForm: FormGroup;
  kinshipRelationWithUserProfileForm: FormGroup;
  kinshipRelationForm: FormGroup;
  kinshipRelationCriminalDataForm: FormGroup;

  userGeneralData
  userId;
  stepId;
  materialStatusTypes;
  userMaterialStatus;

  kinshipRelationWithUserProfileData;
  kinshipRelationData;
  kinshipRelationCriminalData;
  registrationFluxStep;

  addOrEditMaterialStatusButton: boolean;
  addOrEditKinshipRelationWithUserProfileButton: boolean;
  addOrEditKinshipRelationButton: boolean;
  addOrEditKinshipRelationCriminalDataButton: boolean;

  kinshipDegreeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{ label: '', value: '' }];
    
    constructor(private fb: FormBuilder,
      private referenceService: ReferenceService,
      private notificationService: NotificationsService,
      private route: ActivatedRoute,
      private materialStatusService: MaterialStatusService,
      private userProfile: UserProfileService,
      private kinshipRelationWithUserProfileService: KinshipRelationWithUserProfileService,
      private kinshipRelationService: KinshipRelationService,
      private kinshipRelationCriminalDataService: KinshipRelationCriminalDataService,
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

  initForm(data?, materialStatusEnum?: MaterialStatusEnum): void {
    
    if(data == null){

      this.materialStatusForm = this.fb.group({
        id: this.fb.control( null, []),
        materialStatusTypeId: this.fb.control( null, []),
        userProfileId: this.fb.control( null, [])
      });

      this.kinshipRelationCriminalDataForm = this.fb.group({
        id: this.fb.control(null, []),
        text: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
        userProfileId: this.fb.control(null, [])
      });

      this.kinshipRelationWithUserProfileForm = this.fb.group({
        relationWithUserProfile: this.fb.array([this.generateKinshipRelationWithUserProfile()])
      });

      this.kinshipRelationForm = this.fb.group({
        relation: this.fb.array([this.generateKinshipRelation()])
      });

    }
    else if (materialStatusEnum == MaterialStatusEnum.MaterialStatus ){

      this.materialStatusForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        materialStatusTypeId: this.fb.control((data && data.materialStatusTypeId) || null, []),
        userProfileId: this.fb.control(data.userProfileId || null, [])
      });

    }
    else if (materialStatusEnum == MaterialStatusEnum.KinshipRelationWithUserProfile){
       
      this.kinshipRelationWithUserProfileForm = this.fb.group({
        relationWithUserProfile: this.fb.array([this.generateKinshipRelationWithUserProfile(data, this.userId)])
      });
    }
    else if(materialStatusEnum == MaterialStatusEnum.KinshipRelation){
      
      this.kinshipRelationForm = this.fb.group({
        relation: this.fb.array([this.generateKinshipRelation(data, this.userId)])
      });
    } 
    else if(materialStatusEnum == MaterialStatusEnum.KinshipRelationCriminalData){
      
      this.kinshipRelationCriminalDataForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) ||null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
        userProfileId: this.fb.control(data.userProfileId || null, [])
      });
    } 
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe( res => {

      this.userGeneralData = res.data;
      const userData = res.data;

      if (userData.materialStatusId != 0) {

        this.getUserMaterialStatus(this.userGeneralData.id);
      }
      else {
        this.addOrEditMaterialStatusButton = false;
        this.isLoadingMaterialStatus = false;
        this.userMaterialStatus = null;
      }

      if (userData.kinshipRelationCriminalDataId != 0) {

        this.getKinshipRelationCriminalData(this.userGeneralData.id);
      }
      else {
        this.addOrEditKinshipRelationCriminalDataButton = false;
        this.isLoadingKinshipRelationCriminalData = false;
        this.kinshipRelationCriminalData = null;
      }

      if (userData.kinshipRelationWithUserProfilesCount != 0) {

        this.getKinshipRelationWithUserProfile(this.userGeneralData.id);
      }
      else {
        this.addOrEditKinshipRelationWithUserProfileButton = false;
        this.isLoadingKinshipRelationWithUserProfile = false;
        this.kinshipRelationWithUserProfileData = null;
      }

      if (userData.kinshipRelationsCount != 0) {

          this.getKinshipRelation(this.userGeneralData.id);
        }
        else {
          this.addOrEditKinshipRelationButton = false;
          this.isLoadingKinshipRelation = false;
          this.kinshipRelationData = null;
        }
    })
  }

  getUserMaterialStatus(userId){
    this.isLoadingMaterialStatus = true;

    this.materialStatusService.get(userId).subscribe(res => {
      this.userMaterialStatus = res.data;
      this.initForm(this.userMaterialStatus, MaterialStatusEnum.MaterialStatus);
      this.isLoadingMaterialStatus = false;
      this.addOrEditMaterialStatusButton = true;
    })
    
  }

  getKinshipRelationCriminalData(userId){
    this.isLoadingKinshipRelationCriminalData = true;

    this.kinshipRelationCriminalDataService.get(userId).subscribe(res => {
      this.kinshipRelationCriminalData = res.data;
      this.initForm(this.kinshipRelationCriminalData, MaterialStatusEnum.KinshipRelationCriminalData);
      this.isLoadingKinshipRelationCriminalData = false;
      this.addOrEditKinshipRelationCriminalDataButton = true;
    })
    
  }

  getKinshipRelationWithUserProfile(userId) {
    this.isLoadingKinshipRelationWithUserProfile = true;

    const request = {
      userProfileId: userId
    }

    this.kinshipRelationWithUserProfileService.list(request).subscribe(res => {
      this.kinshipRelationWithUserProfileData = res.data.items;
      this.initKinshipRelationForm(this.kinshipRelationWithUserProfileData, MaterialStatusEnum.KinshipRelationWithUserProfile);
      this.isLoadingKinshipRelationWithUserProfile = false;
      this.addOrEditKinshipRelationWithUserProfileButton = true;
    })
  }

  getKinshipRelation(userId) {
    this.isLoadingKinshipRelation = true;

    const request = {
      userProfileId: userId
    }

    this.kinshipRelationService.list(request).subscribe(res => {
      this.kinshipRelationData = res.data.items;
      this.initKinshipRelationForm(this.kinshipRelationData, MaterialStatusEnum.KinshipRelation);
      this.isLoadingKinshipRelation = false;
      this.addOrEditKinshipRelationButton = true;
    })
  }
 
  initKinshipRelationForm(relation, materialStatusEnum: MaterialStatusEnum) {
    console.log(" relation", relation);
    
    if (relation != null) {

      for (let i = 0; i < relation.length; i++) {

        if (i > 0) {
          switch(materialStatusEnum){
            case MaterialStatusEnum.KinshipRelationWithUserProfile:
                this.addKinshipRelationWithUserProfile(relation[i]);
              break;
              case MaterialStatusEnum.KinshipRelation:
                this.addKinshipRelation(relation[i]);
                break;

          }
          
        }else{
          this.initForm(relation[i], materialStatusEnum)
        }
      }
    }
  }

  generateKinshipRelationWithUserProfile(kinship?, userProfileId?) {
    
    return this.fb.group({
      id: this.fb.control((kinship && kinship.id) || null, []),
      name: this.fb.control((kinship && kinship.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((kinship && kinship.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((kinship && kinship.function) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      subdivision: this.fb.control((kinship && kinship.subdivision) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      kinshipDegree: this.fb.control((kinship && kinship.kinshipDegree) || null, []),
      userProfileId: this.fb.control(userProfileId || null, []),
    });
  }

  generateKinshipRelation(kinship?, userProfileId?) {
    
    return this.fb.group({
      id: this.fb.control((kinship && kinship.id) || null, []),
      kinshipDegree: this.fb.control((kinship && kinship.kinshipDegree) || null, []),
      name: this.fb.control((kinship && kinship.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((kinship && kinship.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      birthDate: this.fb.control((kinship && kinship.birthDate) || null, [Validators.required]),
      birthLocation: this.fb.control((kinship && kinship.birthLocation) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((kinship && kinship.function) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      workLocation: this.fb.control((kinship && kinship.workLocation) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      residenceAddress: this.fb.control((kinship && kinship.residenceAddress) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      userProfileId: this.fb.control(userProfileId || null, []),
    });
  }

  retrieveDropdowns(): void {
    this.referenceService.getMaterialStatusType().subscribe(res => {
      var enums = res.data;
      this.materialStatusTypes = enums.sort(function(a, b){return a.value - b.value});
    });

    this.referenceService.getKinshipDegreeEnum().subscribe(res => {
      this.kinshipDegreeEnum = res.data;
    })
  }
  
  createMaterialStatus(){
    
    this.materialStatusService.add(this.parseMaterialStatus(this.materialStatusForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Material status was added!', NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.userId);
    }, errpr => {
      this.notificationService.error('Error', 'Material status was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  createKinshipRelationCriminalData(){
    
    this.kinshipRelationCriminalDataService.add(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Kinship relation was added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.userId);
    }, errpr => {
      this.notificationService.error('Error', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateMaterialStatus(){
    this.materialStatusService.update(this.parseMaterialStatus(this.materialStatusForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Material status was updated!', NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.userId);
    }, errpr => {
      this.notificationService.error('Error', 'Material status was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateKinshipRelationCriminalData(){
    this.kinshipRelationCriminalDataService.update(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Kinship relation status was updated!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.userId);
    }, errpr => {
      this.notificationService.error('Error', 'Kinship relation was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }

  creteKinshipRelationWithUserProfile(){
    this.buildKinshipRelationWithUserProfileForm().subscribe(response => {
      this.notificationService.success('Success', 'Kinship relation was added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationWithUserProfile(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  creteKinshipRelation(){
    this.buildKinshipRelationForm().subscribe(response => {
      this.notificationService.success('Success', 'Kinship relation added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelation(this.userGeneralData.id);
    }, error => {
      this.notificationService.error('Failure', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  buildKinshipRelationWithUserProfileForm(): Observable<any> {
    const request = this.parseKinshipRelationsWithUserProfile(this.kinshipRelationWithUserProfileForm.getRawValue().relationWithUserProfile, this.userId);
    return this.kinshipRelationWithUserProfileService.addMultiple(request);
  }

  buildKinshipRelationForm(): Observable<any> {
    const request = this.parseKinshipRelations(this.kinshipRelationForm.getRawValue().relation, this.userId);
    return this.kinshipRelationService.addMultiple(request);
  }

  parseKinshipRelationsWithUserProfile(data: KinshipRelationWithUserProfileModel[], userProfileId: number): KinshipRelationWithUserProfileModel[] {
    return data.map(el => this.parseKinshipRelationWithUserProfile(el, userProfileId));
  }

  parseKinshipRelations(data: KinshipRelationModel[], userProfileId: number): KinshipRelationModel[] {
    return data.map(el => this.parseKinshipRelation(el, userProfileId));
  }

  parseKinshipRelationWithUserProfile(data: KinshipRelationWithUserProfileModel, userProfileId): KinshipRelationWithUserProfileModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      function: data.function,
      subdivision: data.subdivision,
      kinshipDegree: parseInt(data.kinshipDegree),
      userProfileId: userProfileId
    })
  }

  parseKinshipRelation(data: KinshipRelationModel, userProfileId): KinshipRelationModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      function: data.function,
      birthLocation: data.birthLocation,
      birthDate: data.birthDate,
      workLocation: data.workLocation,
      residenceAddress: data.residenceAddress,
      kinshipDegree: parseInt(data.kinshipDegree),
      userProfileId: userProfileId
    })
  }

  parseKinshipRelationCriminalData(data, userId): KinshipRelationCriminalDataModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      text: data.text,
      userProfileId: userId
    })
  }

  parseMaterialStatus(data, userId): MaterialStatusModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      materialStatusTypeId: parseInt(data.materialStatusTypeId),
      userProfileId: userId
    })
  }

  addKinshipRelationWithUserProfile(kinship?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    if (kinship == null) {
      (<FormArray>this.kinshipRelationWithUserProfileForm.controls.relationWithUserProfile).controls.push(this.generateKinshipRelationWithUserProfile());
    } else {
      (<FormArray>this.kinshipRelationWithUserProfileForm.controls.relationWithUserProfile).controls.push(
        this.generateKinshipRelationWithUserProfile(
          kinship,
          this.userId
        )
      );
    }
  }
  addKinshipRelation(kinship?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());

    console.log("kinship", kinship);
    

    if (kinship == null) {
      (<FormArray>this.kinshipRelationForm.controls.relation).controls.push(this.generateKinshipRelation());
    } else {
      (<FormArray>this.kinshipRelationForm.controls.relation).controls.push(
        this.generateKinshipRelation(
          kinship,
          this.userId
        )
      );
    }
  }

  removeKinshipRelationWithUserProfile(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.kinshipRelationWithUserProfileForm.controls.relationWithUserProfile).controls.splice(index, 1);
  }

  removeKinshipRelation(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.kinshipRelationForm.controls.relation).controls.splice(index, 1);
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
    if(this.userMaterialStatus != null || this.kinshipRelationCriminalData != null || this.kinshipRelationWithUserProfileData != null || this.kinshipRelationData != null){
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