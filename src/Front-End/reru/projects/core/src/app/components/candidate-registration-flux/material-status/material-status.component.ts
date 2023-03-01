import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin, Observable, Subject } from 'rxjs';
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
import { I18nService } from '../../../utils/services/i18n.service';

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
  contractorId;
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

  title: string;
  description: string;

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
    private ds: DataService,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.retrieveDropdowns();
    this.getUserGeneralData();
  }

  ngOnDestroy() {
    // clear message
    this.ds.clearData();
  }

  initForm(data?, materialStatusEnum?: MaterialStatusEnum): void {

    if (data == null) {

      this.materialStatusForm = this.fb.group({
        id: this.fb.control(null, []),
        materialStatusTypeId: this.fb.control(null, []),
        contractorId: this.fb.control(null, [])
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
    else if (materialStatusEnum == MaterialStatusEnum.MaterialStatus) {

      this.materialStatusForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        materialStatusTypeId: this.fb.control((data && data.materialStatusTypeId) || null, []),
        contractorId: this.fb.control(data.contractorId || null, [])
      });

    }
    else if (materialStatusEnum == MaterialStatusEnum.KinshipRelationWithUserProfile) {

      this.kinshipRelationWithUserProfileForm = this.fb.group({
        relationWithUserProfile: this.fb.array([this.generateKinshipRelationWithUserProfile(data, this.contractorId)])
      });
    }
    else if (materialStatusEnum == MaterialStatusEnum.KinshipRelation) {

      this.kinshipRelationForm = this.fb.group({
        relation: this.fb.array([this.generateKinshipRelation(data, this.contractorId)])
      });
    }
    else if (materialStatusEnum == MaterialStatusEnum.KinshipRelationCriminalData) {

      this.kinshipRelationCriminalDataForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
        contractorId: this.fb.control(data.contractorId || null, [])
      });
    }
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe(res => {

      this.userGeneralData = res.data;
      this.contractorId = res.data.contractorId;
      const userData = res.data;

      if (userData.materialStatusId != 0) {

        this.getUserMaterialStatus(this.contractorId);
      }
      else {
        this.addOrEditMaterialStatusButton = false;
        this.isLoadingMaterialStatus = false;
        this.userMaterialStatus = null;
      }

      if (userData.kinshipRelationCriminalDataId != 0) {

        this.getKinshipRelationCriminalData(this.contractorId);
      }
      else {
        this.addOrEditKinshipRelationCriminalDataButton = false;
        this.isLoadingKinshipRelationCriminalData = false;
        this.kinshipRelationCriminalData = null;
      }

      if (userData.kinshipRelationWithUserProfilesCount != 0) {

        this.getKinshipRelationWithUserProfile(this.contractorId);
      }
      else {
        this.addOrEditKinshipRelationWithUserProfileButton = false;
        this.isLoadingKinshipRelationWithUserProfile = false;
        this.kinshipRelationWithUserProfileData = null;
      }

      if (userData.kinshipRelationsCount != 0) {

        this.getKinshipRelation(this.contractorId);
      }
      else {
        this.addOrEditKinshipRelationButton = false;
        this.isLoadingKinshipRelation = false;
        this.kinshipRelationData = null;
      }

      this.getExistentStep(this.stepId, this.contractorId);
    })
  }

  getUserMaterialStatus(contractorId) {
    this.isLoadingMaterialStatus = true;

    this.materialStatusService.get(contractorId).subscribe(res => {
      this.userMaterialStatus = res.data;
      this.initForm(this.userMaterialStatus, MaterialStatusEnum.MaterialStatus);
      this.isLoadingMaterialStatus = false;
      this.addOrEditMaterialStatusButton = true;
    })

  }

  getKinshipRelationCriminalData(contractorId) {
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
          switch (materialStatusEnum) {
            case MaterialStatusEnum.KinshipRelationWithUserProfile:
              this.addKinshipRelationWithUserProfile(relation[i]);
              break;
            case MaterialStatusEnum.KinshipRelation:
              this.addKinshipRelation(relation[i]);
              break;

          }

        } else {
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
      kinshipDegree: this.fb.control((kinship && kinship.kinshipDegree) || null, [Validators.required]),
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
      birthLocation: this.fb.control((kinship && kinship.birthLocation) || null, [Validators.required]),
      function: this.fb.control((kinship && kinship.function) || null, [Validators.required]),
      workLocation: this.fb.control((kinship && kinship.workLocation) || null, [Validators.required]),
      residenceAddress: this.fb.control((kinship && kinship.residenceAddress) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, []),
    });
  }

  retrieveDropdowns(): void {
    this.referenceService.getMaterialStatusType().subscribe(res => {
      var enums = res.data;
      this.materialStatusTypes = enums.sort(function (a, b) { return a.value - b.value });
    });

    this.referenceService.getKinshipDegreeEnum().subscribe(res => {
      this.kinshipDegreeEnum = res.data;
    })
  }

  createMaterialStatus() {

    this.materialStatusService.add(this.parseMaterialStatus(this.materialStatusForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-material-status-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-material-status-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  createKinshipRelationCriminalData() {

    this.kinshipRelationCriminalDataService.add(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-kinship-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-kinship-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateMaterialStatus() {
    this.materialStatusService.update(this.parseMaterialStatus(this.materialStatusForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.update-material-status-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserMaterialStatus(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.update-material-status-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateKinshipRelationCriminalData() {
    this.kinshipRelationCriminalDataService.update(this.parseKinshipRelationCriminalData(this.kinshipRelationCriminalDataForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.update-kinship-relation-criminal-data-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationCriminalData(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.update-kinship-relation-criminal-data-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  creteKinshipRelationWithUserProfile() {
    this.buildKinshipRelationWithUserProfileForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-kinship-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelationWithUserProfile(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-kinship-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  creteKinshipRelation() {
    this.buildKinshipRelationForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-kinship-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getKinshipRelation(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-kinship-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  buildKinshipRelationWithUserProfileForm(): Observable<any> {
    const request = this.parseKinshipRelationsWithUserProfile(this.kinshipRelationWithUserProfileForm.getRawValue().relationWithUserProfile, this.contractorId);
    return this.kinshipRelationWithUserProfileService.addMultiple(request);
  }

  buildKinshipRelationForm(): Observable<any> {
    const request = this.parseKinshipRelations(this.kinshipRelationForm.getRawValue().relation, this.contractorId);
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
          this.contractorId
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
          this.contractorId
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

  getExistentStep(step, contractorId) {
    const request = {
      contractorId: contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep() {
    if (this.userMaterialStatus != null || this.kinshipRelationCriminalData != null || this.kinshipRelationWithUserProfileData != null || this.kinshipRelationData != null) {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractorId);
    }
    else {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractorId);
    }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId) {
    const datas = {
      isDone: success,
      stepId: this.stepId
    }
    if (stepData.length == 0) {
      this.addCandidateRegistationStep(success, stepId, contractorId);
      this.ds.sendData(datas);
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId) {
    const request = {
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }
    this.registrationFluxService.add(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId) {
    const request = {
      id: id,
      isDone: isDone,
      step: step,
      contractorId: contractorId
    }

    this.registrationFluxService.update(request).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.step-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  inputValidator(form, field) {
    return !ValidatorUtil.isInvalidPattern(form, field) && form.get(field).valid
      ? 'is-valid' : 'is-invalid';
  }

  isInvalidPattern(form, field: string): boolean {
    return ValidatorUtil.isInvalidPattern(form, field);
  }

}