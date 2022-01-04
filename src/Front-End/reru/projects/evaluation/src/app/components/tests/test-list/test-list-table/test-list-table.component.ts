import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { ReferenceService } from '../../../../utils/services/reference/reference.service';
import { PrintTemplateService } from '../../../../utils/services/print-template/print-template.service';
import { TestService } from '../../../../utils/services/test/test.service';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum'
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum'
import { saveAs } from 'file-saver';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationUtil } from '../../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';


@Component({
  selector: 'app-test-list-table',
  templateUrl: './test-list-table.component.html',
  styleUrls: ['./test-list-table.component.scss']
})
export class TestListTableComponent implements OnInit {
  pagination: PaginationModel = new PaginationModel();
  testTypeName = [];
  testToSearch;
  userName;
  testTypeId: number;
  testType = [];
  pager: number[] = [];
  statusList = [];
  resultStatusList = [];
  score: number[];
  statusData: any[];
  status = TestStatusEnum;
  result = TestResultStatusEnum;
  selectedStatus: number;
  verificationProgress = [];
  dateTimeFrom: string;
  searchFrom: string;
  dateTimeTo: string;
  searchTo: string;
  testStatusesList;
  selectedSortOrder = "Date range";
  dateData = ["None", "Today", "This week", "This month"];
  testPassStatus;
  testPassEnum: TestStatusEnum;
  eventName;
  locationName;
  isLoading: boolean = true;

  constructor(
    private testService: TestService,
    private modalService: NgbModal,
    public router: Router,
    private referenceService: ReferenceService,
    private datePipe: DatePipe,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService
  ) { }

  ngOnInit(): void {
    this.initializeTestList();
    this.getTestStatuses();
  }

  initializeTestList(): void {
    this.getResultStatus();
    this.getTests();
  }

  getResultStatus(): void {
    this.resultStatusList = Object.keys(TestResultStatusEnum)
      .map(key => TestResultStatusEnum[key])
      .filter(value => typeof value === 'number') as string[];
  }

  setTimeToSearch(): void {
    if (this.dateTimeFrom) {
      const date = new Date(this.dateTimeFrom);
      this.searchFrom = new Date(date.getTime() - (new Date(this.dateTimeFrom).getTimezoneOffset() * 60000)).toISOString();
    }
    if (this.dateTimeTo) {
      const date = new Date(this.dateTimeTo);
      this.searchTo = new Date(date.getTime() - (new Date(this.dateTimeTo).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  getTests(data: any = {}) {
    this.setTimeToSearch();
    this.isLoading = true;

    let params = {
      testTypeName: this.testToSearch || '',
      locationKeyword: this.locationName || '',
      eventName: this.eventName || '',
      userName: this.userName || '',
      programmedTimeFrom: this.searchFrom,
      programmedTimeTo: this.searchTo,
      testStatus: data.selectedStatus || this.selectedStatus,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }

    this.testService.getTests(params).subscribe(res => {
      if (res && res.data) {
        this.testType = res.data.items;
        this.verificationProgress = res.data.items.map(el => el.verificationProgress);
        this.testTypeName = res.data.items.map(it => it.testTypeName);
        this.score = res.data.items.map(s => s.score);
        this.pagination = res.data.pagedSummary;
        this.searchFrom = '';
        this.searchTo = '';
        this.isLoading = false;

        for (let i = 1; i <= this.pagination.totalCount; i++) {
          this.pager.push(i);
        }
      }
    });
  }

  ChangeSortOrder(newSortOrder) {
    this.selectedSortOrder = newSortOrder;
    this.setTimeToSearch();

    if (newSortOrder === "None") {
      this.searchFrom = ""; this.searchTo = "";
      this.dateTimeFrom = ""; this.dateTimeTo = "";
    } else if (newSortOrder === "Today")
      this.today();
    else if (newSortOrder === "This week")
      this.thisWeek();
    else if (newSortOrder === "This month")
      this.thisMonth();
    this.getTests();
  }

  today() {
    this.setTimeToSearch();

    this.dateTimeFrom = this.datePipe.transform(new Date(new Date().setHours(0, 0)), "MM/dd/yyyy,  hh:mm a");
    this.dateTimeTo = this.datePipe.transform(new Date(new Date().setHours(23, 59)), "MM/dd/yyyy,  hh:mm a");
  }

  thisWeek() {
    this.setTimeToSearch();
    let first = new Date().getDate() - new Date().getDay() + 1;

    this.dateTimeFrom = this.datePipe.transform(new Date(new Date().setDate(first)).toUTCString(), "MM/dd/yyyy");
    this.dateTimeTo = this.datePipe.transform(new Date(new Date().setDate(first + 6)).toUTCString(), "MM/dd/yyyy");
  }

  thisMonth() {
    this.setTimeToSearch();

    this.dateTimeFrom = this.datePipe.transform(new Date(new Date().getFullYear(), new Date().getMonth(), 1), "MM/dd/yyyy");
    this.dateTimeTo = this.datePipe.transform(new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0), "MM/dd/yyyy");
  }

  getTestStatuses() {
    this.referenceService.getTestStatuses().subscribe((res) => this.testStatusesList = res.data);
  }

  stopTest(id): void {
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = "Finish Test";
    modalRef.componentInstance.description = "Do you want to finish test ?";
    modalRef.result.then(
      () => this.testService.finalizeTest(id).subscribe(() => { this.getTests() }),
      () => { }
    );

  }

  closeTest(testId): void {
    this.changeStatus(testId, TestStatusEnum.Closed);
  }

  allowToStartTest(testId) {
    this.testService.allow({ testId: testId }).subscribe(() => this.getTests())
  }

  changeStatus(testId, status?) {
    let data = {
      testId: testId,
      status: status
    }
    this.testService.changeStatus(data).subscribe(() => this.getTests());
  }

  export(): void {
    const fileName = 'list.xlsx';
    const fileType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    this.testService.export().subscribe((response: HttpResponse<any>) => {
      const blob = new Blob([response.body], { type: fileType });
      const file = new File([blob], fileName, { type: fileType });
      saveAs(file);
    });
  }

  removeTest(id: number): void {
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = "Delete Test";
    modalRef.componentInstance.description = "Do you want to delete test ?";
    modalRef.result.then(
      () => this.onConfirm(id), () => { })
  }

  onConfirm(testId: number): void {
    this.testService.deleteTest({ id: testId }).subscribe(() => {
      this.getTests()
      this.notificationService.success('Success', 'Test was successfully deleted', NotificationUtil.getDefaultMidConfig());
    });
  }

  printTest(testId) {
    this.printService.getTestPdf(testId).subscribe((response: any) => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }
}
