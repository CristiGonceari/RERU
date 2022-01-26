import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-filter-role-state',
  templateUrl: './filter-role-state.component.html',
  styleUrls: ['./filter-role-state.component.scss']
})
export class FilterRoleStateComponent {
  @Output() filter: EventEmitter<string> = new EventEmitter<string>();
  constructor() { }
}
