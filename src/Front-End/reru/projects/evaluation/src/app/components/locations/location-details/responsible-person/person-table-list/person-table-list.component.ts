import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { AttachToLocationService } from 'projects/evaluation/src/app/utils/services/attach-to-location/attach-to-location.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { PrintTableService } from 'projects/evaluation/src/app/utils/services/print-table/print-table.service';
import { LocationResponsiblePersonService } from 'projects/evaluation/src/app/utils/services/location-responsible-persons/location-responsible-person.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';

@Component({
	selector: 'app-person-table-list',
	templateUrl: './person-table-list.component.html',
	styleUrls: ['./person-table-list.component.scss']
})
export class PersonTableListComponent implements OnInit {
	attachedPersons = [];
	pagination: PaginationModel = new PaginationModel();
	locationId: number;
	isLoading: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private locationService: LocationService,
		private attachToLocationService: AttachToLocationService,
		public translate: I18nService,
		private modalService: NgbModal,
		private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private printTableService: PrintTableService,
		private locationResponsiblePersonService: LocationResponsiblePersonService
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	getHeaders(title: string){
		let headersDto = [
			'fullName',
			'idnp'
		];

		let filters = {
			locationId: this.locationId
		}

		this.printTableService.getHeaders(this.locationResponsiblePersonService, title, headersDto, filters, document);
	}

	list(data: any = {}) {
		this.isLoading = true;
		let params = {
			locationId: this.locationId,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || 10
		}

		this.attachToLocationService.getPersons(params).subscribe(
			res => {
				if (res && res.data) {
					this.attachedPersons = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

	subsribeForParams(): void {
		this.route.parent.params.subscribe(params => {
			this.locationId = params.id;
			if (this.locationId) {
				this.list();
			}
		});
	}

	openUsersModal(): void {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.exceptUserIds = [];
		modalRef.componentInstance.attachedItems = [...this.attachedPersons.map(el => el.id)];
		modalRef.componentInstance.inputType = 'checkbox';
		modalRef.result.then(() => {
			this.attachPersons(modalRef.result.__zone_symbol__value.attachedItems);
		}, () => { });
	}

	parse(users) {
		return {
			locationId: +this.locationId,
			userProfileId: users || this.attachedPersons
		};
	}

	attachPersons(users) {
		this.locationService.assignPerson(this.parse(users)).subscribe(() => {
		  forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('events.succes-add-delete-person-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		}, () => {}, () => this.list());
	}
}
