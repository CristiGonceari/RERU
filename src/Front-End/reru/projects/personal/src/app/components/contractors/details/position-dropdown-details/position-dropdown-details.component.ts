import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-position-dropdown-details',
  templateUrl: './position-dropdown-details.component.html',
  styleUrls: ['./position-dropdown-details.component.scss']
})
export class PositionDropdownDetailsComponent {
  @Input() index: number;
  constructor() { }
}
