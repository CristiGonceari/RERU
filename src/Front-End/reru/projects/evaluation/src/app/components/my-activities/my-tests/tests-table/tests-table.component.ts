import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { Test } from '../../../../utils/models/tests/test.model';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';

@Component({
  selector: 'app-tests-table',
  templateUrl: './tests-table.component.html',
  styleUrls: ['./tests-table.component.scss']
})
export class TestsTableComponent implements OnInit {
  @ViewChild('testName') testName: any;
  @ViewChild('eventName') eventName: any;
  testRowList: Test[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  filters: any = {};
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

  constructor(private testService: TestService,
    private testTemplateService: TestTemplateService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date);
    }

    const request = {
      date: this.selectedDay,
      page: data.page || this.pagedSummary.currentPage,
      testName: this.filters.testName || '',
      eventName: this.filters.eventName || '',
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getMyTests(request).subscribe(response => {
      if (response.success) {
        this.testRowList = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  parseDates(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;
  }

  parseDatesForTable(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${dayWithZero}/${monthWithZero}/${year}`;
  }

  getTestById(testId: number) {
    this.testService.getTest(testId).subscribe(res => {
      this.testId = res.data.id;
      this.getTestTemplate(res.data.testTemplateId);
    })
  }

  getTestTemplate(testTemplateId) {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: testTemplateId }).subscribe(res => {
      this.settings = res.data;
      this.goToTest();
    })
  }

  goToTest(): void {
    if (this.settings.showManyQuestionPerPage)
      this.router.navigate(['my-activities/multiple-per-page', this.testId]);
    else
      this.router.navigate(['my-activities/one-test-per-page', this.testId]);

  }

  getListOfCoutedTests(data) {
    const request = {
      startTime: this.parseDates(data.fromDate),
      endTime: this.parseDates(data.tillDate)
    }

    this.testService.getMyTestsCount(request).subscribe(
      response => {
        if (response.success) {
          this.countedTests = response.data;

          for (let calendar of data.calendar) {
            let data = new Date(calendar.date);

            for (let values of response.data) {
              let c = new Date(values.date);
              let compararea = +data == +c;

              if (compararea) {
                calendar.count = values.count;
              }
            }
          }
        }
      }
    )
  }

  resetFilters(): void {
    this.filters = {};
    this.testName.key = '';
    this.eventName.key = '';
    this.pagedSummary.currentPage = 1;
    this.getListByDate();
  }

  setFilter(field: string, value): void {
    this.filters[field] = value;
    this.pagedSummary.currentPage = 1;
    this.getListByDate();
  }
}
