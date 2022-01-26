import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RequestProfileModel } from '../../../utils/models/request-profile.model';
import { VacationStateEnum } from '../../../utils/models/vacation-state.enum';

@Component({
  selector: 'app-subordinate-requests-card',
  templateUrl: './subordinate-requests-card.component.html',
  styleUrls: ['./subordinate-requests-card.component.scss']
})
export class SubordinateRequestsCardComponent {
  @Input() request: RequestProfileModel;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();
  @Output() process: EventEmitter<any> = new EventEmitter<any>();
  vacationStates = VacationStateEnum;
}
