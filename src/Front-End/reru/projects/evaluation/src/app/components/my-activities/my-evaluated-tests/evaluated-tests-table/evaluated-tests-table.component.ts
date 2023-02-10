import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Test } from 'projects/evaluation/src/app/utils/models/tests/test.model';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../../utils/services/i18n/i18n.service';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-evaluated-tests-table',
  templateUrl: './evaluated-tests-table.component.html',
  styleUrls: ['../../table-inherited.component.scss'],
})
export class EvaluatedTestsTableComponent {
  tests: any;
  testRowList: Test[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  isLoadingCountedTests: boolean = true;

  enum = TestStatusEnum;
  resultEnum = TestResultStatusEnum;

  testDto = new Test();
  settings: any;
  testId;

  startTime;
  endTime;
  selectedDay;
  myTests: any[] = [];
  countedTests: Events[] = [];

  displayYear: number;
  displayMonth: string;
  displayDate;

  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];
  filters: any = {};

  constructor(
    private testService: TestService,
    private testTemplateService: TestTemplateService,
    private router: Router,
    private modalService: NgbModal,
    public translate: I18nService,
  ) { }

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date);
    }

    const request = {
      date: this.selectedDay,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    };

    this.testService
      .getMyEvaluatedTestsByDate(request)
      .subscribe((response) => {
        if (response.success) {
          this.testRowList = response.data.items || [];
          this.pagedSummary = response.data.pagedSummary;
          this.isLoading = false;
        }
      }, () => {
        this.isLoading = false;
      });
  }

  getHeaders(name: string): void {
    this.translateData();
    let headersHtml = document.getElementsByName('column');
    let headersDto = [
      'programmedTime',
      'userName',
      'testTemplateName',
      'accumulatedPercentage',
      'resultValue'
    ];

    for (let i = 0; i < headersHtml.length; i++) {
      if (i == 0) {
        headersHtml[i].innerHTML = headersHtml[i].innerHTML.split('&')[0]
      }
      if (i == 1) {
        this.headersToPrint.push({ value: "testStatus", label: "Statut", isChecked: true })
      }
      if (i == 3) {
        headersHtml[i].innerHTML = headersHtml[i].innerHTML.split('/')[0]
      }
      if (i == 4) {
        this.headersToPrint.push({ value: "minPercent", label: "Procentaj minim", isChecked: true })
      }

      this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
    }

    let printData = {
      tableName: name,
      fields: this.headersToPrint,
      orientation: 2,
      date: this.selectedDay,
      ...this.filters
    };

    const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.tableData = printData;
    modalRef.componentInstance.translateData = this.printTranslates;
    modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
    this.headersToPrint = [];
  }

  translateData(): void {
    this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format']
    forkJoin([
      this.translate.get('print.print-table'),
      this.translate.get('print.print-msg'),
      this.translate.get('print.sorted-by'),
      this.translate.get('button.cancel'),
      this.translate.get('print.select-file-format')
    ]).subscribe(
      (items) => {
        for (let i = 0; i < this.printTranslates.length; i++) {
          this.printTranslates[i] = items[i];
        }
      }
    );
  }

  printTable(data): void {
    this.downloadFile = true;

    this.testService.prinMyEvaluatedTests(data).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -2);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], data.tableName.trim(), { type: response.body.type });
        saveAs(file);
        this.downloadFile = false;
      }
    }, () => this.downloadFile = false);
  }


  getUserTests(data: any = {}) {
    this.isLoading = true;

    if (data.fromDate != null && data.tillDate != null) {
      (this.startTime = data.fromDate), (this.endTime = data.tillDate);
    }

    if (data.displayMonth != null && data.displayYear != null) {
      this.displayMonth = data.displayMonth;
      this.displayYear = data.displayYear;
    }

    let params = {
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
      startTime: this.parseDates(this.startTime),
      endTime: this.parseDates(this.endTime),
    };

    this.testService.getMyEvaluatedTests(params).subscribe((res) => {
      if (res && res.data) {
        this.testRowList = res.data.items;
        this.isLoading = false;
        this.pagedSummary = res.data.pagedSummary;
        this.selectedDay = null;
      }
    }, () => {
      this.isLoading = false;
    });
  }

  parseDates(date) {
    const day = (date && date.getDate()) || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = (date && date.getMonth() + 1) || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = (date && date.getFullYear()) || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;
  }

  parseDatesForTable(date) {
    const day = (date && date.getDate()) || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = (date && date.getMonth() + 1) || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = (date && date.getFullYear()) || -1;

    return `${dayWithZero}/${monthWithZero}/${year}`;
  }

  getTestById(testId: number) {
    this.testService.getTest(testId).subscribe((res) => {
      this.testId = res.data.id;
      this.getTestTemplate(res.data.testTemplateId);
    });
  }

  getTestTemplate(testTemplateId) {
    this.testTemplateService
      .getTestTemplateSettings({ testTemplateId: testTemplateId })
      .subscribe((res) => {
        this.settings = res.data;
        this.goToTest();
      });
  }

  goToTest(): void {
    if (this.settings.showManyQuestionPerPage)
      this.router.navigate(['my-activities/multiple-per-page', this.testId]);
    else this.router.navigate(['my-activities/one-test-per-page', this.testId]);
  }

  getListOfCoutedTests(data) {
    this.isLoadingCountedTests = true;

    const request = {
      startTime: this.parseDates(data.fromDate),
      endTime: this.parseDates(data.tillDate),
    };

    this.testService.getMyEvaluatedTestsCount(request).subscribe((response) => {
      if (response.success) {
        this.countedTests = response.data;

        for (let calendar of data.calendar) {
          let data = new Date(calendar.date);

          for (let values of response.data) {
            let c = new Date(values.date);

            if (+data == +c) {
              calendar.count = values.count;
            }
          }
        }
      }
      this.isLoadingCountedTests = false;
    }, () => {
      this.isLoadingCountedTests = false;
    })
  }
}
