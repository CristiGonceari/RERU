import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-filter-user-state',
  templateUrl: './filter-user-state.component.html',
  styleUrls: ['./filter-user-state.component.scss']
})
export class FilterUserStateComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

}
