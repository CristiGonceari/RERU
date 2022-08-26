import { Component, OnInit } from '@angular/core';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { SolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/solicited-test.model';
import { Location } from '@angular/common';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { ActivatedRoute } from '@angular/router';
import { AddEditTest } from 'projects/evaluation/src/app/utils/models/tests/add-edit-test.model';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { SolicitedVacantPositionUserFileService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-user-file/solicited-vacant-position-user-file.service';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ReviewSolicitedVacandPositionModalComponent } from 'projects/evaluation/src/app/utils/modals/review-solicited-vacand-position-modal/review-solicited-vacand-position-modal.component';
import { SolicitedVacantPositionEmailMessageService } from 'projects/evaluation/src/app/utils/services/solicited-vacant-position-email-message/solicited-vacant-position-email-message.service';

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

  candidatePositionId;

  messageText = "";

  userListToAdd: number[] = [];

  userFiles: any[] = [];

  constructor(
    private referenceService: ReferenceService,
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    public activatedRoute: ActivatedRoute,
    private modalService: NgbModal,
    private testTemplateService: TestTemplateService,
    private solicitedVacantPositionUserFileService: SolicitedVacantPositionUserFileService,
    private solicitedVacantPositionEmailMessageService: SolicitedVacantPositionEmailMessageService
  ) { }

  ngOnInit(): void {
    this.getEvaluators();
    this.getEvents();
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data;
      this.getActiveTestTemplate();

    });
  }

  GetFile(fileId: string) {
    this.solicitedVacantPositionUserFileService.get(fileId).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        const fileNameParsed = fileName.substring(1, fileName.length - 1);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileNameParsed, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

  initData(): void {
    this.solicitedTestId = this.activatedRoute.snapshot.paramMap.get('id');
    this.candidatePositionId = this.activatedRoute.snapshot.paramMap.get('positionId');
    if (this.solicitedTestId != null) this.getSolicitedTest(this.solicitedTestId, this.candidatePositionId)
    else this.isLoading = false;
  }

  getSolicitedTest(id, positionId) {
    this.solicitedTestService.getSolicitedTest({ id: id, candidatePositionId: positionId }).subscribe(res => {
      if (res && res.data) {
        this.solicitedTest = res.data;
        this.date = res.data.solicitedTime;
        this.isLoading = false;
        this.checkIfEventHasEvaluator(res.data.eventId);
        this.checkIfIsOneAnswer(res.data.testTemplateId)
        this.userListToAdd.push(res.data.userProfileId);
      }
      this.solicitedVacantPositionUserFileService.getList({ userId: this.solicitedTest.userProfileId, solicitedVacantPositionId: this.solicitedTestId, candidatePositionId: this.solicitedTest.candidatePositionId }).subscribe(res => {
        this.userFiles = res.data
      })
    });

  }

  getActiveTestTemplate() {
    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active
    }

    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data;
      this.initData();
    })
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
      this.isTestTemplateOneAnswer = this.selectActiveTests.find(x => x.testTemplateId === testTemplateId).isOnlyOneAnswer;
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

  checkIfEventHasEvaluator(event) {
    if (event) {
      this.hasEventEvaluator = this.eventsList.find(x => x.eventId === event).isEventEvaluator;
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
    const modalRef: any = this.modalService.open(ReviewSolicitedVacandPositionModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.userEmail = this.solicitedTest.email;
    modalRef.componentInstance.userName = this.solicitedTest.userProfileName;
    modalRef.result.then(() => {
      this.sendEmailAndChangeStatus( modalRef.result.__zone_symbol__value)
    }, () => { });
  }

  sendEmailAndChangeStatus(data) {
    let request = {
      emailMessage: data.EmailMessage,
      result: data.messageEnum,
      solicitedVacantPositionId: this.solicitedTestId
    }

    this.solicitedVacantPositionEmailMessageService.changeStatusAndSendEmail(request).subscribe(res => {
      this.backClicked();
    })
  }

  ceckFileNameLength(fileName: string) {
    return fileName.length <= 20 ? fileName : fileName.slice(0, 20) + "...";
  }

  backClicked() {
    this.location.back();
  }
}
