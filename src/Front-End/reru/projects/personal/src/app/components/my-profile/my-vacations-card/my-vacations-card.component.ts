import { Component, EventEmitter, Input, Output } from '@angular/core';
import { VacationProfileModel } from '../../../utils/models/vacation-profile.model';
import { VacationTypeEnum } from '../../../utils/models/vacation-type.enum';

@Component({
  selector: 'app-my-vacations-card',
  templateUrl: './my-vacations-card.component.html',
  styleUrls: ['./my-vacations-card.component.scss']
})
export class MyVacationsCardComponent {
  @Input() vacation: VacationProfileModel;
  vacationTypes = VacationTypeEnum;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();
  constructor() {}
}
