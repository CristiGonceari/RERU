import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-position-dropdown-table',
  templateUrl: './position-dropdown-table.component.html',
  styleUrls: ['./position-dropdown-table.component.scss']
})
export class PositionDropdownTableComponent {
  @Input() index: number;

  @Output() changeCurrentPosition: EventEmitter<void> = new EventEmitter<void>();
  @Output() addPreviousPosition: EventEmitter<void> = new EventEmitter<void>();
  @Output() transferToNewPosition: EventEmitter<void> = new EventEmitter<void>();
  @Output() dismiss: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
