import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-department-dropdown-details',
  templateUrl: './department-dropdown-details.component.html',
  styleUrls: ['./department-dropdown-details.component.scss']
})
export class DepartmentDropdownDetailsComponent {
  index: number;
  @Input() editLink: string[];
  @Input() viewLink: string[];
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
