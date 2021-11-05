import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-badge-state',
  templateUrl: './badge-state.component.html',
  styleUrls: ['./badge-state.component.scss']
})
export class BadgeStateComponent {
  @Input() state: string;
  constructor() { }
}
