import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { SolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/solicited-test.model';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { ActivatedRoute } from '@angular/router';
import { AddEditTest } from 'projects/evaluation/src/app/utils/models/tests/add-edit-test.model';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { SolicitedTestStatusEnum } from 'projects/evaluation/src/app/utils/enums/solicited-test-status.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';

@Component({
  selector: 'app-approve-solicited-test',
  templateUrl: './approve-solicited-test.component.html',
  styleUrls: ['./approve-solicited-test.component.scss']
})
export class ApproveSolicitedTestComponent implements OnInit {
  eventsList: any;
  selectActiveTests: any;
  evaluatorList: any;

  user = new SelectItem();
  event = new SelectItem();
  testTemplate = new SelectItem();
  evaluator = new SelectItem();

  date: Date;
  search: string;
  title: string;
  description: string;
  isLoading: boolean = true;
  solicitedTestId;
  solicitedTest: SolicitedTest = new SolicitedTest();

  showName: boolean = false;
  isTestTemplateOneAnswer: boolean = false;
  printTest: boolean = true;
  needEvaluator: boolean = false;
  hasEventEvaluator: boolean = false;

  messageText = "";

  userListToAdd: number[] = [];

  constructor(
    private referenceService: ReferenceService,
    private testService: TestService,
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    public activatedRoute: ActivatedRoute,
    private eventService: EventService,
    private testTemplateService: TestTemplateService
  ) { }

  ngOnInit(): void {
    this.initData();
    this.getEvaluators();
  }

  initData(): void {
    this.solicitedTestId = this.activatedRoute.snapshot.paramMap.get('id');
    if (this.solicitedTestId != null) this.getSolicitedTest(this.solicitedTestId)
    else this.isLoading = false;
  }

  getSolicitedTest(id) {
    this.solicitedTestService.getSolicitedTest(id).subscribe(res => {
      if (res && res.data) {
        this.solicitedTest = res.data;
        this.date = res.data.solicitedTime;
        this.isLoading = false;
        this.checkIfEventHasEvaluator(res.data.eventId);
        this.checkIfIsOneAnswer(res.data.testTemplateId)
        this.userListToAdd.push(res.data.userProfileId);
        console.log(this.solicitedTest)
      }
    });
  }

  getEvaluators() {
    if (this.needEvaluator === false && this.hasEventEvaluator === false) {
      this.referenceService.getUsers({ eventId: this.event.value }).subscribe(res => { this.evaluatorList = res.data });
    } else {
      this.evaluatorList = [];
      this.evaluator.value = null;
    };
  }

  checkIfIsOneAnswer(testTemplateId) {
    if (testTemplateId) {
      this.testTemplateService.getTestTemplate(testTemplateId).subscribe(res => {
        this.isTestTemplateOneAnswer = res.data.isOnlyOneAnswer;
        this.printTest = res.data.printTest;
      })
    } else {
      this.isTestTemplateOneAnswer = false;
    }

    if (!this.printTest)
      this.messageText = "Acest test poate conține video sau audio!"

    if (this.isTestTemplateOneAnswer) {
      this.evaluator.value = null;
    }
  }

  onItemChange(event) {
    this.showName = event.target.checked;
  }

  checkIfEventHasEvaluator(eventId) {
    if(eventId){
      this.eventService.getEvent(eventId).subscribe(res => this.hasEventEvaluator = res.data.isEventEvaluator)
    }
  }

  setTimeToSearch(): void {
    if (this.date) {
      const date = new Date(this.date);
      this.search = new Date(date.getTime() - (new Date(this.date).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  parse() {
    this.setTimeToSearch();
    return new AddEditTest({
      userProfileId: this.userListToAdd,
      programmedTime: this.search,
      eventId: this.solicitedTest.eventId || null,
      evaluatorId: +this.evaluator.value || null,
      testStatus: TestStatusEnum.Programmed,
      testTemplateId: this.solicitedTest.testTemplateId || 0,
      showUserName: this.showName
    })
  }

  createTest() {
    this.testService.createTest(this.parse()).subscribe(() => {
      this.changeStatus();

      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('tests.tests-were-programmed'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  changeStatus() {
    let params: any = {
      id: this.solicitedTestId,
      status: SolicitedTestStatusEnum.Approved
    }

    this.solicitedTestService.changeStatus(params).subscribe(res => this.backClicked());
  }

  backClicked() {
    this.location.back();
  }
}
