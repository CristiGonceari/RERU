import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-organigram-dropdown-details',
  templateUrl: './organigram-dropdown-details.component.html',
  styleUrls: ['./organigram-dropdown-details.component.scss']
})
export class OrganigramDropdownDetailsComponent  {
  @Input() viewLink: string[];
  @Input() editLink: string[];
  @Input() index: number;

  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
