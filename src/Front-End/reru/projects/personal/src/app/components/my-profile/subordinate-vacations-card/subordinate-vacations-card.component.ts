import { Component, EventEmitter, Input, Output } from '@angular/core';
import { VacationProfileModel } from '../../../utils/models/vacation-profile.model';
import { VacationStateEnum } from '../../../utils/models/vacation-state.enum';
import { VacationTypeEnum } from '../../../utils/models/vacation-type.enum';

@Component({
  selector: 'app-subordinate-vacations-card',
  templateUrl: './subordinate-vacations-card.component.html',
  styleUrls: ['./subordinate-vacations-card.component.scss']
})
export class SubordinateVacationsCardComponent {
  @Input() vacation: VacationProfileModel;
  vacationStates = VacationStateEnum;
  vacationTypes = VacationTypeEnum;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();
  @Output() process: EventEmitter<any> = new EventEmitter<any>();
  constructor() {}
}
