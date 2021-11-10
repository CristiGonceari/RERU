import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Plan } from 'projects/evaluation/src/app/utils/models/plans/plan.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';

@Component({
  selector: 'app-attach',
  templateUrl: './attach.component.html',
  styleUrls: ['./attach.component.scss']
})
export class AttachComponent implements OnInit {
  planId: number;
  eventId: number;
  plan: Plan = new Plan();
  isLoading: boolean = true;

  constructor(private location: Location, 
    private planService: PlanService, 
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(response => {this.planId = response.id; this.get()});
    this.planService.event.subscribe(x => this.eventId = x);
  }

  attach(){
    this.planId = this.planId == undefined ? 0 : this.planId;

    this.planService.attachEvent({data: {eventId: +this.eventId, planId: +this.planId}}).subscribe(() => {
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
