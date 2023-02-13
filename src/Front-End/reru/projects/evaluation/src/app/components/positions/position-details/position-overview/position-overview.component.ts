import { Component, OnInit } from '@angular/core';
import { CandidatePositionService } from 'projects/evaluation/src/app/utils/services/candidate-position/candidate-position.service';
import { Position } from 'projects/evaluation/src/app/utils/models/positions/position.model';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalColumnEnum } from 'projects/evaluation/src/app/utils/enums/medical-column.enum';
import { EventTestTemplateService } from 'projects/evaluation/src/app/utils/services/event-test-template/event-test-template.service';
import { CandidatePositionNotificationService } from '../../../../utils/services/candidate-position-notifications/candidate-position-notification.service'
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-position-overview',
  templateUrl: './position-overview.component.html',
  styleUrls: ['./position-overview.component.scss']
})
export class PositionOverviewComponent implements OnInit {
  isLoading: boolean = true;
  position: Position = new Position();
  medicalColumn = MedicalColumnEnum;
  documnets = [];
  events = [];
  eventsListForDescription = [];
  attachedUsers = [];
  countOfUsers;
  countOfEvents;
  public Editor = DecoupledEditor;

  constructor(private positionService: CandidatePositionService,
    private route: ActivatedRoute,
    private eventTestTemplateService: EventTestTemplateService,
    private candidatePositionNotificationService: CandidatePositionNotificationService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
      if (response.id) {
        this.get(response.id);
      }
    })
  }

  get(id: number): void {
    this.positionService.get(id).subscribe(response => {
      this.position = response.data;
      this.documnets = response.data.requiredDocuments.map(e => e.label);
      this.events = response.data.events.map(e => e.label);
      this.countOfEvents = response.data.events.length;
      this.getTestTemplateByEventsIds(response.data.events);
      this.getAttachedUsers();
      this.isLoading = false;
    });
  }

  getTestTemplateByEventsIds(events) {
    this.eventTestTemplateService.getTestTemplateByEventsIds({ eventIds: events.map(x => x.value) }).subscribe(res => {
      this.eventsListForDescription = res.data;
    })
  }

  getAttachedUsers() {
    this.candidatePositionNotificationService.getNotificatedUsers(this.position.id).subscribe(res => {
      this.attachedUsers = res.data.map(e => e.label);
      this.countOfUsers = res.data.length;
    })
  }
}
