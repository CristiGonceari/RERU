import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { EvaluationListQueries } from '../../../utils/models/evaluation-list.model';
import { EvaluationsTableComponent } from '../evaluations-table/evaluations-table.component';
import { ObjectUtil } from '../../../utils/util/object.util';
import { parseDate } from '../../../utils/util/parsings.util';
import * as moment from 'moment';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements AfterViewInit {
  @ViewChild(EvaluationsTableComponent) evaluationsTable: EvaluationsTableComponent;
  @ViewChild('highlight') highlight: ElementRef;
  private readonly highlightKey = 'HIGHLIGHT';
  // includeAll: boolean;
  private readonly initialFilters: EvaluationListQueries = {
    evaluatedName: '',
    evaluatorName: '',
    counterSignerName: '',
    type: '',
    status: '',
    createDateFrom: '',
    createDateTo: ''
  };
  parsedCreateDateFrom: string;
  parsedCreateDateTo: string;
  public filters: EvaluationListQueries = {...this.initialFilters};

  constructor(private readonly cd: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.initHighlight();
    this.cd.detectChanges();
  }

  initHighlight(): void {
    const highlight = localStorage.getItem(this.highlightKey);
    if (highlight && highlight === 'true') this.highlight.nativeElement.checked = true;
  }

  filterBy(): void {
    this.evaluationsTable.list(ObjectUtil.capitalizeProperties({ ...this.filters }));
  }

  clearFilters(): void {
    this.parsedCreateDateFrom = null;
    this.parsedCreateDateTo = null;
    this.filters = {...this.initialFilters};
    this.evaluationsTable.list({});
  }

  parseDate(value: string|Date, field: string): void {
    const nationalDateRegex = new RegExp(/^(0{1}[1-9]{1}|[1-2]{1}[0-9]{1}|[3]{1}[0-1])\.(0{1}[1-9]{1}|1{1}[0-2]{1})\.([1-9]{1}[0-9]{1}[0-9]{1}[0-9]{1})$/);
    if (typeof value === 'string' && value === '') {
      this.filters[field] = null;
      return;
    }

    if (typeof value === 'string' && !nationalDateRegex.test(value as string)) {
      this.filters[field] = null;
      return;
    }

    if (typeof value === 'string' && nationalDateRegex.test(value as string)) {
      const date = value.split('.');
      this.filters[field] = new Date(`${date[2]}-${date[1]}-${date[0]}`).toISOString();
      return;
    }

    if (typeof value === 'object') {
      const date = moment(value).toDate();
      this.filters[field] = new Date(date).toISOString()
    }
  }

  // TODO: Functionality to include full list for full-access users
  // filterList(includeAll: boolean): void {
  //   if (includeAll) {
  //     this.evaluationsTable.includeAll = true;
  //     this.evaluationsTable.list();
  //   } else {
  //     this.evaluationsTable.includeAll = false;
  //     this.evaluationsTable.list();
  //   }
  // }

  handleHighlightChange(value: boolean): void {
    if (value) {
      localStorage.setItem(this.highlightKey, value+'');
      return;
    }

    localStorage.removeItem(this.highlightKey);
  }
}
