import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-faq-dropdown-details',
  templateUrl: './faq-dropdown-details.component.html',
  styleUrls: ['./faq-dropdown-details.component.scss']
})
export class FaqDropdownDetailsComponent {
  index: number;
  @Input() editLink: string[];
  @Input() viewLink: string[];
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
