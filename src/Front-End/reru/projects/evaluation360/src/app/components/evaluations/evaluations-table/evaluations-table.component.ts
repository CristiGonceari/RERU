import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ObjectUtil, EvaluationListModel } from '@utils';
import { EvaluationService, I18nService,  } from '@utils/services';
import { ConfirmDeleteEvaluationModalComponent } from '../../../utils/modals/confirm-delete-evaluation-modal/confirm-delete-evaluation-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PaginationClass, PaginationModel } from '../../../utils/models/pagination.model';
import { EvaluationStatusEnum } from '../../../utils/models/evaluation-status.enum';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-evaluations-table',
  templateUrl: './evaluations-table.component.html',
  styleUrls: ['./evaluations-table.component.scss']
})
export class EvaluationsTableComponent implements OnInit, AfterViewInit {
  @Input() evaluateType: number;
  @Input() showHighlighted: boolean;
  isLoading: boolean = true;
  evaluations: EvaluationListModel[] = [];
  pagedSummary: PaginationModel = new PaginationClass();
  includeAll: boolean;
  EvaluationStatusEnum = EvaluationStatusEnum;
  headersHtml: HTMLCollectionOf<HTMLTableCellElement>;
  notification = {
    success: 'Success', 
    warning: "Warning",
    error: 'Error',
    deleteEvaluation: 'Evaluation was deleted successfully!',
    warningBody: 'Something went wrong!',
    anError: 'There has been an error!'
  };

  constructor(private evaluationService: EvaluationService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              public translate: I18nService) { }

  ngOnInit(): void {
    this.processTypeEvaluation(+this.evaluateType);
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  ngAfterViewInit(): void {
    this.headersHtml = document.getElementsByTagName('th');
  }

  get isActionsEnabled(): boolean {
    if (!this.evaluations.length) {
      return false;
    }

    return this.evaluations.some((el: EvaluationListModel) => {
      return (el.canDelete || el.canAccept  || el.canFinished|| el.canEvaluate || el.canCounterSign || el.canDownload);
    })
  }

  set isActionsEnabled(value: boolean) {}

  processTypeEvaluation(type: number) {
    switch(type) {
      case 0: this.list(); break;
      // case 1: this.listEvaluations(); break;
      // case 2: this.listAutoevaluations(); break;
      // case 3: this.listCountersign(); break;
    }
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      ...data,
      includeAll: this.includeAll,
      page: data.page || this.pagedSummary.currentPage
    })
    this.evaluationService.listMine(ObjectUtil.preParseObject(request)).subscribe((response: any) => {
      this.evaluations = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  // listEvaluations(): void {
  //   this.isLoading = true;
  //   this.evaluationService.listEvaluation({}).subscribe(response => {
  //     this.surveys = response;
  //     this.isLoading = false;
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // listAutoevaluations(): void {
  //   this.isLoading = true;
  //   this.evaluationService.listAutoevaluation({}).subscribe(response => {
  //     this.surveys = response;
  //     this.isLoading = false;
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  // listCountersign(): void {
  //   this.isLoading = true;
  //   this.evaluationService.listCountersign({}).subscribe(response => {
  //     this.surveys = response;
  //     this.isLoading = false;
  //   }, () => {
  //     this.isLoading = false;
  //   });
  // }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.title.success'),
      this.translate.get('notification.title.warning'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.body.success.delete-evaluation'),
      this.translate.get('notification.body.warning'),
      this.translate.get('notification.body.error'),
    ]).subscribe(([success, warning, error, deleteEvaluation, warningBody, anError]) => {
      this.notification.success = success;
      this.notification.warning = warning;
      this.notification.error = error;
      this.notification.deleteEvaluation = deleteEvaluation;
      this.notification.warningBody = warningBody;
      this.notification.anError = anError;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  openConfirmEvaluationDeleteModal(id: number): void {
    const modalRef = this.modalService.open(ConfirmDeleteEvaluationModalComponent, { centered: true });
    modalRef.result.then(() => this.deleteEvaluation(id),() => {});
  }

  deleteEvaluation(id: number): void {
    this.evaluationService.delete(id).subscribe(response => {
      this.notificationService.success(this.notification.success, this.notification.deleteEvaluation, NotificationUtil.getDefaultConfig());
      this.processTypeEvaluation(this.evaluateType);
    }, error => {
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.warningBody, NotificationUtil.getDefaultConfig());
      }

      this.notificationService.error(this.notification.error, this.notification.anError, NotificationUtil.getDefaultConfig());
    });
  }

}
