import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-personal-dropdown-details',
  templateUrl: './personal-dropdown-details.component.html',
  styleUrls: ['./personal-dropdown-details.component.scss']
})
export class PersonalDropdownDetailsComponent{
  @Input() index: number;

  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  @Output() addContact: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
