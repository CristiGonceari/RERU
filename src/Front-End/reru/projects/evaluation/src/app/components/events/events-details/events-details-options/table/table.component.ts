import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { EventTestTemplateService } from 'projects/evaluation/src/app/utils/services/event-test-template/event-test-template.service';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';

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
  testTemplates: boolean = true;
  locations: boolean = true;
  evaluators: boolean = true;
  person: boolean = true;
  user: boolean = true;

  currentUrl;
  enum = TestingLocationTypeEnum;
  fields = [];

  title: string;
	description: string;
	no: string;
	yes: string;

  constructor(private eventService: EventService,
    private route: ActivatedRoute,
		public translate: I18nService,
    private router: Router,
    private eventTestTemplateService: EventTestTemplateService,
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
      this.eventTestTemplateService.getTestTemplates(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.testTemplates = false;
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
    forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('modal.delete-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
    modalRef.componentInstance.title = 'Delete';
    modalRef.componentInstance.description = `${this.description} ${this.currentUrl} ?`;
    modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
    if (this.locations == false) {
      modalRef.result.then(() => this.detachLocation(eventId, itemId), () => { });
    }

    if (this.testTemplates == false) {
      modalRef.result.then(() => this.detachTestTemplate(eventId, itemId), () => { });
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
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('events.succes-remove-location-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachTestTemplate(eventId, testTemplateId) {
    this.eventTestTemplateService.detachTestTemplate(eventId, testTemplateId).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-remove-test-type-msg'),
        ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachUser(eventId, userProfileId) {
    this.eventService.detachUser(eventId, userProfileId).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-remove-user-msg'),
        ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachEvaluator(eventId, evaluatorId) {
    this.eventService.detachEvaluator(eventId, evaluatorId).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-detach-evaluator-msg'),
        ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }

  detachPerson(eventId, userProfileId){
    this.eventService.detachPerson(eventId, userProfileId).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-remove-person-msg'),
        ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    });
  }
}
