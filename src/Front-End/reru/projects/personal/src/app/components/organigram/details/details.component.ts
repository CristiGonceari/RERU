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
  constructor(private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private router: Router,
              private ngZone: NgZone,
              private departmentContentService: DepartmentContentService) { }

  ngOnInit(): void {
    this.subscribeForRouteChanges();
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
    this.departmentContentService.getCalculated(event.id).subscribe(response => {
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

  createRelation(data): void {
    this.organigramService.relation(data).subscribe(response => {
      this.retrieveOrganigram(this.organigram.id);
      this.notificationService.success('Success', 'Relation added!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'An error occured!', NotificationUtil.getDefaultConfig());
    });
  }

  deleteRelation(id: number): void {
    this.organigramService.deleteRelation(id).subscribe(() => {
      this.retrieveOrganigram(this.organigram.id);
      this.notificationService.success('Success', 'Relation deleted!', NotificationUtil.getDefaultConfig());
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
      this.notificationService.success('Success', 'Organigram deleted!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error deleting the organigram!', NotificationUtil.getDefaultConfig());
    })
  }
}
