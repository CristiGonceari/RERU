import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Location } from '@angular/common';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { FormControl, FormGroup } from '@angular/forms';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { TestTemplateModeEnum } from 'projects/evaluation/src/app/utils/enums/test-template-mode.enum';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-position-add-evaluation',
  templateUrl: './position-add-evaluation.component.html',
  styleUrls: ['./position-add-evaluation.component.scss']
})
export class PositionAddEvaluationComponent implements OnInit {
  @Input() testEvent: boolean;
  @Input() isTestEvent: boolean;

  @Output() newItemEvent = new EventEmitter<Boolean>();

  @Input() eventId: number;
  @Input() testTemplateId: number;
  @Input() positionId: any;
  @Input() usersDiagram: any;

  processProgress: any;

  isLoading: boolean = true;

  eventsList: any;
  selectedActiveTestsWithEvent: any;
  selectActiveLocations: any;
  eventDatas: any;
  evaluatorList: number[] = [];
  userListToAdd: number[] = [];
  processId: number;

  title: string;
  description: string;

  locationSelect = new SelectItem({ value: "0", label: "Select" });
  evaluator = new SelectItem();
  myControl = new FormControl();

  showEventCard: boolean = false;
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
  asignedUsers = [];
  asignedEvaluators = [];
  asignedTemplates = [];
  templatesIds = [];
  settingsForm: FormGroup;
  disable: boolean = false;
  locationId: number;

  constructor(
    private referenceService: ReferenceService,
    private testTemplateService: TestTemplateService,
    private testService: TestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService,
    private modalService: NgbModal,
    private eventService: EventService
  ) { }

  ngOnInit(): void {
    this.settingsForm = new FormGroup({
      sendTimeAndLocationEmail: new FormControl()
    });
    this.getEvent(this.eventId);
  }


  getActiveTestTemplateWithEvent(eventId) {
    if (eventId == "0") {
      this.clearForm();
      return;
    }

    this.isLoading = true;

    this.userListToAdd = [];
    this.evaluatorList = [];

    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active,
      eventId: eventId || null,
      mode: TestTemplateModeEnum.Evaluation
    }

    if ((params.eventId == null || params.eventId === undefined)) {
      this.showEventCard = false;
      this.isLoading = true;
      return;
    }

    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectedActiveTestsWithEvent = res.data.find(x => x.testTemplateId === this.testTemplateId);
      this.isLoading = false;
    })
  }

  ceckTestTemplate(testTemplate): boolean {
    if (typeof (testTemplate.value) === 'undefined' || typeof (testTemplate.value) === 'string') {
      return true
    } else {
      return false
    }
  }

  getActiveLocations() {
    let params = {
      eventId: this.eventDatas.id
    }

    this.referenceService.getLocationsSelectValue(params).subscribe(res => { this.selectActiveLocations = res.data; })
  }

  setTimeToSearch(): void {
    if (this.date) {
      const date = new Date(this.date);
      this.search = new Date(date.getTime() - (new Date(this.date).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  clearForm() {
    this.showEventCard = false;
    this.isLoading = true;
    this.evaluatorList.length = 0;
    this.isTestTemplateOneAnswer = false;
  }

  onChange(event) {
    this.eventId = event;
  }

  getEvent(eventId: any) {
    if (eventId == 0) return;
    this.eventService.getEvent(eventId).subscribe((res) => {
      this.eventDatas = res.data;
      this.showEventCard = true;

      this.getActiveLocations();
      this.getActiveTestTemplateWithEvent(res.data.id);
    })
  }

  parse() {
    this.setTimeToSearch();
    return {
      userProfileIds: this.userListToAdd,
      programmedTime: null,
      solicitedTime: this.search,
      eventId: +this.eventDatas.id || null,
      evaluatorIds: this.evaluatorList || [],
      testStatus: TestStatusEnum.Programmed,
      testTemplateId: +this.selectedActiveTestsWithEvent.testTemplateId || 0,
      processId: this.processId || null,
      locationId: this.locationSelect.value == "0" ? null : this.locationSelect.value
    }
  }

  createTest() {
    this.disableBtn = true;
    this.isStartAddingTests = true;

    this.testService.startAddProcess({ totalProcesses: this.parse().userProfileIds.length, processType: 2 }).subscribe(res => {
      this.processId = res.data;

      const interval = this.setIntervalGetProcess();

      this.testService.createEvaluations(this.parse()).subscribe(
        () => {
          forkJoin([
            this.translate.get('modal.success'),
            this.translate.get('tests.evaliations-were-programmed'),
          ]).subscribe(([title, description]) => {
            this.title = title;
            this.description = description;
          });

          clearInterval(interval);
          this.isStartAddingTests = false;

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

  createTestAndPrint() {
    this.disableBtn = true;
    this.testService.createTest(this.parse()).subscribe((res) => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('tests.tests-were-programmed'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.performingTestPdf(res.data);
      this.backClicked();
      this.disableBtn = false;
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.newItemEvent.emit(false);
  }

  performingTestPdf(testIds) {
    this.printService.getPerformingTestPdf({ testsIds: testIds }).subscribe((response: any) => {
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
    this.exceptUserIds = this.usersDiagram.map(x => x.userProfileId);
    this.openUsersModal(this.evaluatorList, 'checkbox', true);
  }

  attachUsers(): void {
    this.exceptUserIds = this.evaluatorList;
    this.openUsersModal(this.userListToAdd, 'checkbox', false);
  }

  openUsersModal(attachedItems, inputType, whichUser): void {
    const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.exceptUserIds = this.exceptUserIds;
    modalRef.componentInstance.positionId = whichUser ? null : this.positionId;
    modalRef.componentInstance.attachedItems = [...attachedItems];
    modalRef.componentInstance.inputType = inputType;
    modalRef.componentInstance.page = 'add-evaluation';
    modalRef.componentInstance.eventId = this.eventDatas.id;
    modalRef.componentInstance.whichUser = whichUser;
    modalRef.componentInstance.testTemplateId = whichUser ? +this.selectedActiveTestsWithEvent.testTemplateId : null;
    modalRef.result.then(() => {
      if (whichUser == true) this.evaluatorList = modalRef.result.__zone_symbol__value.attachedItems;
      else if (whichUser == false) this.userListToAdd = modalRef.result.__zone_symbol__value.attachedItems;
    }, () => { });
  }
}
