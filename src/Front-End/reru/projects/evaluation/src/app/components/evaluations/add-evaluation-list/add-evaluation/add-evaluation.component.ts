import { Component, Input, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Location } from '@angular/common';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AddEditTest } from '../../../../utils/models/tests/add-edit-test.model';
import { FormControl } from '@angular/forms';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { EventTestTemplateService } from 'projects/evaluation/src/app/utils/services/event-test-template/event-test-template.service';
import { TestTemplateModeEnum } from 'projects/evaluation/src/app/utils/enums/test-template-mode.enum';
import { ViewUsersModalComponent } from 'projects/evaluation/src/app/utils/modals/view-users-modal/view-users-modal.component';
import { ViewTemplatesModalComponent } from 'projects/evaluation/src/app/utils/modals/view-templates-modal/view-templates-modal.component';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-add-evaluation',
  templateUrl: './add-evaluation.component.html',
  styleUrls: ['./add-evaluation.component.scss']
})
export class AddEvaluationComponent implements OnInit {
  @Input() testEvent: boolean;

  processProgress: any;

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
  eventId;
  asignedUsers = [];
  asignedEvaluators = [];
  asignedTemplates = [];
  templatesIds = [];

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
    private eventTestTemplateService: EventTestTemplateService,
  ) { }

  ngOnInit(): void {
    this.getEvents();
  }

  getActiveTestTemplate() {
    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active,
      eventId: this.event.value || null,
      mode: TestTemplateModeEnum.Evaluation
    }
    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data;
    })

    if (params.eventId != null) this.getEvent(params.eventId);
    else this.showEventCard = false;
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data;
      this.getActiveTestTemplate();
    });
  }

  onChange(event){
    this.eventId = event;
  }

  getEvent(eventId: any) {
    this.eventService.getEvent(eventId).subscribe((res) => {
      this.eventDatas = res.data;
      this.showEventCard = true;
    })
  }

  parse() {
    return new AddEditTest({
      userProfileId: this.userListToAdd,
      programmedTime: null,
      eventId: +this.event.value || null,
      evaluatorId: this.evaluatorList || null,
      testStatus: TestStatusEnum.Programmed,
      testTemplateId: +this.testTemplate.value || 0,
      showUserName: true
    })
  }

  createTest() {
    this.disableBtn = true
    console.log(this.parse())

    this.testService.createEvaluations(this.parse()).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('tests.tests-were-programmed'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });

      this.backClicked();
      this.disableBtn = false;
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
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
    this.location.back();
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
    this.exceptUserIds = this.userListToAdd;
    this.openUsersModal(this.evaluatorList, 'checkbox', true);
  }

  attachUsers(): void {
    this.exceptUserIds = this.evaluatorList;
    this.openUsersModal(this.userListToAdd, 'checkbox', false);
  }

  openUsersModal(attachedItems, inputType, whichUser): void {
    const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.exceptUserIds = this.exceptUserIds;
    modalRef.componentInstance.attachedItems = attachedItems;
    modalRef.componentInstance.inputType = inputType;
    modalRef.componentInstance.page = 'add-evaluation';
    modalRef.componentInstance.eventId = +this.event.value;
    modalRef.componentInstance.whichUser = whichUser;
    modalRef.result.then(() => {
      if (whichUser == true) this.evaluatorList = modalRef.result.__zone_symbol__value.attachedItems;
      else if (whichUser == false) this.userListToAdd = modalRef.result.__zone_symbol__value.attachedItems;
    }, () => { });
  }
}
