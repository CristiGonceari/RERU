import { PlatformLocation } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
import { EnumStringTranslatorService } from 'projects/evaluation/src/app/utils/services/enum-string-translator.service';
import { ObjectUtil } from 'projects/evaluation/src/app/utils/util/object.util';
import { CloudFileService } from 'projects/evaluation/src/app/utils/services/cloud-file/cloud-file.service';
import { SignatureService } from 'projects/evaluation/src/app/utils/services/signature/signature.service';
import { UserProfileService } from 'projects/evaluation/src/app/utils/services/user-profile/user-profile.service';
import { HttpResponse } from '@angular/common/http';


@Component({
  selector: 'app-test-list-table',
  templateUrl: './test-list-table.component.html',
  styleUrls: ['./test-list-table.component.scss']
})

export class TestListTableComponent implements OnInit {
  pagination: PaginationModel = new PaginationModel();
  testTemplateName = [];
  testToSearch;
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
  roleName;
  functionName;
  isLoading: boolean = true;
  isLoadingSignButton: boolean = false;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];
  filters: any = {};
  testName: string = '';
  userName: string = '';
  evaluatorName: string = '';
  userEmail: string = '';
  idnp: string = '';
  testEvent: string = '';
  testLocation: string = '';
  colaboratorId: number;

  title: string;
  description: string;
  no: string;
  yes: string;

  isCollpasedRow: boolean[] = []
  testsByHash: any[] = []
  currentUser;
  signResponseTest;

  constructor(
    private testService: TestService,
    private modalService: NgbModal,
    public translate: I18nService,
    public router: Router,
    private referenceService: ReferenceService,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService,
    private enumStringTranslatorService: EnumStringTranslatorService,
    private cloudFileService: CloudFileService,
    private signatureService: SignatureService,
    private route: ActivatedRoute,
    private location: PlatformLocation,
    private userProfileService: UserProfileService
  ) {
  }

  ngOnInit(): void {
    this.initializeTestList();
    this.getTestStatuses();
    this.userProfileService.getCurrentUser().subscribe(res => {
      this.currentUser = res.data;
    });
    this.route.params.subscribe(response => {
      this.signResponseTest = response.id;
    })
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

  translateResultValue(item) {
    return this.enumStringTranslatorService.translateTestResultValue(item);
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

  parseCandidatePositions(candidatePositionsNames: string[]) {
    let string = candidatePositionsNames.join();
    return string.split(',').join(', ');
  }

  getTests(data: any = {}) {
    this.setTimeToSearch();
    this.isLoading = true;

    let params = ObjectUtil.preParseObject({
      mode: null,
      testTemplateName: this.testName || '',
      locationKeyword: this.testLocation || '',
      idnp: this.idnp || '',
      eventName: this.testEvent || '',
      colaboratorId: this.colaboratorId || null,
      userName: this.userName || '',
      evaluatorName: this.evaluatorName || '',
      email: this.userEmail || this.userEmail || '',
      programmedTimeFrom: this.searchFrom || null,
      programmedTimeTo: this.searchTo || null,
      testStatus: this.filters.selectedStatus || this.selectedStatus,
      resultStatus: this.filters.selectedResult || this.selectedResult,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      ...this.filters
    });

    this.testService.getTests(params).subscribe(res => {
      if (res && res.data) {
        this.testTemplate = res.data.items.filter((item, i, arr) => arr.findIndex((t) => t.hashGroupKey == null ? t.id == item.id : t.hashGroupKey == item.hashGroupKey) === i);
        this.verificationProgress = res.data.items.map(el => el.verificationProgress);
        this.testsByHash = res.data.items;
        this.testTemplateName = res.data.items.map(it => it.testTemplateName);
        this.score = res.data.items.map(s => s.score);
        this.pagination = res.data.pagedSummary;
        this.isCollpasedRow.length = res.data.items.length;
        this.isLoading = false;

        this.testTemplate.forEach(item => {
          item.isOpenAccordeon = false;
          item.isLoadingAccordeon = false;
          this.testService.getDocumentsForSign(item.id).subscribe(res => {
            if (res && res.data) {
              item.documentForSign = res.data;
            }
          });
        });
  
        this.pager = Array.from({ length: this.pagination.totalCount }, (_, i) => i + 1);
  
        if (this.signResponseTest) {
          const findIndex = this.testTemplate.findIndex(x => x.id == this.signResponseTest);
          if (findIndex !== -1) {
            this.testTemplate[findIndex].isOpenAccordeon = true;
          }
        }
      }
    });
  }

  getTestsByHash(hashGroupKey, testId) {
    if (hashGroupKey == null) {
      return;
    }
    else if (this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey).length <= 1) {
      return this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey && x.id != testId);
    }

    return this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey);
  }

  hasTestHashGroup(hashGroupKey, testId) {
    if (hashGroupKey == null) {
      return 0;
    }
    else if (this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey).length <= 1) {
      return this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey && x.id != testId).length;
    }

    return this.testsByHash.filter(x => x.hashGroupKey == hashGroupKey).length;
  }

  getTestStatuses() {
    this.referenceService.getTestStatuses().subscribe((res) => this.testStatusesList = res.data);
  }

  getTestResults() {
    this.referenceService.getTestResults().subscribe((res) => this.testResultsList = res.data);
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

  printTestResult(testId) {
    this.printService.getTestResultPdf(testId).subscribe((response: any) => {
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
      'verificationProgress',
      'result',
      'accumulatedPercentage',
      'minPercent'
    ];

    for (let i = 0; i < headersHtml.length - 1; i++) {
      if (i == 2) {
        this.headersToPrint.push({ value: "idnp", label: "Idnp", isChecked: true })
      }
      if (i == 4) {
        headersHtml[i].innerHTML = headersHtml[i].innerHTML.split('/')[0]
      }
      if (i == 5) {
        this.headersToPrint.push({ value: "functionName", label: this.functionName, isChecked: true })
        this.headersToPrint.push({ value: "roleName", label: this.roleName, isChecked: true })
      }
      this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
    }

    let printData = {
      tableName: name,
      fields: this.headersToPrint,
      orientation: 2,
      testStatus: this.filters.selectedStatus || this.selectedStatus,
      resultStatus: this.filters.selectedResult || this.selectedResult,
      testTemplateName: this.testName || '',
      locationKeyword: this.testLocation || '',
      idnp: this.idnp || '',
      eventName: this.testEvent || '',
      userName: this.userName || '',
      evaluatorName: this.evaluatorName || '',
      email: this.userEmail || '',
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
    this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows']
    forkJoin([
      this.translate.get('print.print-table'),
      this.translate.get('print.print-msg'),
      this.translate.get('print.sorted-by'),
      this.translate.get('button.cancel'),
      this.translate.get('print.select-file-format'),
      this.translate.get('print.max-print-rows')
    ]).subscribe(
      (items) => {
        for (let i = 0; i < this.printTranslates.length; i++) {
          this.printTranslates[i] = items[i];
        }
      }
    );

    forkJoin([
      this.translate.get('user-roles.function'),
      this.translate.get('user-roles.role'),
    ]).subscribe(([roleName, functionName]) => {
      this.roleName = roleName;
      this.functionName = functionName;
    });
  }

  printTable(data): void {
    this.downloadFile = true;

    this.testService.print(data).subscribe(response => {
      if (response) {
        const fileName = data.tableName;
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName.trim(), { type: response.body.type });
        saveAs(file);
        this.downloadFile = false;
      }
    }, () => this.downloadFile = false);
  }

  openGenerateDocumentModal(id, name) {
    const modalRef: any = this.modalService.open(GenerateDocumentModalComponent, { centered: true, size: 'xl', windowClass: 'my-class', scrollable: true });
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.testName = name;
    modalRef.componentInstance.fileType = 3;
    modalRef.result.then((response) => (response), () => { });
  }

  setFilter(field: string, value): void {
    this.filters[field] = value;
    this.pagination.currentPage = 1;
    this.getTests();
  }

  parseStringLength(name: string): string {
    if (name.length > 12) {
      return name.slice(0, 12) + "...";
    }

    return name;
  }

  resetFilters(): void {
    this.filters = {};
    this.pagination.currentPage = 1;
    this.testName = '';
    this.testEvent = '';
    this.testLocation = '';
    this.userName = '';
    this.evaluatorName = '';
    this.userEmail = '';
    this.idnp = '';
    this.dateTimeFrom = '';
    this.dateTimeTo = '';
    this.searchFrom = '';
    this.searchTo = '';
    this.colaboratorId = null;
    this.getTests();
  }

  printTestDocumentForSign(mediaFileId: string) {
    this.cloudFileService.download(mediaFileId);
  }

  generateTestResult(item) {
    item.isLoadingAccordeon = true;
    this.testService.generateTestResultFile(item.id).subscribe(res => {
      this.isLoading = true;
      this.getTestsBefore(item);
    })
  }

  signTestDocument(doc, value) {
    this.isLoadingSignButton = true;
    this.signatureService.signDocumentAttmpet(doc.documentForSignId).subscribe(res => {

      let firstSymbol= /\//gi;
      let secondSymbols = /\#/gi;
      let defaultUrl: string = "";

      if (this.location.href.includes("signed")) {
        defaultUrl = this.location.href;
      }
      else {
        defaultUrl = (this.location.href + "/signed/" + value.id).toString();
      }

      let redirectUrl: string = defaultUrl.replace(firstSymbol, "@").replace(secondSymbols, "$");
      value.isLoadingAccordeon = true;
      this.testService.getDocumentsForSign(value.id).subscribe(res => {
        if (res && res.data) {
          value.isOpenAccordeon = true;
          value.documentForSign = res.data;
        }
      });
      let redirect = this.signatureService.redirectMSign(res.data, redirectUrl);

      forkJoin([
        this.translate.get('documents.sign-file'),
        this.translate.get('documents.sign-file-description'),
        this.translate.get('modal.no'),
        this.translate.get('modal.yes'),
      ]).subscribe(([title, description, no, yes]) => {
        this.title = title;
        this.description = description + " " + doc.fileName + ' ?';
        this.no = no;
        this.yes = yes;
      });

      const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
      modalRef.componentInstance.title = this.title;
      modalRef.componentInstance.description = this.description;
      modalRef.componentInstance.buttonNo = this.no;
      modalRef.componentInstance.buttonYes = this.yes;
      modalRef.result.then(
        () => this.redirect(redirect), () => { value.isLoadingAccordeon = false; })

      this.isLoadingSignButton = false;
    })
    this.isLoadingSignButton = false;
    
  }

  redirect(url) {
    window.location.href = url;
  }

  checkSignButton(signedDocuments) {
    return signedDocuments.some(item => item.userProfileId == this.currentUser.id);
  }

  getTestsBefore(item) {
    this.setTimeToSearch();
    this.isLoading = true;

    let params = ObjectUtil.preParseObject({
      mode: null,
      testTemplateName: this.testName || '',
      locationKeyword: this.testLocation || '',
      idnp: this.idnp || '',
      eventName: this.testEvent || '',
      colaboratorId: this.colaboratorId || null,
      userName: this.userName || '',
      evaluatorName: this.evaluatorName || '',
      email: this.userEmail || this.userEmail || '',
      programmedTimeFrom: this.searchFrom || null,
      programmedTimeTo: this.searchTo || null,
      testStatus: this.filters.selectedStatus || this.selectedStatus,
      resultStatus: this.filters.selectedResult || this.selectedResult,
      page: this.pagination.currentPage,
      itemsPerPage: this.pagination.pageSize,
      ...this.filters
    });

    this.testService.getTests(params).subscribe(res => {
      if (res && res.data) {
        this.testTemplate = res.data.items.filter((item, i, arr) => arr.findIndex((t) => t.hashGroupKey == null ? t.id == item.id : t.hashGroupKey == item.hashGroupKey) === i);
        this.verificationProgress = res.data.items.map(el => el.verificationProgress);
        this.testsByHash = res.data.items;
        this.testTemplateName = res.data.items.map(it => it.testTemplateName);
        this.score = res.data.items.map(s => s.score);
        this.pagination = res.data.pagedSummary;
        this.isCollpasedRow.length = res.data.items.length;
        this.isLoading = false;
        item.isLoadingAccordeon = true;

        for (let i = 0; i < this.testTemplate.length; i++) {
          this.testTemplate[i] = Object.assign({}, this.testTemplate[i], { isOpenAccordeon: false, isLoadingAccordeon: false });
        }

        this.getDocumentsForSign(item.id);

        for (let i = 1; i <= this.pagination.totalCount; i++) {
          this.pager.push(i);
        }
      }
    });
  }

  getDocumentsForSign(id)
  {
    this.testService.getDocumentsForSign(id).subscribe(res => {
      if (res && res.data) {
        let findIndex = this.testTemplate.findIndex(x => x.id == id);
        this.testTemplate[findIndex].isOpenAccordeon = true;
        this.testTemplate[findIndex].documentForSign = res.data;
      }
    });
  }
}
