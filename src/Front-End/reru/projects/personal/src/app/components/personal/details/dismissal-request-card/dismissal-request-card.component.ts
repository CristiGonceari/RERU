import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RequestProfileModel } from 'projects/personal/src/app/utils/models/request-profile.model';

@Component({
  selector: 'app-dismissal-request-card',
  templateUrl: './dismissal-request-card.component.html',
  styleUrls: ['./dismissal-request-card.component.scss']
})
export class DismissalRequestCardComponent{
  
  @Input() contractorId: number;
  @Input() request: RequestProfileModel;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }
}
