import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EvaluationService, I18nService } from '@utils/services';
import { EvaluationClass, EvaluationModel, NotificationUtil } from '@utils';
import { parseCounterSignModel, parseEvaluatedModel, parseEvaluation } from '../../../utils/util/parsings.util';
import { Response } from '../../../utils/models/response.model';
import { EvaluationRoleEnum } from '../../../utils/models/evaluation-role.enum';
import { ActionFormEnum, ActionFormModel } from '../../../utils/models/action-form.type';
import { EvaluationAcceptClass } from '../../../utils/models/evaluation-accept.model';
import { EvaluationCounterSignClass } from '../../../utils/models/evaluation-countersign.model';
import { forkJoin } from 'rxjs';


@Component({
  templateUrl: './evaluation-process.component.html',
  styleUrls: ['./evaluation-process.component.scss']
})
export class EvaluationProcessComponent implements OnInit {
  isLoading: boolean = true;
  evaluation: EvaluationModel;
  evaluationRole: EvaluationRoleEnum;
  notification = {
    success: 'Success', 
    warning: "Warning",
    error: 'Error',
    evWas: "Prof. evaluation was",
    sent: "sent",
    saved: "saved",
    accepted: "accepted",
    rejected: "rejected",
    countersigned: "countersigned",
    withSucces: "successfully!",
    validationError: "Validation error!",
    serverError: "Server error occured!",
    evaluatedKnown: "The evaluated got acquainted with the final results successfully!",
  };

  constructor(private readonly evaluationService: EvaluationService,
              private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notificationService: NotificationsService,
              public translate: I18nService) { }

  ngOnInit(): void {
    this.evaluationRole = this.decideRole();
    this.subscribeForParams();
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  subscribeForParams(): void {
    this.route.params.subscribe((params: Params) => {
      if (params.id) {
        this.evaluationService.get(params.id).subscribe((response: Response<EvaluationModel>) => {
          this.evaluation = response.data;
          this.isLoading = false;
        }, error => {
          this.isLoading = false;
          this.router.navigate(['../../evaluation'])
        });
      } else {
        this.router.navigate(['../../evaluation'])
      }
    })
  }

  refetchEvaluation(id: number): void {
    this.evaluationService.get(id).subscribe((response: Response<EvaluationModel>) => {
      this.evaluation = response.data;
      this.isLoading = false;
    })
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.title.success'),
      this.translate.get('notification.title.warning'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.body.success.ev-was'),
      this.translate.get('notification.body.success.sent'),
      this.translate.get('notification.body.success.saved'),
      this.translate.get('notification.body.success.accepted'),
      this.translate.get('notification.body.success.rejected'),
      this.translate.get('notification.body.success.countersigned'),
      this.translate.get('notification.body.success.with-succes'),
      this.translate.get('notification.body.validation-error'),
      this.translate.get('notification.body.server-error'),
      this.translate.get('notification.body.success.evaluated-known')
    ]).subscribe(([success, warning, error, evWas, sent, saved, accepted, rejected,
                   countersigned, withSucces, validationError, serverError, evaluatedKnown]) => {
      this.notification.success = success;
      this.notification.warning = warning;
      this.notification.error = error;
      this.notification.evWas = evWas;//
      this.notification.sent = sent;//
      this.notification.saved = saved;//
      this.notification.accepted = accepted;
      this.notification.rejected = rejected;
      this.notification.countersigned = countersigned;
      this.notification.withSucces = withSucces;//
      this.notification.validationError = validationError;
      this.notification.serverError = serverError;
      this.notification.evaluatedKnown = evaluatedKnown;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  submit(form: ActionFormModel): void {
    this.isLoading = true;
    if ((form.action === ActionFormEnum.isSave || form.action === ActionFormEnum.isConfirm) && form.data instanceof EvaluationClass) {
      this.evaluationService[form.action === ActionFormEnum.isConfirm ? 'confirm' : 'update'](this.evaluation.id, parseEvaluation(form.data)).subscribe(() => {
        this.notificationService.success(this.notification.success, 
                                        `${this.notification.evWas} ${form.action === ActionFormEnum.isConfirm? this.notification.sent : this.notification.saved} ${this.notification.withSucces}`,
                                        NotificationUtil.getDefaultMidConfig());
        if (form.action === ActionFormEnum.isConfirm) {
          this.router.navigate(['../../'], { relativeTo: this.route});
        } else {
          this.refetchEvaluation(this.evaluation.id);
        }
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn(this.notification.warning , this.notification.validationError, NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
      });
    }

    if ((form.action === ActionFormEnum.isAccept || form.action === ActionFormEnum.isReject) && form.data instanceof EvaluationAcceptClass) {
      this.evaluationService[form.action === ActionFormEnum.isAccept ? 'accept' : 'reject'](this.evaluation.id, parseEvaluatedModel(form.data)).subscribe(() => {
        this.notificationService.success(this.notification.success, 
                                        `${this.notification.evWas} ${form.action === ActionFormEnum.isAccept? this.notification.accepted : this.notification.rejected} ${this.notification.withSucces}`, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn(this.notification.warning , this.notification.validationError, NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
      });
    }

    if ((form.action === ActionFormEnum.isCounterSignAccept || form.action === ActionFormEnum.isCounterSignReject) && form.data instanceof EvaluationCounterSignClass) {
      this.evaluationService[form.action === ActionFormEnum.isCounterSignAccept ? 'counterSignAccept' : 'counterSignReject'](this.evaluation.id, parseCounterSignModel(form.data)).subscribe(() => {
        this.notificationService.success(this.notification.success, 
                                        `${this.notification.evWas} ${form.action === ActionFormEnum.isCounterSignAccept? this.notification.countersigned : this.notification.rejected} ${this.notification.withSucces}`, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn(this.notification.warning , this.notification.validationError, NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
      });
    }

    if (form.action === ActionFormEnum.isAcknowledge) {
      this.evaluationService.acknowledge({id: this.evaluation.id}).subscribe(() => {
        this.notificationService.success(this.notification.success, 
                                        this.notification.evaluatedKnown, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn(this.notification.warning , this.notification.validationError, NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error(this.notification.error , this.notification.serverError, NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  private decideRole(): EvaluationRoleEnum {
    switch(true) {
      case this.router.url.includes('evaluate'): return EvaluationRoleEnum.Evaluator;
      case this.router.url.includes('accept'): return EvaluationRoleEnum.Evaluated;
      case this.router.url.includes('countersign'): return EvaluationRoleEnum.CounterSigner;
      case this.router.url.includes('acknowledge'): return EvaluationRoleEnum.EvaluatedKnow;
    }
  }
}
