import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { TestStatusEnum } from '../../../utils/enums/test-status.enum';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestService } from '../../../utils/services/test/test.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { AddEditTest } from '../../../utils/models/tests/add-edit-test.model';
import { Location } from '@angular/common';
import { TestTypeStatusEnum } from '../../../utils/enums/test-type-status.enum';

@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.scss']
})
export class AddTestComponent implements OnInit {

  usersList: any;
  // eventsList: SelectItem[] = [{ label: '', value: '' }];
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
  

  constructor(
    private referenceService: ReferenceService,
    private testTypeService: TestTypeService,
    private testService: TestService,
    private location: Location,
    private notificationService: NotificationsService
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
      this.referenceService.getUsers({ eventId: this.event.value }).subscribe(res => {this.evaluatorList = res.data});
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

  checkIfIsOneAnswer($event){
    this.isTestTypeOneAnswer = this.selectActiveTests.find(x => x.testTypeId === $event).isOnlyOneAnswer
  }

  checkIfEventHasEvaluator($event){
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

  createTest() {
    this.testService.createTest(this.parse()).subscribe(() => {
      this.backClicked();
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
