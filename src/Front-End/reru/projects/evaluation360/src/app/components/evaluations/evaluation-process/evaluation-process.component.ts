import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EvaluationService } from '@utils/services';
import { EvaluationClass, EvaluationModel, NotificationUtil } from '@utils';
import { parseCounterSignModel, parseEvaluatedModel, parseEvaluation } from '../../../utils/util/parsings.util';
import { Response } from '../../../utils/models/response.model';
import { EvaluationRoleEnum } from '../../../utils/models/evaluation-role.enum';
import { ActionFormEnum, ActionFormModel } from '../../../utils/models/action-form.type';
import { EvaluationAcceptClass } from '../../../utils/models/evaluation-accept.model';
import { EvaluationCounterSignClass } from '../../../utils/models/evaluation-countersign.model';


@Component({
  templateUrl: './evaluation-process.component.html',
  styleUrls: ['./evaluation-process.component.scss']
})
export class EvaluationProcessComponent implements OnInit {
  isLoading: boolean = true;
  evaluation: EvaluationModel;
  evaluationRole: EvaluationRoleEnum;

  constructor(private readonly evaluationService: EvaluationService,
              private readonly router: Router,
              private readonly route: ActivatedRoute,
              private readonly notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.evaluationRole = this.decideRole();
    this.subscribeForParams();
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

  submit(form: ActionFormModel): void {
    this.isLoading = true;
    if ((form.action === ActionFormEnum.isSave || form.action === ActionFormEnum.isConfirm) && form.data instanceof EvaluationClass) {
      this.evaluationService[form.action === ActionFormEnum.isConfirm ? 'confirm' : 'update'](parseEvaluation(form.data)).subscribe(() => {
        this.notificationService.success('Success', 
                                        ActionFormEnum.isConfirm ? 'Fisa a fost trimisă cu success!' : 'Fisa a fost salvata cu succes!', 
                                        NotificationUtil.getDefaultMidConfig());
        if (form.action === ActionFormEnum.isConfirm) {
          this.router.navigate(['../../'], { relativeTo: this.route});
        } else {
          this.refetchEvaluation(this.evaluation.id);
        }
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
      });
    }

    if ((form.action === ActionFormEnum.isAccept || form.action === ActionFormEnum.isReject) && form.data instanceof EvaluationAcceptClass) {
      this.evaluationService[form.action === ActionFormEnum.isAccept ? 'accept' : 'reject'](parseEvaluatedModel(form.data)).subscribe(() => {
        this.notificationService.success('Success', 
                                        `Fisa a fost ${form.action === ActionFormEnum.isAccept?'acceptata':'respinsa'} cu succes!`, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
      });
    }

    if ((form.action === ActionFormEnum.isCounterSignAccept || form.action === ActionFormEnum.isCounterSignReject) && form.data instanceof EvaluationCounterSignClass) {
      this.evaluationService[form.action === ActionFormEnum.isCounterSignAccept ? 'counterSignAccept' : 'counterSignReject'](parseCounterSignModel(form.data)).subscribe(() => {
        this.notificationService.success('Success', 
                                        `Fisa a fost ${form.action === ActionFormEnum.isAccept?'contrasemnata':'respinsa'} cu succes!`, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
      });
    }

    if (form.action === ActionFormEnum.isAcknowledge) {
      this.evaluationService.acknowledge({id: form.data.id}).subscribe(() => {
        this.notificationService.success('Success', 
                                        `Evaluatul a luat cunostinta cu rezultatele finale cu succes!`, 
                                        NotificationUtil.getDefaultMidConfig());
          this.router.navigate(['../../'], { relativeTo: this.route});
      }, (error) => {
        this.isLoading = false;
        if (error.status === 400) {
          this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
          return;
        }

        this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
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
