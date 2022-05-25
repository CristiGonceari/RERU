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
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { NgtscCompilerHost } from '@angular/compiler-cli/src/ngtsc/file_system';
import { Subscription, timer } from 'rxjs';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-add-test',
  templateUrl: './add-test.component.html',
  styleUrls: ['./add-test.component.scss'],
})
export class AddTestComponent implements OnInit {
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

  toolBarValue: number = 0;
  toolBarProcents: number = 0;

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
    private eventService: EventService
  ) { }

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data;
      this.getActiveTestTemplate();
    });
  }

  setTimeToSearch(): void {
    if (this.date) {
      const date = new Date(this.date);
      this.search = new Date(date.getTime() - (new Date(this.date).getTimezoneOffset() * 60000)).toISOString();
    }
  }

  getActiveTestTemplate() {
    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active,
      eventId: this.event.value || null
    }

    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data;
    })

    if (params.eventId != null) {
      this.getEvent(params.eventId);
    }
    else {
      this.showEventCard = false;
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
  }

  checkIfEventHasEvaluator(event) {
    if (event)
      this.hasEventEvaluator = this.eventsList.find(x => x.eventId === event).isEventEvaluator;
  }

  parse() {
    this.setTimeToSearch();
    return new AddEditTest({
      userProfileId: this.userListToAdd,
      programmedTime: this.search || null,
      eventId: +this.event.value || null,
      evaluatorId: this.evaluatorList[0] || null,
      testStatus: TestStatusEnum.Programmed,
      processId: this.processId || null,
      testTemplateId: +this.testTemplate.value || 0,
      showUserName: this.showName
    })
  }

  // parseForBulkAdd() {
  //   this.setTimeToSearch();
  //   return new AddEditTest({
  //     userProfileId: this.getSubArray(0, 5, this.userListToAdd),
  //     programmedTime: this.search || null,
  //     eventId: +this.event.value || null,
  //     evaluatorId: this.evaluatorList[0] || null,
  //     testStatus: TestStatusEnum.Programmed,
  //     testTemplateId: +this.testTemplate.value || 0,
  //     showUserName: this.showName
  //   })
  // }


  roundUpNearest10(num) {
    return Math.ceil(num / 10) * 10;
  }

  getSubArray(idx, _length, _array) {
    return _array.slice(idx, idx + _length);
  }
  //Create test Recursion 
  // addTests() {
  //   this.isStartAddingTests = true;
  //   let requests = Math.ceil((this.userListToAdd.length / 5));
  //   this.createTest(requests);
  // }

  // createTest(requests: number) {
  //   if (this.userListToAdd.length > 0) {
  //     this.testService.createTest(this.parseForBulkAdd()).subscribe((res) => {
  //       this.userListToAdd.splice(0, 5);
  //       this.toolBarProcents++;
  //       this.toolBarValue = Math.round(this.toolBarProcents / requests * 100);

  // this.testService.sendEmailNotification({testIds: res.data}).subscribe();

  //       this.createTest(requests);
  //     });
  //   } else {

  //     this.isStartAddingTests = false;
  //     forkJoin([
  //       this.translate.get('modal.success'),
  //       this.translate.get('tests.tests-were-programmed'),
  //     ]).subscribe(([title, description]) => {
  //       this.title = title;
  //       this.description = description;
  //     });
  //     this.backClicked();
  //     this.disableBtn = false;
  //     this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());

  //   }
  // }

  //Create For Every 10 users
  // createTest() {
  //   this.disableBtn = true;
  //   let users = this.parse();

  //   for (let i = 0; i <= 10; i++) {

  //     if (i % 10 == 0 && i != 0 && users.userProfileId.length >= 10) {

  //       var tests = new AddEditTest({
  //         userProfileId: this.getSubArray(i - 10, 10, users.userProfileId),
  //         programmedTime: this.search,
  //         eventId: +this.event.value || null,
  //         evaluatorId: this.evaluatorList[0] || null,
  //         testStatus: TestStatusEnum.Programmed,
  //         testTemplateId: +this.testTemplate.value || 0,
  //         showUserName: this.showName
  //       })

  //       this.testService.createTest(tests).subscribe(() => {
  //         if (users.userProfileId.length == 0) {
  //           forkJoin([
  //             this.translate.get('modal.success'),
  //             this.translate.get('tests.tests-were-programmed'),
  //           ]).subscribe(([title, description]) => {
  //             this.title = title;
  //             this.description = description;
  //           });
  //           this.backClicked();
  //           this.disableBtn = false;


  //           this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
  //         }

  //       });

  //       users.userProfileId.splice(0, 10);

  //       i = 0;
  //     }
  //   }

  //   if (users.userProfileId.length < 10 && users.userProfileId.length > 0) {

  //     var tests = new AddEditTest({
  //       userProfileId: users.userProfileId,
  //       programmedTime: this.search,
  //       eventId: +this.event.value || null,
  //       evaluatorId: this.evaluatorList[0] || null,
  //       testStatus: TestStatusEnum.Programmed,
  //       testTemplateId: +this.testTemplate.value || 0,
  //       showUserName: this.showName
  //     })

  //     this.testService.createTest(tests).subscribe(() => {
  //       forkJoin([
  //         this.translate.get('modal.success'),
  //         this.translate.get('tests.tests-were-programmed'),
  //       ]).subscribe(([title, description]) => {
  //         this.title = title;
  //         this.description = description;
  //       });
  //       this.backClicked();
  //       this.disableBtn = false;

  //       this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
  //     });

  //   }
  // }

  //Create Test For All bulk
  createTest() {
    this.disableBtn = true
    this.isStartAddingTests = true;

    this.testService.startBulkAddProcess({ totalProcesses: this.parse().userProfileId.length }).subscribe(res => {
      this.processId = res.data;


      const interval = setInterval(() => {
        this.testService.getBulkImportProcess(this.processId).subscribe(res => {
          this.processProgress = res.data;
          this.toolBarValue = Math.round(this.processProgress.doneProcesses * 100 / this.processProgress.totalProcesses);
        })
      }, 10 * 300);


      this.testService.createTest(this.parse()).subscribe(() => {

        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('tests.tests-were-programmed'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });

        

        if (this.toolBarValue == 100) {
          this.testService.getBulkImportProcess(this.processId).subscribe(res => {
            this.processProgress = res.data;
            this.toolBarValue = this.processProgress.doneProcesses * 100 / this.processProgress.totalProcesses;
            console.log("this.toolbarValue:", this.toolBarValue)
            this.testService.getBulkImportResult(this.processProgress.fileId).subscribe(response => {
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

        clearInterval(interval);
        this.isStartAddingTests = true;
        this.backClicked();
        this.disableBtn = false;
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      });
    })
  }


  // addTestAndPrint(){
  //   this.isStartAddingTests = true;
  //   let requests = Math.ceil((this.userListToAdd.length / 5));
  //   this.createTestAndPrint(requests);
  // }

  // createTestAndPrint(requests) {
  //   this.disableBtn = true;
  //   if (this.userListToAdd.length > 0) {
  //     this.testService.createTest(this.parseForBulkAdd()).subscribe((res) => {
  //       this.userListToAdd.splice(0, 5);
  //       this.toolBarProcents++;
  //       this.toolBarValue = Math.round(this.toolBarProcents / requests * 100);
  //       this.performingTestPdf(res.data);
  //       this.createTestAndPrint(requests);
  //     });
  //   } else {
  //     this.isStartAddingTests = false;
  //     forkJoin([
  //       this.translate.get('modal.success'),
  //       this.translate.get('tests.tests-were-programmed'),
  //     ]).subscribe(([title, description]) => {
  //       this.title = title;
  //       this.description = description;
  //     });
  //     this.backClicked();
  //     this.disableBtn = false;
  //     this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
  //   }

  // }

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

  onItemChange(event) {
    this.showName = event.target.checked;
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
    this.openUsersModal(this.evaluatorList, 'radio');
  }

  attachUsers(): void {
    this.exceptUserIds = this.evaluatorList;
    this.openUsersModal(this.userListToAdd, 'checkbox');
  }

  openUsersModal(attachedItems, inputType): void {
    const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.exceptUserIds = this.exceptUserIds;
    modalRef.componentInstance.attachedItems = attachedItems;
    modalRef.componentInstance.inputType = inputType;
    modalRef.componentInstance.page = 'add-test';
    modalRef.componentInstance.eventId = +this.event.value;
    modalRef.result.then(() => {
      if (inputType == 'radio') this.evaluatorList = modalRef.result.__zone_symbol__value.attachedItems;
      else if (inputType == 'checkbox') this.userListToAdd = modalRef.result.__zone_symbol__value.attachedItems;
    }, () => { });
  }

}
