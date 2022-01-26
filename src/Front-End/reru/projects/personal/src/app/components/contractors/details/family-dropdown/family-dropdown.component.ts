import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-family-dropdown',
  templateUrl: './family-dropdown.component.html',
  styleUrls: ['./family-dropdown.component.scss']
})
export class FamilyDropdownComponent {
  @Input() index: number;
  @Output() edit: EventEmitter<void> = new EventEmitter<void>();
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
}
