import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-languages-dropdown-details',
  templateUrl: './languages-dropdown-details.component.html',
  styleUrls: ['./languages-dropdown-details.component.scss']
})
export class LanguagesDropdownDetailsComponent {
  @Input() index: number;
  @Input() editLink: string[];
  @Input() paramId: number;
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
