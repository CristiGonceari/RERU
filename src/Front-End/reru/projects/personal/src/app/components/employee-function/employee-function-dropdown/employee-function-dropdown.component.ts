import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-employee-function-dropdown',
  templateUrl: './employee-function-dropdown.component.html',
  styleUrls: ['./employee-function-dropdown.component.scss']
})
export class EmployeeFunctionDropdownComponent {
  @Input() index: number;
  @Input() editLink: string[];
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
