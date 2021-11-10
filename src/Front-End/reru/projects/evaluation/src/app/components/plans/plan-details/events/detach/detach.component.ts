import { Component, OnInit } from '@angular/core';
import { Plan } from 'projects/evaluation/src/app/utils/models/plans/plan.model';
import { Event } from 'projects/evaluation/src/app/utils/models/events/event.model';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';

@Component({
  selector: 'app-detach',
  templateUrl: './detach.component.html',
  styleUrls: ['./detach.component.scss']
})
export class DetachComponent implements OnInit {
  eventId: number;
  planId: number;
  plan = new Plan();
  event = new Event();
  isLoading: boolean = true;

  constructor(
    private eventService: EventService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private planService: PlanService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.planId = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.planId)
      this.getPlan();

    this.eventId = +this.activatedRoute.snapshot.paramMap.get('id2');
    if (this.eventId)
      this.getEvent();
  }

  getPlan(): void {
    this.planService.get(this.planId ).subscribe((res) => {
      if (res && res.data) {
        this.plan = res.data;
        this.isLoading = false;
      }
    })
  }

  getEvent(): void {
    this.eventService.getEvent({ id: this.eventId }).subscribe((res) => {
      if (res && res.data) {
        this.event = res.data;
        this.isLoading = false;
      }
    })
  }

  detach() {
    this.planService.detachEvent({data: { planId: this.planId, eventId: this.eventId }}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Location was successfully detached', NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.location.back();
  }
}
