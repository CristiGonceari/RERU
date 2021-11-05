import { Component, Input } from '@angular/core';
import { IN_SERVICE, DISMISSED } from '../../../utils/constants/employer-state.constant';
import { Contractor } from '../../../utils/models/contractor.model';

@Component({
  selector: 'app-last-department-label',
  templateUrl: './last-department-label.component.html',
  styleUrls: ['./last-department-label.component.scss']
})
export class LastDepartmentLabelComponent {
  @Input() item: Contractor;
  employerState = { IN_SERVICE, DISMISSED };
  constructor() { }
}
