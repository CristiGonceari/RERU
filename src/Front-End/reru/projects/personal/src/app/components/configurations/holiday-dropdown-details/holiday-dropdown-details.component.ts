import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-holiday-dropdown-details',
  templateUrl: './holiday-dropdown-details.component.html',
  styleUrls: ['./holiday-dropdown-details.component.scss']
})
export class HolidayDropdownDetailsComponent{
  @Input() index: number;
  @Output() edit: EventEmitter<void> = new EventEmitter<void>();
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }

}
