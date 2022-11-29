import { Component, ViewChild } from '@angular/core';
import { SurveyTableComponent } from '../survey-table/survey-table.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  @ViewChild(SurveyTableComponent) surveyTable: SurveyTableComponent;
  includeAll: boolean;
  constructor() { }

  filterList(includeAll: boolean): void {
    if (includeAll) {
      this.surveyTable.includeAll = true;
      this.surveyTable.list();
    } else {
      this.surveyTable.includeAll = false;
      this.surveyTable.list();
    }
  }
}
