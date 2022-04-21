import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { AddEditSolicitedTest } from 'projects/evaluation/src/app/utils/models/solicitet-tests/add-edit-solicited-test.model';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { CandidatePositionService } from 'projects/evaluation/src/app/utils/services/candidate-position/candidate-position.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-edit-solicited-test',
  templateUrl: './add-edit-solicited-test.component.html',
  styleUrls: ['./add-edit-solicited-test.component.scss']
})
export class AddEditSolicitedTestComponent implements OnInit {
  eventsList: [] = [];
  selectActiveTests: [] = [];
  user = new SelectItem();
  event = new SelectItem();
  testTemplate = new SelectItem();
  date: Date;
  search: string;
  title: string;
  description: string;
  isLoading: boolean = true;
  solicitedTestId;
  solicitedTest: AddEditSolicitedTest;
  candidatePositions = new SelectItem();

  constructor(
    private referenceService: ReferenceService,
    private testTemplateService: TestTemplateService,
    private solicitedTestService: SolicitedTestService,
    public translate: I18nService,
    private location: Location,
    private notificationService: NotificationsService,
    public activatedRoute: ActivatedRoute,
    private candidatePosition: CandidatePositionService,
  ) { }

  ngOnInit(): void {
    this.initData();
    this.getEvents();
    this.retrievePositions();
  }

  initData(): void {
    this.solicitedTestId = this.activatedRoute.snapshot.paramMap.get('id');
    if (this.solicitedTestId != null) this.getSolicitedTest(this.solicitedTestId)
    else this.isLoading = false;
  }

  getSolicitedTest(id) {
    this.solicitedTestService.getMySolicitedTest(id).subscribe(res => {
      if (res && res.data) {
        this.solicitedTest = res.data;
        this.event.value = res.data.eventId;
        this.testTemplate.value = res.data.testTemplateId;
        this.date = res.data.solicitedTime;
        this.candidatePositions.value = res.data.candidatePositionId;
        this.isLoading = false;
        console.log(this.solicitedTest)
      }
    });
  }

  retrievePositions(){
    this.candidatePosition.getPositionValues().subscribe((res) => (
        this.candidatePositions = res.data
      ));
  }

  getEvents() {
    this.referenceService.getEvents().subscribe(res => {
      this.eventsList = res.data;
      this.getActiveTestTemplate();
    });
  }

  getActiveTestTemplate() {
    let params = {
      testTemplateStatus: TestTemplateStatusEnum.Active,
      eventId: this.event.value || null
    }

    this.testTemplateService.getTestTemplateByStatus(params).subscribe((res) => {
      this.selectActiveTests = res.data;
    })
  }

  onSave(): void {
    if (this.solicitedTestId) {
      this.edit();
    } else {
      this.add();
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
    if (this.solicitedTestId != null) {
      return {
        data: {
          id: this.solicitedTestId,
          solicitedTime: this.search,
          eventId: +this.event.value || null,
          testTemplateId: +this.testTemplate.value || 0,
          solicitedTestStatus: this.solicitedTest.solicitedTestStatus,
          candidatePositionId : this.candidatePositions.value || 0
        }
      };
    } else {
      return {
        data: {
          solicitedTime: this.search,
          eventId: +this.event.value || null,
          testTemplateId: +this.testTemplate.value || 0,
          candidatePositionId : this.candidatePositions.value || 0
        }
      };
    }
  }

  add() {
    this.solicitedTestService.addMySolicitedTest(this.parse()).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('solicited-test.succes-add-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  edit() {
    this.solicitedTestService.editMySolicitedTest(this.parse()).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('solicited-test.succes-edit-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.location.back();
  }
}
