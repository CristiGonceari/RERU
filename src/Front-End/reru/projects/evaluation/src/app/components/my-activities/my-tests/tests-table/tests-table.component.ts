import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { Test } from '../../../../utils/models/tests/test.model';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
import { EventCalendarComponent } from 'projects/evaluation/src/app/utils/components/event-calendar/event-calendar.component';
import { renderFlagCheckIfStmt } from '@angular/compiler/src/render3/view/template';


@Component({
  selector: 'app-tests-table',
  templateUrl: './tests-table.component.html',
  styleUrls: ['./tests-table.component.scss']
})
export class TestsTableComponent implements OnInit {

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

  constructor(private testService: TestService,
    private testTypeService: TestTypeService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
    }

    const request = {
      date: this.selectedDay,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUserTestsWithoutEventByDate(request).subscribe(response => {
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
      this.startTime = data.fromDate,
        this.endTime = data.tillDate
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
    }

    this.testService.getUserTestsWithoutEvent(params).subscribe(
      res => {
        if (res && res.data) {
          this.testRowList = res.data.items;
          this.isLoading = false;
          this.pagedSummary = res.data.pagedSummary;
          this.selectedDay = null
        }
      }
    )
  }

  parseDates(date) {

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
      this.getTestType(res.data.testTypeId);
    })
  }


  getTestType(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTypeId: testTypeId }).subscribe(res => {
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

    this.testService.getUserTestsWithoutEventCount(request).subscribe(response => {

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
    })
  }

}
