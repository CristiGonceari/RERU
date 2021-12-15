import { Component, Input, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Location } from '@angular/common';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { TestTypeStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-type-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AddEditTest } from '../../../../utils/models/tests/add-edit-test.model';
import { TestQuestionService } from 'projects/evaluation/src/app/utils/services/test-question/test-question.service';

@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.scss']
})
export class AddTestComponent implements OnInit {
  usersList: any;
  eventsList: any;
  selectActiveTests: any;
  evaluatorList: [] = [];
  user = new SelectItem();
  event = new SelectItem();
  testType = new SelectItem();
  evaluator = new SelectItem();
  testTypeEvaluator: any;
  eventEvaluator: any;
  needEvaluator: boolean = false;
  hasEventEvaluator: boolean = false;
  date: Date;
  search: string;
  showName: boolean = false;
  isTestTypeOneAnswer: boolean = false;
  @Input() testEvent: boolean;


  constructor(
    private referenceService: ReferenceService,
    private testTypeService: TestTypeService,
    private testService: TestService,
    private location: Location,
    private notificationService: NotificationsService,
    private testQuestionService: TestQuestionService
  ) { }

  ngOnInit(): void {
    this.getUsers();
    this.getEvents();
    this.getEvaluators();
  }

  getUsers() {
    this.referenceService.getUsers({ eventId: this.event.value }).subscribe(res => {
      this.usersList = res.data
    });
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data
      this.getActiveTestType();
    });
  }

  getEvaluators() {
    if (this.needEvaluator === false && this.hasEventEvaluator === false) {
      this.referenceService.getUsers({ eventId: this.event.value }).subscribe(res => { this.evaluatorList = res.data });
    }
    else {
      this.evaluatorList = [];
      this.evaluator.value = null;
    };
  }

  setTimeToSearch(): void {
    if (this.date) {
      const date = new Date(this.date);
      this.search = new Date(date.getTime() - (new Date(this.date).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  getActiveTestType() {
    let params = {
      testTypeStatus: TestTypeStatusEnum.Active,
      eventId: this.event.value
    }

    this.testTypeService.getTestTypeByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data
    })
    this.getUsers();
  }

  checkIfIsOneAnswer($event) {
    this.isTestTypeOneAnswer = this.selectActiveTests.find(x => x.testTypeId === $event).isOnlyOneAnswer
    if(this.isTestTypeOneAnswer){
      this.evaluator.value = null;
    }

  }

  checkIfEventHasEvaluator($event) {
    this.hasEventEvaluator = this.eventsList.find(x => x.eventId === $event).isEventEvaluator
  }


  parse() {
    this.setTimeToSearch();
    return {
      data: new AddEditTest({
        userProfileId: +this.user.value || 0,
        programmedTime: this.search,
        eventId: +this.event.value || null,
        evaluatorId: +this.evaluator.value || null,
        testStatus: TestStatusEnum.Programmed,
        testTypeId: +this.testType.value || 0,
        showUserName: this.showName
      })
    }
  }

  generate(testId){
    this.testQuestionService.generate(testId).subscribe(() => {});
  }


  createTest() {
    this.testService.createTest(this.parse()).subscribe((res) => {
      this.backClicked();
      this.generate(res.data);
      this.notificationService.success('Success', 'Test was successfully programmed', NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.location.back();
  }

  onItemChange(event) {
    this.showName = event.target.checked;
  }
}
