import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { EventTestTypeService } from 'projects/evaluation/src/app/utils/services/event-test-type/event-test-type.service';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AttachLocationToEventModel } from 'projects/evaluation/src/app/utils/models/locations/attachTestTypeToEventModel';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {
  @Input() category: string;
  @Input() importedId: number;
  
  pagination: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  testTypes: boolean = true;
  locations: boolean = true;
  evaluators: boolean = true;
  person: boolean = true;
  user: boolean = true;

  currentUrl;
  enum = TestingLocationTypeEnum;
  fields = [];

  constructor(private eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private eventTestTypeService: EventTestTypeService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
  ) { }

  ngOnInit(): void {
    this.list();
    this.currentUrl = this.router.url.split("/").pop();
  }

  list(data: any = {}) {
    let params = {
      eventId: this.importedId,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
    }

    if (this.category == "users") {
      this.eventService.getUsers(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.user = false;
        }
      });
    }

    if (this.category == "test-types") {
      this.eventTestTypeService.getTestTypes(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.testTypes = false;
        }
      });
    }

    if (this.category == "locations") {
      this.eventService.getLocations(params).subscribe(
        res => {
          if (res && res.data) {
            this.fields = res.data.items;
            this.pagination = res.data.pagedSummary;
            this.isLoading = false;
            this.locations = false;
          }
        }
      )
    }

    if (this.category == "evaluators") {
      this.eventService.getEvaluators(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.evaluators = false;
        }

      });
    }

    if (this.category == "persons") {
      this.eventService.getResponsiblePersons(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.person = false;
        }

      });
    }
  }
  openConfirmationDeleteModal(eventId: number, itemId): void {

    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = 'Delete';
    modalRef.componentInstance.description = `Are you sure you want to delete this ${this.currentUrl} ?`;
    if (this.locations == false) {
      modalRef.result.then(() => this.detachLocation(eventId, itemId), () => { });
    }

    if (this.testTypes == false) {
      modalRef.result.then(() => this.detachTestType(eventId, itemId), () => { });
    }

    if (this.user == false) {
      modalRef.result.then(() => this.detachUser(eventId, itemId), () => { });
    }

    if (this.evaluators == false) {
      modalRef.result.then(() => this.detachEvaluator(eventId, itemId), () => { });
    }

    if(this.person == false){
      modalRef.result.then(() => this.detachPerson(eventId, itemId), () => { });
    }
  }

  detachLocation(eventId, locationId) {
    this.eventService.detachLocation(eventId, locationId).subscribe(() => {
      this.notificationService.success('Success', 'Location was successfully detached', NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachTestType(eventId, testTypeId) {
    this.eventTestTypeService.detachTestType(eventId, testTypeId).subscribe(() => {
      this.notificationService.success('Success', 'Test type was successfully detached', NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachUser(eventId, userProfileId) {
    this.eventService.detachUser(eventId, userProfileId).subscribe(() => {
      this.notificationService.success('Success', 'User was successfully detached', NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachEvaluator(eventId, evaluatorId) {
    this.eventService.detachEvaluator(eventId, evaluatorId).subscribe(() => {
      this.notificationService.success('Success', 'Evaluator was successfully detached', NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachPerson(eventId, userProfileId){
    this.eventService.detachPerson(eventId, userProfileId).subscribe(() => {
			this.notificationService.success('Success', 'Person was successfully detached', NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }




}
