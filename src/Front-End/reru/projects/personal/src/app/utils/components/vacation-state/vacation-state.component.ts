import { Component, Input } from '@angular/core';
import { VacationStateEnum } from '../../../utils/models/vacation-state.enum';

@Component({
  selector: 'app-vacation-state',
  templateUrl: './vacation-state.component.html',
  styleUrls: ['./vacation-state.component.scss']
})
export class VacationStateComponent{
  @Input() state: VacationStateEnum;
  vacationStates = VacationStateEnum;
}
