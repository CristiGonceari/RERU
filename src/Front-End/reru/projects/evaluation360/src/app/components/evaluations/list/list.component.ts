import { Component, ViewChild } from '@angular/core';
import { EvaluationsTableComponent } from '../evaluations-table/evaluations-table.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  @ViewChild(EvaluationsTableComponent) evaluationsTable: EvaluationsTableComponent;
  includeAll: boolean;
  constructor() {
    console.log('list constructor')
   }

  filterList(includeAll: boolean): void {
    if (includeAll) {
      this.evaluationsTable.includeAll = true;
      this.evaluationsTable.list();
    } else {
      this.evaluationsTable.includeAll = false;
      this.evaluationsTable.list();
    }
  }
}
