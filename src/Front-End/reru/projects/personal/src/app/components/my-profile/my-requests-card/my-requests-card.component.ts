import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RequestProfileModel } from '../../../utils/models/request-profile.model';

@Component({
  selector: 'app-my-requests-card',
  templateUrl: './my-requests-card.component.html',
  styleUrls: ['./my-requests-card.component.scss']
})
export class MyRequestsCardComponent {
  @Input() request: RequestProfileModel;
  @Output() download: EventEmitter<number> = new EventEmitter<number>();
  constructor() {}
}
