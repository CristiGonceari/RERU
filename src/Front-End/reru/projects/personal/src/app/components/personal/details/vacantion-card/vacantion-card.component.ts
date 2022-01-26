import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { VacantionModel } from 'projects/personal/src/app/utils/models/vacantion.model';
import { VacationTypeEnum } from 'projects/personal/src/app/utils/models/vacation-type.enum';

@Component({
  selector: 'app-vacantion-card',
  templateUrl: './vacantion-card.component.html',
  styleUrls: ['./vacantion-card.component.scss']
})
export class VacantionCardComponent{

  @Input() contractorId: number;
  @Input() vacantion: VacantionModel;
  vacantionTypes = VacationTypeEnum;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();
  
  constructor() { }
}
