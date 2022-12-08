import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ObjectUtil, EvaluationListModel } from '@utils';
import { EvaluationService,  } from '@utils/services';
import { ConfirmDeleteEvaluationModalComponent } from '../../../utils/modals/confirm-delete-evaluation-modal/confirm-delete-evaluation-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PaginationClass, PaginationModel } from '../../../utils/models/pagination.model';

@Component({
  selector: 'app-evaluations-table',
  templateUrl: './evaluations-table.component.html',
  styleUrls: ['./evaluations-table.component.scss']
})
export class EvaluationsTableComponent implements OnInit {
  @Input() evaluateType: number;
  isLoading: boolean = true;
  evaluations: EvaluationListModel[] = [];
  pagedSummary: PaginationModel = new PaginationClass();
  includeAll: boolean;
  constructor(private evaluationService: EvaluationService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.processTypeEvaluation(+this.evaluateType);
  }

  get isActionsEnabled(): boolean {
    if (!this.evaluations.length) {
      return false;
    }

    return this.evaluations.some((el: EvaluationListModel) => {
      return (el.canDelete || el.canEvaluate || el.canCounterSign || el.canDownload);
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

  openConfirmEvaluationDeleteModal(id: number): void {
    const modalRef = this.modalService.open(ConfirmDeleteEvaluationModalComponent, { centered: true });
    modalRef.result.then(() => this.deleteEvaluation(id),() => {});
  }

  deleteEvaluation(id: number): void {
    this.evaluationService.delete(id).subscribe(response => {
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
