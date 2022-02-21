import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Test } from 'projects/evaluation/src/app/utils/models/tests/test.model';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';

@Component({
  selector: 'app-evaluated-tests-table',
  templateUrl: './evaluated-tests-table.component.html',
  styleUrls: ['./evaluated-tests-table.component.scss'],
})
export class EvaluatedTestsTableComponent implements OnInit {
  tests: any;
  testRowList: Test[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;

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

  constructor(
    private testService: TestService,
    private testTypeService: TestTypeService,
    private router: Router
  ) {}

  ngOnInit(): void {}

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
      });
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
      this.getTestType(res.data.testTemplateId);
    });
  }

  getTestType(testTypeId) {
    this.testTypeService
      .getTestTypeSettings({ testTemplateId: testTypeId })
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
    });
  }
}
