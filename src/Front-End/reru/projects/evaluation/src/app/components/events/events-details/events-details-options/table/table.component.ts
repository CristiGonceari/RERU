import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { PrintTableService } from 'projects/evaluation/src/app/utils/services/print-table/print-table.service';

import { EventUsersService } from 'projects/evaluation/src/app/utils/services/event-users/event-users.service';
import { EventLocationsService } from 'projects/evaluation/src/app/utils/services/event-locations/event-locations.service';
import { EventEvaluatorsService } from 'projects/evaluation/src/app/utils/services/event-evaluators/event-evaluators.service';
import { EventResponsiblePersonsService } from 'projects/evaluation/src/app/utils/services/event-responsible-persons/event-responsible-persons.service';
import { EventTestTemplateService } from 'projects/evaluation/src/app/utils/services/event-test-template/event-test-template.service';

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
  testTemplates: boolean = false;
  locations: boolean = false;
  evaluators: boolean = false;
  persons: boolean = false;
  users: boolean = false;

  currentUrl;
  urlApi: string;
  enum = TestingLocationTypeEnum;
  fields = [];
  attachedUsers = [];

  title: string;
  description: string;
  no: string;
  yes: string;
  currentDeleteName;

  constructor(private eventService: EventService,
    private route: ActivatedRoute,
    public translate: I18nService,
    private router: Router,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    private printTableService: PrintTableService,
    private eventUserService: EventUsersService,
    private eventLocationService: EventLocationsService,
    private eventEvaluatorService: EventEvaluatorsService,
    private eventResponsiblePersonsService: EventResponsiblePersonsService,
    private eventTestTemplateService: EventTestTemplateService,

  ) { }

  ngOnInit(): void {
    this.list();
    this.currentUrl = this.router.url.split("/").pop();
  }

  list(data: any = {}) {
    this.isLoading = true;
    let params = {
      eventId: this.importedId,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
    }

    if (this.category == "users") {
      this.urlApi = 'EventUser';
      this.eventService.getUsers(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.users = true;
        }
      });
      this.eventService.getListOfEventUsers({ eventId: this.importedId }).subscribe(res => {
        if (res && res.data) {
          this.attachedUsers = res.data;
        }
      })
    }

    if (this.category == "test-types") {
      this.eventTestTemplateService.getTestTemplates(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.testTemplates = true;
          this.getNameForDeleteModal(this.category);
        }
      });
    }

    if (this.category == "locations") {
      this.eventService.getLocations(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.locations = true;
          this.getNameForDeleteModal(this.category);
        }
      })
    }

    if (this.category == "evaluators") {
      this.urlApi = 'EventEvaluator';
      this.eventService.getEvaluators(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.evaluators = true;
        }
      });
      this.eventService.getListOfEventEvaluators({ eventId: this.importedId }).subscribe(res => {
        if (res && res.data) {
          this.attachedUsers = res.data;
        }
      })
    }

    if (this.category == "persons") {
      this.urlApi = 'EventResponsiblePerson';
      this.eventService.getResponsiblePersons(params).subscribe(res => {
        if (res && res.data) {
          this.fields = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
          this.persons = true;
        }
      });
      this.eventService.getListOfEventPersons({ eventId: this.importedId }).subscribe(res => {
        if (res && res.data) {
          this.attachedUsers = res.data;
        }
      })
    }
  }

  getNameForDeleteModal(category: string) {
    if (category == "test-types") {
      forkJoin([
        this.translate.get('tests.test-template-delete-modal')
      ]).subscribe(([currentDeleteName]) => {
        this.currentDeleteName = currentDeleteName;
      });
    } else if (category == "locations") {
      forkJoin([
        this.translate.get('locations.location')
      ]).subscribe(([currentDeleteName]) => {
        this.currentDeleteName = currentDeleteName;
      });
    } else {
      this.currentDeleteName = null;
    }
  }

  openConfirmationDeleteModal(eventId: number, itemId): void {
    this.getNameForDeleteModal(this.category);

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
    modalRef.componentInstance.description = `${this.description} ${this.currentDeleteName} ?`;
    modalRef.componentInstance.buttonNo = this.no;
    modalRef.componentInstance.buttonYes = this.yes;
    if (this.locations) {
      modalRef.result.then(() => this.detachLocation(eventId, itemId), () => { });
    }

    if (this.testTemplates) {
      modalRef.result.then(() => this.detachTestTemplate(eventId, itemId), () => { });
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

  openUsersModal(): void {
    const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
    modalRef.componentInstance.exceptUserIds = [];
    modalRef.componentInstance.page = this.category;
    modalRef.componentInstance.eventId = this.importedId;
    modalRef.componentInstance.attachedItems = [...this.attachedUsers];
    modalRef.componentInstance.inputType = 'checkbox';
    modalRef.result.then(() => {
      if (this.persons) {
        this.attachPersons(modalRef.result.__zone_symbol__value);
      } else if (this.users) {
        this.attachUser(modalRef.result.__zone_symbol__value);
      } else if (this.evaluators) {
        this.attachEvaluators(modalRef.result.__zone_symbol__value);
      }
    }, () => { });
  }

  parse(data) {
    return {
      eventId: +this.importedId,
      userProfileId: data.attachedItems || this.attachedUsers
    };
  }

  attachPersons(data): void {
    this.isLoading = true;
    this.eventService.attachPerson(this.parse(data)).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-add-delete-person-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.isLoading = false;
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => { }, () => this.list());
  }

  attachUser(data): void {
    this.isLoading = true;
    this.eventService.attachUser(this.parse(data)).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-add-delte-user-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.isLoading = false;
      this.list();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, (err) => { this.list(); this.isLoading = false; });
  }

  sendEmail(): void {
    for (let i = 0; i < this.attachedUsers.length; i++) {
      this.eventService.sendEmail(this.urlApi, this.attachedUsers[i], this.importedId).subscribe();
    }
  }

  parseCandidatePositions(candidatePositionsNames: string[]) {
    let string = candidatePositionsNames.join();
    return string.split(',').join(', ');
  }

  attachEvaluators(data): void {
    let params = {
      eventId: +this.importedId,
      evaluatorId: data.attachedItems || this.fields
    }
    this.isLoading = true;
    this.eventService.attachEvaluator(params).subscribe((res) => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('events.succes-attach-detach-evaluator-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.isLoading = false;
      this.list();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, (err) => { this.list(); this.isLoading = false; });
  }

  printTable(title: string) {
    let filters = {eventId:  this.importedId};
    switch (this.category) {
      case "users": {
        let headersDto = [
          'fullName',
			    'idnp',
          'candidatePositionNames'
        ];

        this.printTableService.getHeaders(this.eventUserService, title, headersDto, filters, document, "users");
        break;
      }
      case "test-types": {
        let headersDto = [
          'name',
          'questionCount',
          'duration'
        ];

        this.printTableService.getHeaders(this.eventTestTemplateService, title, headersDto, filters, document, "testTemplates");
        break;
      }
      case "locations": {
        let headersDto = [
          'name',
          'address',
          'type',
          'places'
        ];

        this.printTableService.getHeaders(this.eventLocationService, title, headersDto, filters, document, "locations");
        break;
      }
      case "evaluators": {
        let headersDto = [
          'fullName',
			    'idnp'
        ];

        this.printTableService.getHeaders(this.eventEvaluatorService, title, headersDto, filters, document, "locations");
        break;
      }
      case "persons": {
        let headersDto = [
          'fullName',
			    'idnp'
        ];

        this.printTableService.getHeaders(this.eventResponsiblePersonsService, title, headersDto, filters, document, "evaluators");
        break;
      }
      default:
        break;
    }
  }

}
