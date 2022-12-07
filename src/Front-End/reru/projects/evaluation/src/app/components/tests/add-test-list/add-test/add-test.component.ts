import { Component, Input, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Location } from '@angular/common';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AddEditTest } from '../../../../utils/models/tests/add-edit-test.model';
import { FormControl } from '@angular/forms';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { BehaviorSubject, forkJoin, interval, Subscribable, Subscriber } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { NgtscCompilerHost } from '@angular/compiler-cli/src/ngtsc/file_system';
import { Subscription, timer } from 'rxjs';
import { map } from 'rxjs/operators';
import { GetBulkProgressHistoryService } from 'projects/evaluation/src/app/utils/services/bulk-progress/get-bulk-progress-history.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { is } from 'date-fns/locale';
import { EmitHint } from 'typescript';
import { TestTemplateModeEnum } from 'projects/evaluation/src/app/utils/enums/test-template-mode.enum';


@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.scss'],
})
export class AddTestComponent implements OnInit {
  @Input() testEvent: boolean;

  processProgress: any;
  isLoading: boolean = true;
  eventsList: any;
  selectActiveTests: any;
  eventDatas: any;
  evaluatorList = [];
  userListToAdd: number[] = [];
  processId: number;

  title: string;
  description: string;

  event = new SelectItem();
  testTemplate = new SelectItem();
  evaluator = new SelectItem();
  myControl = new FormControl();

  showEventCard: boolean = false;
  showName: boolean = false;
  isTestTemplateOneAnswer: boolean = false;
  printTest: boolean = true;
  hasEventEvaluator: boolean = false;
  disableBtn: boolean = false;
  isStartAddingTests: boolean = false;

  cancelRequest: any;
  request;

  toolBarValue: number = 0;
  toolBarProcents: number = 0;

  addTestRequest: any = 0;

  date: Date;
  search: string;
  messageText = '';
  exceptUserIds = [];

  constructor(
    private referenceService: ReferenceService,
    private testTemplateService: TestTemplateService,
    private testService: TestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService,
    private modalService: NgbModal,
    private eventService: EventService,
    private getProcessService: GetBulkProgressHistoryService
  ) { }

  ngOnInit(): void {
    this.getEvents();
    this.getProcessService.currentSendToCancelRequest.subscribe(msg => { this.cancelRequest = msg });
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data;
      this.getActiveTestTemplates();
    });
  }

  setTimeToSearch(): void {
    if (this.date) {
      const date = new Date(this.date);
      this.search = new Date(date.getTime() - (new Date(this.date).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  getEvent(eventId: any) {
    this.eventService.getEvent(eventId).subscribe((res) => {
      this.eventDatas = res.data;
      this.showEventCard = true;
    })
  }

  checkIfIsOneAnswer(event) {
    if (event) {
      this.isTestTemplateOneAnswer = this.selectActiveTests.find(x => x.testTemplateId === event).isOnlyOneAnswer;
      this.printTest = this.selectActiveTests.find(x => x.testTemplateId === event).printTest;
    } else this.isTestTemplateOneAnswer = false;

    if (!this.printTest) this.messageText = "Acest test poate conține video sau audio!"

    if (this.isTestTemplateOneAnswer) {
      this.evaluator.value = null;
    }

    if(event) {
      this.evaluatorList[0] = null;
      this.evaluatorList.length = 0;
    }
  }

  getActiveTestTemplates(event?) {
    this.isLoading = true;

    if (event){
      this.hasEventEvaluator = this.eventsList.find(x => x.eventId === event).isEventEvaluator;
    }

    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active,
      eventId: event || null,
      mode: TestTemplateModeEnum.Test
    }

    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data;
      this.isLoading = false;
    })

    this.userListToAdd = [];
    this.evaluatorList = [];

    if (params.eventId != null) {
      this.getEvent(params.eventId);
    }
    else {
      this.showEventCard = false;
    }

    if (event) this.clearTestData()
  }

  clearTestData(){
    this.userListToAdd.length = 0;
    this.search = null
    this.date = null
    this.evaluatorList.length = 0;
    this.testTemplate.value = null
    this.isTestTemplateOneAnswer = false;
  }

  parse() {
    this.setTimeToSearch();
    return new AddEditTest({
      userProfileIds: this.userListToAdd,
      programmedTime: this.search || null,
      eventId: +this.event.value || null,
      evaluatorIds: this.evaluatorList || null,
      testStatus: TestStatusEnum.Programmed,
      processId: this.processId || null,
      testTemplateId: +this.testTemplate.value || 0,
      showUserName: this.showName
    })
  }

  createTest(print: boolean) {
    this.disableBtn = true
    this.isStartAddingTests = true;

    this.testService.startAddProcess({ totalProcesses: this.parse().userProfileIds.length, processType: 2 }).subscribe(res => {
      this.processId = res.data;

      const interval = this.setIntervalGetProcess();

      this.request = this.testService.createTest(this.parse()).subscribe((response) => {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('tests.tests-were-programmed'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });

        this.downloadProcessResult();

        clearInterval(interval);
        this.isStartAddingTests = false;
        if (print) this.performingTestPdf(response.data);

        this.backClicked();
        this.disableBtn = false;
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      },
        (err) => {
          clearInterval(interval);
          this.isStartAddingTests = false;
          this.disableBtn = false;
        });
    })
  }

  setIntervalGetProcess() {
    return setInterval(() => {
      this.testService.getImportProcess(this.processId).subscribe(res => {
        this.processProgress = res.data;
        this.toolBarValue = Math.round(this.processProgress.done * 100 / this.processProgress.total);
      })
    }, 10 * 1000);
  }

  downloadProcessResult() {
    if (this.toolBarValue == 100) {
      this.testService.getImportProcess(this.processId).subscribe(res => {
        this.processProgress = res.data;
        this.toolBarValue = this.processProgress.doneProcesses * 100 / this.processProgress.totalProcesses;
        this.testService.getImportResult(this.processProgress.fileId).subscribe(response => {
          if (response) {
            const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
            const blob = new Blob([response.body], { type: response.body.type });
            const file = new File([blob], fileName, { type: response.body.type });
            saveAs(file);
          }
        }
        )
      })
    }
  }

  backClicked() {
    this.location.back();
  }

  onItemChange(event) {
    this.showName = event.target.checked;
  }

  performingTestPdf(testsIds) {
    this.printService.getPerformingTestPdf({ testsIds: testsIds }).subscribe((response: any) => {
      let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

      if (response.body.type === 'application/pdf') {
        fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
      }

      const blob = new Blob([response.body], { type: response.body.type });
      const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  attachEvaluators(): void {
    this.exceptUserIds = this.userListToAdd;
    this.openUsersModal(this.evaluatorList, 'radio', true);
  }

  attachUsers(): void {
    this.exceptUserIds = this.evaluatorList;
    this.openUsersModal(this.userListToAdd, 'checkbox', false);
  }

  openUsersModal(attachedItems, inputType, whichUser): void {
    const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.exceptUserIds = this.exceptUserIds;
    modalRef.componentInstance.attachedItems = [...attachedItems];
    modalRef.componentInstance.inputType = inputType;
    modalRef.componentInstance.page = 'add-test';
    modalRef.componentInstance.eventId = +this.event.value;
    modalRef.componentInstance.whichUser = whichUser;
    modalRef.componentInstance.testTemplateId = inputType == "radio" ? +this.testTemplate.value : null;
    modalRef.result.then(() => {
      if (whichUser) {
        console.log("evaluatori", this.evaluatorList)
        this.evaluatorList = modalRef.result.__zone_symbol__value.attachedItems;
      }
      else if(!whichUser) {
        console.log("useri", this.userListToAdd)
         this.userListToAdd = modalRef.result.__zone_symbol__value.attachedItems;
      }
    }, () => { });
  }

  ceckTestTemplate(testTemplate): boolean {
    if (typeof (testTemplate.value) === 'undefined' || typeof (testTemplate.value) === 'string') {
      return true
    } else {
      return false
    }
  }

  cantAdd(){
    if(this.testEvent){
      return this.event.value == null ||
      this.userListToAdd.length <= 0 ||
      (this.testTemplate.value == null || this.testTemplate.value == "") ||
      (this.hasEventEvaluator || this.isTestTemplateOneAnswer ? false : this.evaluatorList[0] == null)
    } else {
      return this.userListToAdd.length <= 0 ||
      (this.isTestTemplateOneAnswer ? false : this.evaluatorList[0] == null) ||
      (this.testTemplate.value == null || this.testTemplate.value == "") ||
      this.date == null
    }
  }
}
