import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-view-contact-dropdown',
  templateUrl: './view-contact-dropdown.component.html',
  styleUrls: ['./view-contact-dropdown.component.scss']
})
export class ViewContactDropdownComponent {
  @Input() index;

  @Output() edit: EventEmitter<void> = new EventEmitter<void>();
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
