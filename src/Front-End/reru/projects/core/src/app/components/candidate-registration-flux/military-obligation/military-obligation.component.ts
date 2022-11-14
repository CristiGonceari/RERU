import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { MilitaryObligationService } from '../../../utils/services/military-obligation.service';
import { forkJoin, Observable, Subject } from 'rxjs';
import { ReferenceService } from '../../../utils/services/reference.service';
import { MilitaryObligationModel } from '../../../utils/models/military-obligation.model';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { DataService } from '../data.service';
import { I18nService } from '../../../utils/services/i18n.service';

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
  contractorId;

  addOrEditMilitaryObligationButton: boolean;
  isLoadingMilitaryObligation: boolean = true;
  militaryObligationData;
  registrationFluxStep;

  militaryObligationTypeEnum;

  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];

  isDone: boolean;

  title: string;
  description: string;

  constructor(private fb: FormBuilder,
    private userProfile: UserProfileService,
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
    this.getUserGeneralData();
  }

  ngOnDestroy() {
    // clear message
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

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe(res => {

      this.userGeneralData = res.data;
      this.contractorId = res.data.contractorId;

      const userData = res.data;

      if (userData.militaryObligationsCount != 0) {

        this.getMilitaryObligations(this.contractorId);
      }
      else {
        this.addOrEditMilitaryObligationButton = false;
        this.isLoadingMilitaryObligation = false;
        this.militaryObligationData = null;
      }

      this.getExistentStep(this.stepId, this.contractorId);
    })
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
      mobilizationYear: this.fb.control((militaryObligation && militaryObligation.mobilizationYear) || null, [Validators.required]),
      withdrawalYear: this.fb.control((militaryObligation && militaryObligation.withdrawalYear) || null, [Validators.required]),
      efectiv: this.fb.control((militaryObligation && militaryObligation.efectiv) || null, [Validators.required]),
      militarySpecialty: this.fb.control((militaryObligation && militaryObligation.militarySpecialty) || null, [Validators.required]),
      degree: this.fb.control((militaryObligation && militaryObligation.degree) || null, [Validators.required]),
      militaryBookletSeries: this.fb.control((militaryObligation && militaryObligation.militaryBookletSeries) || null, [Validators.required]),
      militaryBookletNumber: this.fb.control((militaryObligation && militaryObligation.militaryBookletNumber) || null, []),
      militaryBookletReleaseDay: this.fb.control((militaryObligation && militaryObligation.militaryBookletReleaseDay) || null, [Validators.required]),
      militaryBookletEminentAuthority: this.fb.control((militaryObligation && militaryObligation.militaryBookletEminentAuthority) || null, [Validators.required]),
      contractorId: this.fb.control(contractorId || null, []),
    });
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
      efectiv: data.efectiv,
      militarySpecialty: data.militarySpecialty,
      degree: data.degree,
      militaryBookletSeries: data.militaryBookletSeries,
      militaryBookletNumber: data.militaryBookletNumber,
      militaryBookletReleaseDay: data.militaryBookletReleaseDay,
      militaryBookletEminentAuthority: data.militaryBookletEminentAuthority,
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
