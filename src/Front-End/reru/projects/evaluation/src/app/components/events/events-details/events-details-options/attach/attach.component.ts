import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AttachTestTypeToEventModel } from 'projects/evaluation/src/app/utils/models/events/attachTestTypeToEventModel';
import { NotificationsService } from 'angular2-notifications';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { EventTestTypeService } from 'projects/evaluation/src/app/utils/services/event-test-type/event-test-type.service';
import { AttachLocationToEventModel } from 'projects/evaluation/src/app/utils/models/locations/attachTestTypeToEventModel';
import { AttachPersonToEventModel } from 'projects/evaluation/src/app/utils/models/events/attachPersonToEventModel'

@Component({
  selector: 'app-attach',
  templateUrl: './attach.component.html',
  styleUrls: ['./attach.component.scss']
})
export class AttachComponent implements OnInit {
  
  id: number;
  eventId: number;
  name: string;
  startDate: Date;
  endDate: Date;

  isLoading: boolean = true;
  person: boolean = true;
  locations: boolean = true;
  testType: boolean = true;
  evaluator: boolean = true;
  user: boolean = true;

  url;
  attempts;
  showName = false;

  constructor(private location: Location,
    private eventService: EventService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationsService,
    private router: Router,
    private eventTestTypeService: EventTestTypeService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(response => { this.eventId = response.id; this.get() });
    this.eventService.user.subscribe(x => this.id = x);
    this.url = this.router.url.split("/")[2];

    if (this.url == "attach-person") {
      this.person = false;
    }

    if (this.url == "attach-user") {
      this.user = false;
    }

    if (this.url == "attach-location") {
      this.locations = false;
    }

    if (this.url == "attach-test-type") {
      this.testType = false;
    }

    if (this.url == "attach-evaluator") {
      this.evaluator = false;
    }
  }

  parse() {
    this.id = this.id == undefined ? 0 : this.id;

    if (this.person == false || this.user == false) {
      return {
        data: new AttachPersonToEventModel({
          eventId: +this.eventId,
          userProfileId: +this.id
        })
      };
    }

    if (this.locations == false) {
      return {
        data: new AttachLocationToEventModel({
          eventId: +this.eventId,
          locationId: +this.id
        })
      };
    }

    if (this.testType == false) {
      return {
        data: new AttachTestTypeToEventModel({
          eventId: +this.eventId,
          testTypeId: +this.id,
          maxAttempts: this.attempts
        })
      };
    }

    if (this.evaluator == false) {
      return {
        data: {
          evaluatorId: this.id,
          eventId: +this.eventId,
          showUserName: this.showName
        }
      };
    }
  }

  attach() {
    if (this.person == false) {
      this.eventService.attachPerson(this.parse()).subscribe(() => {
        this.backClicked();
        this.notificationService.success('Success', 'User was successfully attached', NotificationUtil.getDefaultMidConfig());
      });
    }
    if (this.locations == false) {
      this.eventService.attachLocation(this.parse()).subscribe(() => {
        this.backClicked();
        this.notificationService.success('Success', 'Location was successfully attached', NotificationUtil.getDefaultMidConfig());
      });
    }
    if (this.testType == false) {
      this.eventTestTypeService.attachTestType(this.parse()).subscribe(() => {
        this.backClicked();
        this.notificationService.success('Success', 'Test type was successfully attached', NotificationUtil.getDefaultMidConfig());
      });
    }
    if (this.evaluator == false) {
      this.eventService.attachEvaluator(this.parse()).subscribe(() => {
        this.backClicked();
        this.notificationService.success('Success', 'Evaluator was successfully attached', NotificationUtil.getDefaultMidConfig());
      });
    }
    if (this.user == false) {
      this.eventService.attachUser(this.parse()).subscribe(() => {
        this.backClicked();
        this.notificationService.success('Success', 'User was successfully attached', NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  get() {
    this.eventService.getEvent(this.eventId).subscribe(
      res => {
        if (res && res.data) {
          this.name = res.data.name;
          this.endDate = res.data.tillDate;
          this.startDate = res.data.fromDate;
          this.isLoading = false;
        }
      }
    )
  }

  backClicked() {
    this.location.back();
  }

  onItemChange(event) {
    this.showName = event.target.checked;
  }
}
