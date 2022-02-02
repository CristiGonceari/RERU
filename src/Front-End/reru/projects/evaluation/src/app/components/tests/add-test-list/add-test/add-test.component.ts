import { Component, Input, OnInit, ViewChild } from '@angular/core';
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
import { FormControl} from '@angular/forms';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { AssignedUsers } from '../../../../utils/models/tests/assigned-users';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.scss'],
})
export class AddTestComponent implements OnInit {
  @ViewChild('autoCompleteInput', { read: MatAutocompleteTrigger }) autoCompleteElement: MatAutocompleteTrigger;
  @Input() testEvent: boolean;

  usersList: any;
  eventsList: any;
  selectActiveTests: any;
  testTypeEvaluator: any;
  eventEvaluator: any;

  title: string;
	description: string;

  evaluatorList: [] = [];
  userListToAdd:number[] = [];
  displayUserNames: AssignedUsers[] = []; 

  user = new SelectItem();
  event = new SelectItem();
  testType = new SelectItem();
  evaluator = new SelectItem();
  
  showName: boolean = false;
  isTestTypeOneAnswer: boolean = false;
  printTest: boolean = true;
  needEvaluator: boolean = false;
  hasEventEvaluator: boolean = false;
  isListOrderedAsc: boolean = false;

  date: Date;
  search: string;
  key: string = '';
  messageText = "";
  
  myControl = new FormControl();

  constructor(
    private referenceService: ReferenceService,
    private testTypeService: TestTypeService,
    private testService: TestService,
	  public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    private printService: PrintTemplateService
  ) { }

  ngOnInit(): void {
    this.getUsers();
    this.getEvents();
    this.getEvaluators();
  }

  getUsers(id?: number) {
    if (!id)
    this.referenceService.getUsers({name: this.key, eventId: this.event.value }).subscribe(res => {
      this.usersList = res.data; 
    }); 
    else {
      this.updateUserList(id);
      this.key = '';
      this.autoCompleteElement.openPanel();
    }
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

  checkIfIsOneAnswer(event) {
    if (event) {
      this.isTestTypeOneAnswer = this.selectActiveTests.find(x => x.testTypeId === event).isOnlyOneAnswer;
      this.printTest = this.selectActiveTests.find(x => x.testTypeId === event).printTest;
    }
    else this.isTestTypeOneAnswer = false;
    
    if(!this.printTest) 
      this.messageText = "Acest test poate conține video sau audio!"

    if(this.isTestTypeOneAnswer){
      this.evaluator.value = null;
    }
  }

  checkIfEventHasEvaluator($event) {
    this.hasEventEvaluator = this.eventsList.find(x => x.eventId === $event).isEventEvaluator
  }

  deleteFromList(user, index) {
    let indexOfIdToDelete = this.userListToAdd.findIndex(x => x == this.displayUserNames[index].value);
    this.displayUserNames.splice(index, 1);
    this.userListToAdd.splice(indexOfIdToDelete, 1);
    this.usersList.push( new AssignedUsers(user.value, user.label));
  }

  updateUserList(id?: number): void {
    let indexToDelete = this.usersList.map(el => el.value).findIndex(x => x == id);
    this.usersList.splice(indexToDelete, 1);
  }

  addUserToList(id: number, name: string) {
    if(!this.userListToAdd.includes(+id))
    this.displayUserNames.push(
      new AssignedUsers(id, name)
    );
    this.userListToAdd.push(+id);
    this.getUsers(id)
  }

  sortByName(){
    if(!this.isListOrderedAsc){
      this.displayUserNames.sort( (a,b)=> a.label > b.label ? 1:-1 );
      this.isListOrderedAsc = true;
    } else {
      this.displayUserNames.sort( (a,b)=> a.label > b.label ? -1:1 );
      this.isListOrderedAsc = false;
    }
  }

  parse() {
    this.setTimeToSearch();
    return  new AddEditTest({
        userProfileId: this.userListToAdd,
        programmedTime: this.search,
        eventId: +this.event.value || null,
        evaluatorId: +this.evaluator.value || null,
        testStatus: TestStatusEnum.Programmed,
        testTypeId: +this.testType.value || 0,
        showUserName: this.showName
      })
  }

  createTest() {
    this.testService.createTest(this.parse()).subscribe((res) => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.tests-were-programmed'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  createTestAndPrint() {
    this.testService.createTest(this.parse()).subscribe((res) => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.tests-were-programmed'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.performingTestPdf(res.data)
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.location.back();
  }

  onItemChange(event) {
    this.showName = event.target.checked;
  }

  performingTestPdf(testIds) {
    this.printService.getPerformingTestPdf({testsIds: testIds}).subscribe((response: any) => {
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
