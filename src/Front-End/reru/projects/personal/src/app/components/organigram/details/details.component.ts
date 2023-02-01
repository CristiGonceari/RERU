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
    succesDelete: 'Organigram deleted!',
    genericError: 'There was an error deleting the organigram!',
    relationAdded: 'Relation added!',
    anError: 'An error occured!'
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
      modalRef.componentInstance.list = response.data && response.data.roles || [];
      modalRef.componentInstance.container = event;

      modalRef.result.then(response => this.takeAction(response.mode, response.data, event), () => {});
    });
  }

  takeAction(mode: number, data, event): void {
    switch(mode) {
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
      this.translate.get('organigram.organigram-deleted'),
      this.translate.get('organigram.organigram-error-delete'),
      this.translate.get('organigram.relation-added'),
      this.translate.get('organigram.an-error'),
    ]).subscribe(([success, error, succesDelete, genericError, relationAdded, anError]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.succesDelete = succesDelete;
      this.notification.genericError = genericError;
      this.notification.relationAdded = relationAdded;
      this.notification.anError = anError;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  createRelation(data): void {
    this.organigramService.relation(data).subscribe(response => {
      this.retrieveOrganigram(this.organigram.id);
        this.notificationService.success(this.notification.success, this.notification.relationAdded, NotificationUtil.getDefaultMidConfig());    
    }, (error) => {
      this.notificationService.error(this.notification.error, this.notification.anError, NotificationUtil.getDefaultConfig());
    });
  }

  deleteRelation(id: number): void {
    this.organigramService.deleteRelation(id).subscribe(() => {
      this.retrieveOrganigram(this.organigram.id);
      this.notificationService.success(this.notification.success, this.notification.succesDelete, NotificationUtil.getDefaultConfig());
    });
  }

  openOrganigramDeleteModal(): void {
    const modalRef = this.modalService.open(DeleteOrganigramModalComponent);
    modalRef.result.then(() => this.deleteOrganigram(this.organigram.id), () => {});
  }

  deleteOrganigram(id: number): void {
    this.isLoading = true;
    this.organigramService.delete(id).subscribe(() => {
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      this.notificationService.success(this.notification.success, this.notification.succesDelete, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      this.notificationService.error(this.notification.error, this.notification.genericError, NotificationUtil.getDefaultMidConfig());
    })
  }
}
