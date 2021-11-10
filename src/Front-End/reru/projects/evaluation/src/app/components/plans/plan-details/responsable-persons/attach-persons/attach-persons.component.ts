import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Plan } from 'projects/evaluation/src/app/utils/models/plans/plan.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';

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

  constructor(private location: Location, 
    private planService: PlanService, 
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(response => {this.planId = response.id; this.get()});
    this.planService.user.subscribe(x => this.userId = x);
  }

  attach(){
    this.planId = this.planId == undefined ? 0 : this.planId;

    this.planService.attachPerson({data: {userProfileId: +this.userId, planId: +this.planId}}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Location was successfully attached', NotificationUtil.getDefaultMidConfig());
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
