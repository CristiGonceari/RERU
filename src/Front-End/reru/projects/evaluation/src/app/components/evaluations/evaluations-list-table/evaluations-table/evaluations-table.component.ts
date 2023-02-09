import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
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
import { PrintModalComponent } from '@erp/shared';
import { NotificationUtil } from '../../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { GenerateDocumentModalComponent } from 'projects/evaluation/src/app/utils/modals/generate-document-modal/generate-document-modal.component';
import { FileTypeEnum } from 'projects/evaluation/src/app/utils/enums/file-type.enum';
import { TestTemplateModeEnum } from '../../../../utils/enums/test-template-mode.enum';
import { EnumStringTranslatorService } from 'projects/evaluation/src/app/utils/services/enum-string-translator.service';

@Component({
  selector: 'app-evaluations-table',
  templateUrl: './evaluations-table.component.html',
  styleUrls: ['./evaluations-table.component.scss']
})
export class EvaluationsTableComponent implements OnInit {
  pagination: PaginationModel = new PaginationModel();
  testTemplateName = [];
  testToSearch;
  userName;
  userEmail;
  testTemplateId: number;
  testTemplate = [];
  pager: number[] = [];
  statusList = [];
  resultStatusList = [];
  score: number[];
  statusData: any[];
  status = TestStatusEnum;
  result = TestResultStatusEnum;
  fileTypeEnum = FileTypeEnum;
  selectedStatus: number;
  selectedResult: number;
  verificationProgress = [];
  dateTimeFrom: string;
  searchFrom: string;
  dateTimeTo: string;
  searchTo: string;
  testStatusesList;
  testResultsList;
  selectedSortOrder = "Date range";
  dateData = ["None", "Today", "This week", "This month"];
  testPassStatus;
  testPassEnum: TestStatusEnum;
  eventName;
  locationName;
  idnp: string;
  isLoading: boolean = true;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
  filters: any = {};

  title: string;
	description: string;
	no: string;
	yes: string;

  constructor(
    private testService: TestService,
    private modalService: NgbModal,
	  public translate: I18nService,
    public router: Router,
    private referenceService: ReferenceService,
    private datePipe: DatePipe,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService,
    private enumStringTranslatorService: EnumStringTranslatorService
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

  translateResultValue(item){
		return this.enumStringTranslatorService.translateTestResultValue(item);
	}

  getTests(data: any = {}) {
    this.setTimeToSearch();
    this.isLoading = true; 
    
    let params = {
      mode: TestTemplateModeEnum.Evaluation,
      testTemplateName: this.filters.testName || this.testToSearch || '',
      locationKeyword: this.filters.testLocation || this.locationName || '',
      eventName: this.filters.testEvent || this.eventName || '',
      userName: this.filters.userName || this.userName || '',
      evaluatorName: this.filters.evaluatorName || '',
      email: this.filters.userEmail || this.userEmail || '',
      programmedTimeFrom: this.searchFrom,
      programmedTimeTo: this.searchTo,
      testStatus: this.filters.selectedStatus || this.selectedStatus,
      resultStatus: this.filters.selectedResult || this.selectedResult,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      ...this.filters
    }

    this.testService.getEvaluations(params).subscribe(res => {
      if (res && res.data) {
        this.testTemplate = res.data.items;
        this.verificationProgress = res.data.items.map(el => el.verificationProgress);
        this.testTemplateName = res.data.items.map(it => it.testTemplateName);
        this.score = res.data.items.map(s => s.score);
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;

        for (let i = 1; i <= this.pagination.totalCount; i++) {
          this.pager.push(i);
        }
      }
    });
  }

  getTestStatuses() {
    this.referenceService.getTestStatuses().subscribe((res) => this.testStatusesList = res.data);
  }

  getTestResults() {
    this.referenceService.getTestResults().subscribe((res) => this.testResultsList = res.data);
  }

  parseCandidatePositions(candidatePositionsNames: string[]){
    let string = candidatePositionsNames.join();
        return string.split(',').join(', ');
  }

  stopTest(id): void {
    forkJoin([
			this.translate.get('modal.finish-test'),
			this.translate.get('tests.stop-test-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
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
    forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('tests.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(
      () => this.onConfirm(id), () => { })
  }

  onConfirm(testId: number): void {
    this.testService.deleteTest({ id: testId }).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.getTests()
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
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

  getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = [
      'testTemplateName', 
      'userName',
      'evaluatorName', 
      'eventName', 
      'departmentName',
      'locationNames', 
      'testStatus', 
      'resultValue'
    ];
    
		for (let i = 0; i < headersHtml.length - 1; i++) {
      if(i == 2){
        this.headersToPrint.push({ value: "idnp", label: "Idnp", isChecked: true})
      }
      if(i == 4){
         headersHtml[i].innerHTML = headersHtml[i].innerHTML.split('/')[0]
      }
      if(i == 5){
        this.headersToPrint.push({ value: "roleName", label: "Role", isChecked: true})
      }
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
    
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
      testStatus: this.filters.selectedStatus || this.selectedStatus,
      resultStatus: this.filters.selectedResult || this.selectedResult,
      testTemplateName: this.filters.testName || this.testToSearch || '',
      locationKeyword: this.filters.testLocation || this.locationName || '',
      eventName: this.filters.testEvent || this.eventName || '',
      userName: this.filters.userName || this.userName || '',
      evaluatorName: this.filters.evaluatorName || '',
      programmedTimeFrom: this.searchFrom || null,
      programmedTimeTo: this.searchTo || null,
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
    
		this.testService.printEvaluations(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	} 

  printTestResult(testId) {
    this.printService.getEvaluationResultPdf(testId).subscribe((response: any) => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  openGenerateDocumentModal(id, name){
		const modalRef: any = this.modalService.open(GenerateDocumentModalComponent, { centered: true, size:'xl', windowClass: 'my-class', scrollable: true });
		modalRef.componentInstance.id = id;
		modalRef.componentInstance.testName = name;
		modalRef.componentInstance.fileType = 2;
		modalRef.result.then((response) => (response), () => {});
	}
  
  setFilter(field: string, value): void {
		this.filters[field] = value;
		this.pagination.currentPage = 1;
		this.getTests();
	}

	resetFilters(): void {
		this.filters = {};
		this.pagination.currentPage = 1;
    this.dateTimeFrom = '';
    this.dateTimeTo = '';
    this.searchFrom = '';
    this.searchTo = '';
		this.getTests();
	}
}
