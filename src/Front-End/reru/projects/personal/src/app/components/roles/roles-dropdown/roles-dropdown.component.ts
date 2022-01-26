import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-roles-dropdown',
  templateUrl: './roles-dropdown.component.html',
  styleUrls: ['./roles-dropdown.component.scss']
})
export class RolesDropdownComponent {
  @Input() index: number;
  @Input() editLink: string[];
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
