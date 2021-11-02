import { Component, Input } from '@angular/core';
import { IN_SERVICE, DISMISSED } from '../../../utils/constants/employer-state.constant';
import { Contractor } from '../../../utils/models/contractor.model';

@Component({
  selector: 'app-last-organization-role-label',
  templateUrl: './last-organization-role-label.component.html',
  styleUrls: ['./last-organization-role-label.component.scss']
})
export class LastOrganizationRoleLabelComponent {
  @Input() item: Contractor;
  employerState = { IN_SERVICE, DISMISSED };
  constructor() { }
}
