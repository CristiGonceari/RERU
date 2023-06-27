import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { MilitaryObligationService } from 'projects/personal/src/app/utils/services/military-obligation.service';
import { Observable, Subject, forkJoin } from 'rxjs';
import { DataService } from '../data.service';
import { MilitaryObligationModel } from 'projects/personal/src/app/utils/models/military-obligation.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { MilitaryObligationTypeEnum } from '../../../../utils/models/military-obligation-type.enum';
import { I18nService } from '../../../../utils/services/i18n.service';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';

@Component({
  selector: 'app-ranks',
  templateUrl: './ranks.component.html',
  styleUrls: ['./ranks.component.scss']
})
export class RanksComponent implements OnInit {
  @Input() contractor: Contractor;
  militaryObligationForm: FormGroup;
  userGeneralData;
  userId;
  stepId;
  contractorId;

  addOrEditMilitaryObligationButton: boolean;
  isLoadingMilitaryObligation: boolean = false;
  militaryObligationData;
  registrationFluxStep;

  militaryObligationTypeEnum: MilitaryObligationTypeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];

  isDone: boolean;

  title: string;
  description: string;

  isLoadingMilitaryObligationContent:boolean = false;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private militaryObligationService: MilitaryObligationService,
    private referenceService: ReferenceService,
    private notificationService: NotificationsService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);
    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.retrieveDropdowns();
  }

  ngOnDestroy() {
    this.ds.clearData();
  }

  initForm(data?): void {
    if (data == null) {
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations()])
      });
    }
    else {
      this.militaryObligationForm = this.fb.group({
        obligations: this.fb.array([this.generateMilitaryObligations(data, this.contractorId)])
      });
    }
  }

  retrieveDropdowns(): void {
    this.referenceService.getMilitaryObligationTypeEnum().subscribe(res => {
      this.militaryObligationTypeEnum = res.data;
    });
  }

  enableNameAndAdressField(value) {
    if (value.militaryObligationType) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.AlternativeService || value.militaryObligationType == MilitaryObligationTypeEnum.MilitaryChair) {
        return true
      }
    }
    return false;
  }

  enableDegreeAndMilitarySpecialtyAndEfectiveField(value) {
    if (value.militaryObligationType) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.PerformedMilitaryService || value.militaryObligationType == MilitaryObligationTypeEnum.MilitaryChair) {
        return true
      }
    }
    return false;
  }

  enablePeriodField(value) {
    if (value.militaryObligationType) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.AlternativeService || value.militaryObligationType == MilitaryObligationTypeEnum.MilitaryChair) {
        return true
      }
    }
    return false;
  }

  enableYearsField(value) {
    if (value.militaryObligationType) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.PerformedMilitaryService) {
        return true
      }
    }
    return false;
  }

  enableMilitaryBookletField(value) {
    if (parseInt(value.militaryObligationType)) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.Disobedient) {
        return false
      }
    }else{
      return false;
    }
    return true;
  }

  enableMilitaryBookletNameField(value) {
    if (parseInt(value.militaryObligationType)) {
      if (value.militaryObligationType == MilitaryObligationTypeEnum.Recruit) {
        return true
      }
    }
    return false;
  }

  inputValidator(form, field) {
    return !ValidatorUtil.isInvalidPattern(form, field) && form.get(field).valid ? 'is-valid' : 'is-invalid';
  }

  isInvalidPattern(form, field: string): boolean {
    return ValidatorUtil.isInvalidPattern(form, field);
  }

  getMilitaryObligations(contractorId) {
    this.isLoadingMilitaryObligation = true;

    const request = {
      contractorId: contractorId
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
        } else {
          this.initForm(obligation[i])
        }
      }
    }
  }

  generateMilitaryObligations(militaryObligation?, contractorId?) {
    return this.fb.group({
      id: this.fb.control((militaryObligation && militaryObligation.id) || null, []),
      militaryObligationType: this.fb.control((militaryObligation && militaryObligation.militaryObligationType) || null, [Validators.required]),
      institutionName: this.fb.control((militaryObligation && militaryObligation.institutionName) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      institutionAdress: this.fb.control((militaryObligation && militaryObligation.institutionAdress) || null, [Validators.required]),
      mobilizationYear: this.fb.control((militaryObligation && militaryObligation.mobilizationYear) || null, [Validators.required]),
      withdrawalYear: this.fb.control((militaryObligation && militaryObligation.withdrawalYear) || null, [Validators.required]),
      efectiv: this.fb.control((militaryObligation && militaryObligation.efectiv) || null, [Validators.required]),
      militarySpecialty: this.fb.control((militaryObligation && militaryObligation.militarySpecialty) || null, [Validators.required]),
      degree: this.fb.control((militaryObligation && militaryObligation.degree) || null, [Validators.required]),
      militaryBookletSeries: this.fb.control((militaryObligation && militaryObligation.militaryBookletSeries) || null, [Validators.required]),
      militaryBookletNumber: this.fb.control((militaryObligation && militaryObligation.militaryBookletNumber) || null, [Validators.required, , Validators.pattern(/^[0-9]+(\.?[0-9]+)?$/)]),
      militaryBookletReleaseDay: this.fb.control((militaryObligation && militaryObligation.militaryBookletReleaseDay) || null, [Validators.required]),
      startObligationPeriod: this.fb.control((militaryObligation && militaryObligation.startObligationPeriod) || null, [Validators.required]),
      endObligationPeriod: this.fb.control((militaryObligation && militaryObligation.endObligationPeriod) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, []),
    });
  }

  studyTypeFieldsValidator(index) {
    this.isLoadingMilitaryObligationContent =true;
    const obligations = this.militaryObligationForm.controls.obligations as FormArray;
    let obligation = obligations.controls[index];
    const obligationType = parseInt(obligation.get("militaryObligationType").value);


    if (obligationType) {
      if (obligationType == MilitaryObligationTypeEnum.Recruit) {
        obligation.get("degree").setValue(null);
        obligation.get("efectiv").setValue(null);
        obligation.get("endObligationPeriod").setValue(null);
        obligation.get("institutionAdress").setValue(null);
        obligation.get("institutionName").setValue(null);
        obligation.get("militarySpecialty").setValue(null);
        obligation.get("mobilizationYear").setValue(null);
        obligation.get("startObligationPeriod").setValue(null);
        obligation.get("withdrawalYear").setValue(null);
      } else if (obligationType == MilitaryObligationTypeEnum.Disobedient){
        obligation.get("degree").setValue(null);
        obligation.get("efectiv").setValue(null);
        obligation.get("endObligationPeriod").setValue(null);
        obligation.get("institutionAdress").setValue(null);
        obligation.get("institutionName").setValue(null);
        obligation.get("militaryBookletNumber").setValue(null);
        obligation.get("militaryBookletReleaseDay").setValue(null);
        obligation.get("militaryBookletSeries").setValue(null);
        obligation.get("militarySpecialty").setValue(null);
        obligation.get("mobilizationYear").setValue(null);
        obligation.get("startObligationPeriod").setValue(null);
        obligation.get("withdrawalYear").setValue(null);
      } else if (obligationType == MilitaryObligationTypeEnum.PerformedMilitaryService){
        obligation.get("endObligationPeriod").setValue(null);
        obligation.get("institutionAdress").setValue(null);
        obligation.get("institutionName").setValue(null);
        obligation.get("startObligationPeriod").setValue(null);
      } else if (obligationType == MilitaryObligationTypeEnum.AlternativeService){
        obligation.get("degree").setValue(null);
        obligation.get("efectiv").setValue(null);
        obligation.get("militarySpecialty").setValue(null);
        obligation.get("mobilizationYear").setValue(null);
        obligation.get("withdrawalYear").setValue(null);
      } else if (obligationType == MilitaryObligationTypeEnum.MilitaryChair){
        obligation.get("degree").setValue(null);
        obligation.get("efectiv").setValue(null);
        obligation.get("mobilizationYear").setValue(null);
        obligation.get("withdrawalYear").setValue(null);
      }
    }

    setTimeout(() => {
    this.isLoadingMilitaryObligationContent =false;
    }, 100)
  }

  militaryObligationsButtonValidator(military) {
    let results: boolean[] = [];
    
    for (let i = 0; i < military.length; i++) {
      if (parseInt(military[i].value.militaryObligationType)) {
        if (military[i].value.militaryObligationType == MilitaryObligationTypeEnum.Recruit) {
          results.push(
            !(military[i].value.militaryObligationType &&
              military[i].value.militaryBookletSeries &&
              military[i].value.militaryBookletNumber &&
              military[i].value.militaryBookletReleaseDay))
        } else if(military[i].value.militaryObligationType == MilitaryObligationTypeEnum.Disobedient){
          results.push(
            !(military[i].value.militaryObligationType))
        } else if(military[i].value.militaryObligationType == MilitaryObligationTypeEnum.PerformedMilitaryService){
          results.push(
            !(military[i].value.militaryObligationType && 
              military[i].value.mobilizationYear &&
              military[i].value.withdrawalYear &&
              military[i].value.efectiv &&
              military[i].value.militarySpecialty &&
              military[i].value.degree &&
              military[i].value.militaryBookletSeries &&
              military[i].value.militaryBookletNumber &&
              military[i].value.militaryBookletReleaseDay 
              ))
        } else if(military[i].value.militaryObligationType == MilitaryObligationTypeEnum.AlternativeService){
          results.push(
            !(military[i].value.militaryObligationType && 
              military[i].value.institutionName &&
              military[i].value.institutionAdress &&
              military[i].value.startObligationPeriod &&
              military[i].value.endObligationPeriod &&
              military[i].value.militaryBookletSeries &&
              military[i].value.militaryBookletNumber &&
              military[i].value.militaryBookletReleaseDay 
              ))
        }else if(military[i].value.militaryObligationType == MilitaryObligationTypeEnum.MilitaryChair){
          results.push(
            !(military[i].value.militaryObligationType && 
              military[i].value.institutionName &&
              military[i].value.institutionAdress &&
              military[i].value.startObligationPeriod &&
              military[i].value.endObligationPeriod &&
              military[i].value.efectiv &&
              military[i].value.militarySpecialty &&
              military[i].value.degree &&
              military[i].value.militaryBookletSeries &&
              military[i].value.militaryBookletNumber &&
              military[i].value.militaryBookletReleaseDay 
              ))
        }
      }else{
        results.push(true)
      }
    }
    return results.some((x) => x == true) ? true : false;
  }

  createMilitaryObligations() {
    this.buildMilitaryObligationForm().subscribe(response => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-military-obligation-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getMilitaryObligations(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-military-obligation-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  buildMilitaryObligationForm(): Observable<any> {
    const request = this.parseMilitaryObligations(this.militaryObligationForm.getRawValue().obligations, this.contractorId);
    return this.militaryObligationService.addMultiple(request);
  }

  parseMilitaryObligations(data: MilitaryObligationModel[], contractorId: number): MilitaryObligationModel[] {
    return data.map(el => this.parseMilitaryObligation(el, contractorId));
  }

  parseMilitaryObligation(data: MilitaryObligationModel, contractorId): MilitaryObligationModel {
    return ObjectUtil.preParseObject({
      id: data.id,
      militaryObligationType: parseInt(data.militaryObligationType),
      mobilizationYear: data.mobilizationYear,
      withdrawalYear: data.withdrawalYear,
      startObligationPeriod: data.startObligationPeriod,
      endObligationPeriod: data.endObligationPeriod,
      efectiv: data.efectiv,
      militarySpecialty: data.militarySpecialty,
      degree: data.degree,
      militaryBookletSeries: data.militaryBookletSeries,
      militaryBookletNumber: data.militaryBookletNumber,
      militaryBookletReleaseDay: data.militaryBookletReleaseDay,
      militaryBookletEminentAuthority: data.militaryBookletEminentAuthority,
      institutionName: data.institutionName,
      institutionAdress: data.institutionAdress,
      contractorId: contractorId
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
          this.contractorId
        )
      );
    }
  }

  removeMilitaryObligation(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);

    (<FormArray>this.militaryObligationForm.controls.obligations).controls.splice(index, 1);
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
    if (this.militaryObligationData != null) {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractorId);
    }
    else {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractorId, true);
    }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId, inProgress?) {
    const datas = {
      isDone: success,
      stepId: this.stepId,
      inProgress: inProgress
    }
    if (stepData.length == 0) {
      this.addCandidateRegistationStep(success, stepId, contractorId, inProgress);
      this.ds.sendData(datas);
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId, inProgress);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId, inProgress?) {
    const request = {
      isDone: isDone,
      step: step,
      contractorId: contractorId,
      inProgress: inProgress
    }
    this.registrationFluxService.add(request).subscribe(res => {
      if (!inProgress) {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('candidate-registration-flux.step-success'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      } else {
        forkJoin([
          this.translate.get('step-status.in-progress'),
          this.translate.get('candidate-registration-flux.step-in-progress'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      }
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

  updateCandidateRegistationStep(id, isDone, step, contractorId, inProgress?) {
    const request = {
      id: id,
      isDone: isDone,
      step: step,
      contractorId: contractorId,
      inProgress: inProgress
    }

    this.registrationFluxService.update(request).subscribe(res => {
      if (!inProgress) {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('candidate-registration-flux.step-success'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      } else {
        forkJoin([
          this.translate.get('step-status.in-progress'),
          this.translate.get('candidate-registration-flux.step-in-progress'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      }
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
}
