import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { RegistrationFluxStepEnum } from '../../../utils/models/registrationFluxStep.enum';
import { I18nService } from '../../../utils/services/i18n.service';
import { ProfileService } from '../../../utils/services/profile.service';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DataService } from '../data.service';

@Component({
  selector: 'app-declaration',
  templateUrl: './declaration.component.html',
  styleUrls: ['./declaration.component.scss']
})
export class DeclarationComponent implements OnInit {

  @Output() counterChange = new EventEmitter<boolean>();

  isChecked: boolean = false;

  userId;
  stepId;
  contractorId;

  userRegistrationFluxStepData;
  registrationFluxStep;
  registrationFluxSteps: [];

  unfinishedSteps;
  allowToFinish: boolean;

  title: string;
  description: string;

  constructor(
    private route: ActivatedRoute,
    private registrationFluxService: RegistrationFluxStepService,
    private notificationService: NotificationsService,
    private router: Router,
    private ds: DataService,
    private profileService: ProfileService,
    private userProfile: UserProfileService,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);
    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop())

    this.getUnfinishedSteps();
    this.getUserGeneralData();
  }

  ngOnDestroy() {
    // clear message
    this.ds.clearData();
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe(res => {

      this.contractorId = res.data.contractorId;

      this.getExistentStep(this.stepId, this.contractorId)

    })
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

  getUnfinishedSteps() {
    this.profileService.getCandidateRegistrationSteps().subscribe(res => {
      this.unfinishedSteps = res.data.unfinishedSteps;

      if (this.unfinishedSteps.length <= 1 && this.unfinishedSteps[0] == this.stepId) {
        this.allowToFinish = true;
      } else {
        this.allowToFinish = false;
      }

    });
  }

  addRegistrationFluxStep() {
    if (this.isChecked) {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractorId);

      this.counterChange.emit(this.isChecked == true);
    } else {
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
      this.router.navigate(["../../../../"], { relativeTo: this.route }).then(() => { window.location.reload() });
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId);
      this.ds.sendData(datas);
      this.router.navigate(["../../../../"], { relativeTo: this.route }).then(() => { window.location.reload() });
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
}
