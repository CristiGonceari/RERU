import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-filter-employee-state',
  templateUrl: './filter-employee-state.component.html',
  styleUrls: ['./filter-employee-state.component.scss']
})
export class FilterEmployeeStateComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
