import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ObjectUtil } from '../../../utils/util/object.util';
import { SurveyService } from '../../../utils/services/survey.service';
import { ConfirmDeleteSurveyModalComponent } from '../../../utils/modals/confirm-delete-survey-modal/confirm-delete-survey-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-survey-table',
  templateUrl: './survey-table.component.html',
  styleUrls: ['./survey-table.component.scss']
})
export class SurveyTableComponent implements OnInit {
  @Input() evaluateType: number;
  isLoading: boolean = true;
  surveys: any[] = [];
  pagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  includeAll: boolean;
  constructor(private surveyService: SurveyService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.processTypeEvaluation(this.evaluateType);
  }

  processTypeEvaluation(type: number) {
    switch(type) {
      case 0: this.list(); break;
      case 1: this.listEvaluations(); break;
      case 2: this.listAutoevaluations(); break;
      case 3: this.listCountersign(); break;
    }
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = {
      ...data,
      includeAll: this.includeAll,
      page: data.page || this.pagedSummary.currentPage
    }
    this.surveyService.listMine(ObjectUtil.preParseObject(request)).subscribe(response => {
      this.surveys = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  listEvaluations(): void {
    this.isLoading = true;
    this.surveyService.listEvaluation({}).subscribe(response => {
      this.surveys = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  listAutoevaluations(): void {
    this.isLoading = true;
    this.surveyService.listAutoevaluation({}).subscribe(response => {
      this.surveys = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  listCountersign(): void {
    this.isLoading = true;
    this.surveyService.listCountersign({}).subscribe(response => {
      this.surveys = response;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  openConfirmSurveyDeleteModal(id: number): void {
    const modalRef = this.modalService.open(ConfirmDeleteSurveyModalComponent, { centered: true });
    modalRef.result.then(() => this.deleteSurvey(id),() => {

    });
  }

  deleteSurvey(id: number): void {
    this.surveyService.delete(id).subscribe(response => {
      this.notificationService.success('Success', 'Fisa a fost ștearsă cu succes', NotificationUtil.getDefaultConfig());
      this.processTypeEvaluation(this.evaluateType);
    }, error => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Something went wrong!', NotificationUtil.getDefaultConfig());
      }

      this.notificationService.error('Error', 'An error occured!', NotificationUtil.getDefaultConfig());
    });
  }

}
