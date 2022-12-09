import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EvaluationService } from '@utils/services';
import { EvaluationModel, NotificationUtil } from '@utils';
import { parseEvaluation } from '../../../utils/util/parsings.util';
import { Response } from '../../../utils/models/response.model';
import { EvaluationRoleEnum } from '../../../utils/models/evaluation-role.enum';

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

  submit(isConfirm: boolean = false): void {
    this.evaluationService[isConfirm ? 'confirm' : 'update'](parseEvaluation({} as any)).subscribe(() => {
      this.notificationService.success('Success', 
                                        isConfirm? 'Fisa a fost trimisă cu success!' : 'Fisa a fost salvata cu succes!', 
                                       NotificationUtil.getDefaultMidConfig());
      if (isConfirm) {
        this.router.navigate(['../../'], { relativeTo: this.route});
      }
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Eroare de validare', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  private decideRole(): EvaluationRoleEnum {
    switch(true) {
      case this.router.url.includes('evaluate'): return EvaluationRoleEnum.Evaluator;
      case this.router.url.includes('accept'): return EvaluationRoleEnum.Evaluated;
      case this.router.url.includes('countersign'): return EvaluationRoleEnum.CounterSigner;
      case this.router.url.includes('getknow'): return EvaluationRoleEnum.EvaluatedKnow;
    }
  }
}
