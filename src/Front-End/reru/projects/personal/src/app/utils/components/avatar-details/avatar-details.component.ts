import { Component, Input } from '@angular/core';
import { Contractor } from '../../models/contractor.model';

@Component({
  selector: 'app-avatar-details',
  templateUrl: './avatar-details.component.html',
  styleUrls: ['./avatar-details.component.scss']
})
export class AvatarDetailsComponent {
  @Input() contractor: Contractor;

  renderText(user): string {
    if (!user || !user.firstName || !user.lastName) {
      return 'XX';
    }

    return `${user.firstName[0].toUpperCase()}${user.lastName[0].toUpperCase()}`;
  }
}
