import { Component, Input, OnInit } from '@angular/core';
import { IN_SERVICE, DISMISSED } from '../../constants/employer-state.constant';
import { Contractor } from '../../models/contractor.model';

@Component({
  selector: 'app-last-employee-function-label',
  templateUrl: './last-employee-function-label.component.html',
  styleUrls: ['./last-employee-function-label.component.scss']
})
export class LastEmployeeFunctionLabelComponent{
  @Input() item: Contractor;
  employerState = { IN_SERVICE, DISMISSED };
  constructor() {  }
}
