import { Component, Input, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { Observable, Subject } from 'rxjs';
import { DataService } from '../data.service';
import { MaterialStatusService } from 'projects/personal/src/app/utils/services/material-status.service';
import { KinshipRelationWithUserProfileService } from 'projects/personal/src/app/utils/services/kinship-relation-with-user-profile.service';
import { KinshipRelationService } from 'projects/personal/src/app/utils/services/kinship-relation.service';
import { KinshipRelationCriminalDataService } from 'projects/personal/src/app/utils/services/kinship-relation-criminal-data.service';
import { MaterialStatusEnum } from 'projects/personal/src/app/utils//models/material-status.enum';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { KinshipRelationWithUserProfileModel } from 'projects/personal/src/app/utils/models/kinship-relation-with-user-profile.model';
import { KinshipRelationModel } from 'projects/personal/src/app/utils/models/kinship-relation.model';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { KinshipRelationCriminalDataModel } from 'projects/personal/src/app/utils/models/kinship-relation-criminal-data.model';
import { MaterialStatusModel } from 'projects/personal/src/app/utils/models/material-status.model';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';

@Component({
  selector: 'app-family',
  templateUrl: './family.component.html',
  styleUrls: ['./family.component.scss']
})
export class FamilyComponent implements OnInit {
  @Input() contractor: Contractor;
  // families: FamilyModel[];
  // pagedSummary: PagedSummary = new PagedSummary();

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  isLoading: boolean = true;

  public Editor = DecoupledEditor;

  isLoadingMaterialStatus: boolean = true;
  isLoadingKinshipRelationWithUserProfile: boolean = true;
  isLoadingKinshipRelation: boolean = true;
  isLoadingKinshipRelationCriminalData: boolean = true;

  materialStatusForm: FormGroup;
  kinshipRelationWithUserProfileForm: FormGroup;
  kinshipRelationForm: FormGroup;
  kinshipRelationCriminalDataForm: FormGroup;

  stepId;
  materialStatusTypes;
  userMaterialStatus;

  kinshipRelationWithUserProfileData;
  kinshipRelationData;
  kinshipRelationCriminalData;
  registrationFluxStep;
  contractorId;

  addOrEditMaterialStatusButton: boolean;
  addOrEditKinshipRelationWithUserProfileButton: boolean;
  addOrEditKinshipRelationButton: boolean;
  addOrEditKinshipRelationCriminalDataButton: boolean;

  kinshipDegreeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{ label: '', value: '' }];
  

  constructor(
    private notificationService: NotificationsService,
    private kinshipRelationWithUserProfileService: KinshipRelationWithUserProfileService,
    private kinshipRelationService: KinshipRelationService,
    private kinshipRelationCriminalDataService: KinshipRelationCriminalDataService,
    private materialStatusService: MaterialStatusService,
    private ds: DataService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private referenceService: ReferenceService,
    private contractorService: ContractorService,
    private registrationFluxService: RegistrationFluxStepService,
      ) { }

  ngOnInit(): void {
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);
    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.subscribeForParams();
    this.retrieveDropdowns();
    // this.subscribeForData();
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
        contractorId: this.fb.control( null, [])
      });

      this.kinshipRelationCriminalDataForm = this.fb.group({
        id: this.fb.control(null, []),
        text: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
        contractorId: this.fb.control(null, [])
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
        contractorId: this.fb.control(data.contractorId || null, [])
      });

    }
    else if (materialStatusEnum == MaterialStatusEnum.KinshipRelationWithUserProfile){
       
      this.kinshipRelationWithUserProfileForm = this.fb.group({
        relationWithUserProfile: this.fb.array([this.generateKinshipRelationWithUserProfile(data, this.contractor.id)])
      });
    }
    else if(materialStatusEnum == MaterialStatusEnum.KinshipRelation){
      
      this.kinshipRelationForm = this.fb.group({
        relation: this.fb.array([this.generateKinshipRelation(data, this.contractor.id)])
      });
    } 
    else if(materialStatusEnum == MaterialStatusEnum.KinshipRelationCriminalData){
      
      this.kinshipRelationCriminalDataForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) ||null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
        contractorId: this.fb.control(data.contractorId || null, [])
      });
    } 
  }

  subscribeForParams(): void {
    this.getUser(this.contractorId);

    this.getExistentStep(this.stepId, this.contractor.id);
    this.isLoading = false;
}

  

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.contractor = response.data;
      this.subscribeForData(response.data)
    });
  }

  subscribeForData(contractor) {
    if (contractor.hasMaterialStatus) {

      this.getUserMaterialStatus(contractor.id);
    }
    else {
      this.addOrEditMaterialStatusButton = false;
      this.isLoadingMaterialStatus = false;
      this.userMaterialStatus = null;
    }

    if (contractor.hasKinshipRelationCriminalData) {

      this.getKinshipRelationCriminalData(contractor.id);
    }
    else {
      this.addOrEditKinshipRelationCriminalDataButton = false;
      this.isLoadingKinshipRelationCriminalData = false;
      this.kinshipRelationCriminalData = null;
    }

    if (contractor.hasKinshipRelationWithUserProfiles) {

      this.getKinshipRelationWithUserProfile(contractor.id);
    }
    else {
      this.addOrEditKinshipRelationWithUserProfileButton = false;
      this.isLoadingKinshipRelationWithUserProfile = false;
      this.kinshipRelationWithUserProfileData = null;
    }

    if (contractor.hasKinshipRelations) {

        this.getKinshipRelation(contractor.id);
      }
      else {
        this.addOrEditKinshipRelationButton = false;
        this.isLoadingKinshipRelation = false;
        this.kinshipRelationData = null;
      }
}

  getUserMaterialStatus(contractorId){
    this.isLoadingMaterialStatus = true;

    this.materialStatusService.get(contractorId).subscribe(res => {
      this.userMaterialStatus = res.data;
      this.initForm(this.userMaterialStatus, MaterialStatusEnum.MaterialStatus);
      this.isLoadingMaterialStatus = false;
      this.addOrEditMaterialStatusButton = true;
    })
    
  }

  getKinshipRelationCriminalData(contractorId){
    this.isLoadingKinshipRelationCriminalData = true;

    this.kinshipRelationCriminalDataService.get(contractorId).subscribe(res => {
      this.kinshipRelationCriminalData = res.data;
      this.initForm(this.kinshipRelationCriminalData, MaterialStatusEnum.KinshipRelationCriminalData);
      this.isLoadingKinshipRelationCriminalData = false;
      this.addOrEditKinshipRelationCriminalDataButton = true;
    })
    
  }

  getKinshipRelationWithUserProfile(contractorId) {
    this.isLoadingKinshipRelationWithUserProfile = true;

    const request = {
      contractorId: contractorId
    }

    this.kinshipRelationWithUserProfileService.list(request).subscribe(res => {
      this.kinshipRelationWithUserProfileData = res.data.items;
      this.initKinshipRelationForm(this.kinshipRelationWithUserProfileData, MaterialStatusEnum.KinshipRelationWithUserProfile);
      this.isLoadingKinshipRelationWithUserProfile = false;
      this.addOrEditKinshipRelationWithUserProfileButton = true;
    })
  }

  getKinshipRelation(contractorId) {
    this.isLoadingKinshipRelation = true;

    const request = {
      contractorId: contractorId
    }

    this.kinshipRelationService.list(request).subscribe(res => {
      this.kinshipRelationData = res.data.items;
      this.initKinshipRelationForm(this.kinshipRelationData, MaterialStatusEnum.KinshipRelation);
      this.isLoadingKinshipRelation = false;
      this.addOrEditKinshipRelationButton = true;
    })
  }
 
  initKinshipRelationForm(relation, materialStatusEnum: MaterialStatusEnum) {
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

  generateKinshipRelationWithUserProfile(kinship?, contractorId?) {
    
    return this.fb.group({
      id: this.fb.control((kinship && kinship.id) || null, []),
      name: this.fb.control((kinship && kinship.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((kinship && kinship.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((kinship && kinship.function) || null, [Validators.required]),
      subdivision: this.fb.control((kinship && kinship.subdivision) || null, [Validators.required]),
      kinshipDegree: this.fb.control((kinship && kinship.kinshipDegree) || null, []),
      contractorId: this.fb.control(contractorId || null, []),
    });
  }

  generateKinshipRelation(kinship?, contractorId?) {
    
    return this.fb.group({
      id: this.fb.control((kinship && kinship.id) || null, []),
      kinshipDegree: this.fb.control((kinship && kinship.kinshipDegree) || null, []),
      name: this.fb.control((kinship && kinship.name) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      lastName: this.fb.control((kinship && kinship.lastName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      birthDate: this.fb.control((kinship && kinship.birthDate) || null, [Validators.required]),
      birthLocation: this.fb.control((kinship && kinship.birthLocation) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      function: this.fb.control((kinship && kinship.function) || null, [Validators.required]),
      workLocation: this.fb.control((kinship && kinship.workLocation) || null, [Validators.required]),
      residenceAddress: this.fb.control((kinship && kinship.residenceAddress) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, []),
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
    
    this.materialStatusService.add(this.parseMaterialStatus(this.materialStatusForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Material status was added!', NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.contractor.id);
    }, errpr => {
      this.notificationService.error('Error', 'Material status was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  createKinshipRelationCriminalData(){
    
    this.kinshipRelationCriminalDataService.add(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Kinship relation was added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.contractor.id);
    }, errpr => {
      this.notificationService.error('Error', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateMaterialStatus(){
    this.materialStatusService.update(this.parseMaterialStatus(this.materialStatusForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Material status was updated!', NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.contractor.id);
    }, errpr => {
      this.notificationService.error('Error', 'Material status was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateKinshipRelationCriminalData(){
    this.kinshipRelationCriminalDataService.update(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Kinship relation status was updated!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.contractor.id);
    }, errpr => {
      this.notificationService.error('Error', 'Kinship relation was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }

  creteKinshipRelationWithUserProfile(){
    this.buildKinshipRelationWithUserProfileForm().subscribe(response => {
      this.notificationService.success('Success', 'Kinship relation was added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationWithUserProfile(this.contractor.id);
    }, error => {
      this.notificationService.error('Failure', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  creteKinshipRelation(){
    this.buildKinshipRelationForm().subscribe(response => {
      this.notificationService.success('Success', 'Kinship relation added!', NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelation(this.contractor.id);
    }, error => {
      this.notificationService.error('Failure', 'Kinship relation was not added!', NotificationUtil.getDefaultMidConfig());
    });
  }

  buildKinshipRelationWithUserProfileForm(): Observable<any> {
    const request = this.parseKinshipRelationsWithUserProfile(this.kinshipRelationWithUserProfileForm.getRawValue().relationWithUserProfile, this.contractor.id);
    return this.kinshipRelationWithUserProfileService.addMultiple(request);
  }

  buildKinshipRelationForm(): Observable<any> {
    const request = this.parseKinshipRelations(this.kinshipRelationForm.getRawValue().relation, this.contractor.id);
    return this.kinshipRelationService.addMultiple(request);
  }

  parseKinshipRelationsWithUserProfile(data: KinshipRelationWithUserProfileModel[], contractorId: number): KinshipRelationWithUserProfileModel[] {
    return data.map(el => this.parseKinshipRelationWithUserProfile(el, contractorId));
  }

  parseKinshipRelations(data: KinshipRelationModel[], contractorId: number): KinshipRelationModel[] {
    return data.map(el => this.parseKinshipRelation(el, contractorId));
  }

  parseKinshipRelationWithUserProfile(data: KinshipRelationWithUserProfileModel, contractorId): KinshipRelationWithUserProfileModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      name: data.name,
      lastName: data.lastName,
      function: data.function,
      subdivision: data.subdivision,
      kinshipDegree: parseInt(data.kinshipDegree),
      contractorId: contractorId
    })
  }

  parseKinshipRelation(data: KinshipRelationModel, contractorId): KinshipRelationModel {
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
      contractorId: contractorId
    })
  }

  parseKinshipRelationCriminalData(data, contractorId): KinshipRelationCriminalDataModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      text: data.text,
      contractorId: contractorId
    })
  }

  parseMaterialStatus(data, contractorId): MaterialStatusModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      materialStatusTypeId: parseInt(data.materialStatusTypeId),
      contractorId: contractorId
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
          this.contractor.id
        )
      );
    }
  }
  addKinshipRelation(kinship?): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());
    
    if (kinship == null) {
      (<FormArray>this.kinshipRelationForm.controls.relation).controls.push(this.generateKinshipRelation());
    } else {
      (<FormArray>this.kinshipRelationForm.controls.relation).controls.push(
        this.generateKinshipRelation(
          kinship,
          this.contractor.id
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

  getExistentStep(step, contractorId){
    const request = {
      contractorId : contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep(){
    if(this.userMaterialStatus != null || this.kinshipRelationCriminalData != null || this.kinshipRelationWithUserProfileData != null || this.kinshipRelationData != null){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractor.id);
    }
    else{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractor.id);
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

  // retrieveFamilies(data: any = {}): void {
  //   const request = {
  //     contractorId: this.contractor.id,
  //     page: data.page || this.pagedSummary.currentPage,
  //     itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
  //   };
  //   this.familyService.list(request).subscribe(response => {
  //     this.families = response.data.items;
  //     this.pagedSummary = response.data.pagedSummary;
  //     this.isLoading = false;
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // openAddFamilyModal(): void {
  //   const modalRef = this.modalService.open(AddFamilyModalComponent);
  //   modalRef.componentInstance.contractorId = this.contractor.id;
  //   modalRef.result.then((response) => this.addFamily(response), () => {});
  // }

  // addFamily(data: FamilyModel): void {
  //   this.isLoading = true;
  //   this.familyService.create(data).subscribe(() => {
  //     this.retrieveFamilies();
  //     this.notificationService.success('Success', 'Family member added!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // openDeleteFamilyModal(relation: FamilyModel): void {
  //   const modalRef = this.modalService.open(DeleteFamilyModalComponent);
  //   modalRef.componentInstance.name = relation.relationName;
  //   modalRef.result.then(() => this.deleteFamily(relation.id), () => {});
  // }

  // deleteFamily(id: number): void {
  //   this.isLoading = true;
  //   this.familyService.delete(id).subscribe(() => {
  //     this.retrieveFamilies();
  //     this.notificationService.success('Success', 'Family deleted!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // openEditFamilyModal(data: FamilyModel): void {
  //   const modalRef = this.modalService.open(EditFamilyModalComponent);
  //   modalRef.componentInstance.contractorId = this.contractor.id;
  //   modalRef.componentInstance.family = data;
  //   modalRef.result.then((response) => this.editFamily(response), () => {});
  // }

  // editFamily(data: FamilyModel): void {
  //   this.isLoading = true;
  //   this.familyService.update(data).subscribe(() => {
  //     this.retrieveFamilies();
  //     this.notificationService.success('Success', 'Family member updated!', NotificationUtil.getDefaultConfig());
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }
}
