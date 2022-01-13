import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Plan } from 'projects/evaluation/src/app/utils/models/plans/plan.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-attach-persons',
  templateUrl: './attach-persons.component.html',
  styleUrls: ['./attach-persons.component.scss']
})
export class AttachPersonsComponent implements OnInit {
  planId: number;
  userId: number;
  plan: Plan = new Plan();
  isLoading: boolean = true;

  title: string;
  description: string;

  constructor(private location: Location, 
    private planService: PlanService, 
		public translate: I18nService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(response => {this.planId = response.id; this.get()});
    this.planService.user.subscribe(x => this.userId = x);
  }

  attach(){
    this.planId = this.planId == undefined ? 0 : this.planId;

    this.planService.attachPerson({data: {userProfileId: +this.userId, planId: +this.planId}}).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('plans.succes-attach-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  get(){
    this.planService.get(this.planId).subscribe(
      res => {
        if (res && res.data) {
          this.plan = res.data;
          this.isLoading = false;
        }
      }
    )
  }

  backClicked() {
		this.location.back();
	}
}
