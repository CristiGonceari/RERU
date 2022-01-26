import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-vacation-dropdown-details',
  templateUrl: './vacation-dropdown-details.component.html',
  styleUrls: ['./vacation-dropdown-details.component.scss']
})
export class VacationDropdownDetailsComponent  {
  @Input() index: number;
  @Input() editLink: string[];
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
