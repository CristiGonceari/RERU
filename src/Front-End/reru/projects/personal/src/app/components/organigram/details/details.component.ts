import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteOrganigramModalComponent } from '../../../utils/modals/delete-organigram-modal/delete-organigram-modal.component';
import { OrganigramActionModalComponent } from '../../../utils/modals/organigram-action-modal/organigram-action-modal.component';
import { OrganigramService } from '../../../utils/services/organigram.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DepartmentContentService } from '../../../utils/services/department-content.service';
import { NULL_EXPR } from '@angular/compiler/src/output/output_ast';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';


@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  organigram: any;
  nodes: any;
  view: number = 2;

  notification = {
    success: 'Success',
    error: 'Error',
    successAddChart: 'Organigram item has been successfully added',
    errorAddChart: 'Organigram item was not been added',
    successDelete: 'Organigram was deleted',
    errorDelete: 'Organigram was not deleted',
    successDeleteChart: 'Organigram item was deleted successfully',
    errorDeleteChart: 'This organizational item cannot be deleted because it has sub-item/s',
  };

  constructor(private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private router: Router,
              private ngZone: NgZone,
              public translate: I18nService,
              private departmentContentService: DepartmentContentService) { }

  ngOnInit(): void {
    this.subscribeForRouteChanges();
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  retrieveOrganizationalChart(id: number): void {
    this.organigramService.get(id).subscribe(response => {
      this.organigram = response.data;
    });
  }

  subscribeForRouteChanges(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrieveOrganizationalChart(response.id);
        this.retrieveOrganigram(response.id);
      }
    })
  }

  retrieveOrganigram(id: number): void {
    this.organigramService.chart(id).subscribe((response: any) => {
      this.nodes = [response.data];
      this.isLoading = false;
    });
  }

  openOrganigramActionModal(event): void {
    this.departmentContentService.getCalculated(event.id, event.type).subscribe(response => {
      const modalRef = this.modalService.open(OrganigramActionModalComponent);
      modalRef.componentInstance.type = +event.type;
      modalRef.componentInstance.id = this.organigram.id;
      modalRef.componentInstance.users = response.data || [];
      modalRef.componentInstance.container = event;

      modalRef.result.then(response => this.takeAction(response.mode, response.data, event), () => { });
    });
  }

  takeAction(mode: number, data, event): void {
    switch (mode) {
      case 1:
        data.organizationalChartId = this.organigram.id;
        data.parentId = event.id;
        this.createRelation(data);
        break;
      case 2:
        data.organizationalChartId = this.organigram.id;
        this.deleteRelation(event.relationId);
        break;
    }
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('organigram.succes-add-chart-organigram'),
      this.translate.get('organigram.error-add-chart-organigram'),
      this.translate.get('organigram.succes-delete-organigram'),
      this.translate.get('organigram.error-delete-organigram'),
      this.translate.get('organigram.succes-delete-chart-organigram'),
      this.translate.get('organigram.error-delete-chart-organigram')

    ]).subscribe(([success, error, successAddChart, errorAddChart, successDelete, errorDelete, successDeleteChart, errorDeleteChart]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.successAddChart = successAddChart;
      this.notification.errorAddChart = errorAddChart;
      this.notification.successDelete = successDelete;
      this.notification.errorDelete = errorDelete;
      this.notification.successDeleteChart = successDeleteChart;
      this.notification.errorDeleteChart = errorDeleteChart;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  createRelation(data): void {
    this.organigramService.relation(data).subscribe(response => {
      this.retrieveOrganigram(this.organigram.id);
      this.notificationService.success(this.notification.success, this.notification.successAddChart, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.notificationService.error(this.notification.error, this.notification.errorAddChart, NotificationUtil.getDefaultMidConfig());
    });
  }

  deleteRelation(id: number): void {
    this.organigramService.deleteRelation(id).subscribe(() => {
      this.retrieveOrganigram(this.organigram.id);
      this.notificationService.success(this.notification.success, this.notification.successDeleteChart, NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error(this.notification.error, this.notification.errorDeleteChart, NotificationUtil.getDefaultMidConfig());
    });
  }

  openOrganigramDeleteModal(): void {
    const modalRef = this.modalService.open(DeleteOrganigramModalComponent);
    modalRef.result.then(() => this.deleteOrganigram(this.organigram.id), () => { });
  }

  deleteOrganigram(id: number): void {
    this.isLoading = true;
    this.organigramService.delete(id).subscribe(() => {
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      this.notificationService.success(this.notification.success, this.notification.successDelete, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.notificationService.error(this.notification.error, this.notification.errorDelete, NotificationUtil.getDefaultMidConfig());
    })
  }
}
