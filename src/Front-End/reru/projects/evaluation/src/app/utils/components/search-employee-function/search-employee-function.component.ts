import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from '../../services/reference/reference.service';

@Component({
  selector: 'app-search-employee-function',
  templateUrl: './search-employee-function.component.html',
  styleUrls: ['./search-employee-function.component.scss']
})
export class SearchEmployeeFunctionComponent  {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  function: string = '';
  constructor(private referenceService: ReferenceService) { this.getFunctionValues(); }

  getFunctionValues() {
    this.referenceService.getEmployeeFunctions().subscribe(res => {this.list = res.data});
  }
}
