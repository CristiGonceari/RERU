import { Component, Input } from '@angular/core';
import { Contractor } from '../../../utils/models/contractor.model';

@Component({
  selector: 'app-list-dropdown',
  templateUrl: './list-dropdown.component.html',
  styleUrls: ['./list-dropdown.component.scss']
})
export class ListDropdownComponent {
  @Input() index: number;
  @Input() contractor: Contractor;
  constructor() { }

  renderText(user): string {
    if (!user || !user.firstName || !user.lastName) {
      return 'XX';
    }

    return `${user.firstName[0].toUpperCase()}${user.lastName[0].toUpperCase()}`;
  }
}
