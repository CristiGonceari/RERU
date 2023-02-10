import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../services/reference.service';
@Component({
  selector: 'app-search-employee-function',
  templateUrl: './search-employee-function.component.html',
  styleUrls: ['./search-employee-function.component.scss']
})
export class SearchEmployeeFunctionComponent {

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  function: string = '';
  constructor(private referenceService: ReferenceService) { this.getFunctionValues(); }

  getFunctionValues() {
    this.referenceService.getEmployeeFunctionsSelectValues().subscribe(res => {this.list = res.data});
  }
}
