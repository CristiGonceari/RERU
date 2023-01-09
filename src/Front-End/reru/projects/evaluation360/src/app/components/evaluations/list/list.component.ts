import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { EvaluationListQueries } from '../../../utils/models/evaluation-list.model';
import { EvaluationsTableComponent } from '../evaluations-table/evaluations-table.component';
import { ObjectUtil } from '../../../utils/util/object.util';
import * as moment from 'moment';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { EvaluationService } from '../../../utils/services/evaluations.service';

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
  printTranslates: string[] = [];
  headersToPrint: {
    value: string;
    label: string;
    isChecked: boolean;
  }[] = [];
  title: string;
  public filters: EvaluationListQueries = {...this.initialFilters};

  constructor(private readonly cd: ChangeDetectorRef,
              private readonly translate: I18nService,
              private readonly modalService: NgbModal,
              private readonly evaluationService: EvaluationService) {}

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

  async openPrintModal() {
		this.translateData();
    const tableName = await this.translate.get('evaluations.list').toPromise();
    const headersHtml = this.evaluationsTable.headersHtml;
		const headersDto = ['evaluatedName', 'evaluatorName', 'counterSignerName', 'type', 'points', 'status'];

		for (let i = 0; i < 6; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

		const printData = {
			tableName,
			orientation: 2,
			fields: this.headersToPrint,
      evaluatedName: this.filters.evaluatedName || null,
      evaluatorName: this.filters.evaluatorName || null,
      counterSignerName: this.filters.counterSignerName || null,
      type: this.filters.type || null,
			status: +this.filters.status || null
		};

		const modalRef: NgbModalRef = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then((response) => this.printTable(response), () => { });
		this.headersToPrint = [];
	}

  printTable(data): void {
		this.evaluationService.printEvaluations(data).subscribe(response => {
			if (response) {
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
			}
		}, () => {});
	}

  translateData(): void {
		this.printTranslates = Array.from(Array(4).keys()).map(el => el+'');
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
		]).subscribe(
			(items) => {
				for (let i = 0; i < this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
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
