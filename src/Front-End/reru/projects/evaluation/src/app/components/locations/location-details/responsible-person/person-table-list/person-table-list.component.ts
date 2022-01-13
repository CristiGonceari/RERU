import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { AttachToLocationService } from 'projects/evaluation/src/app/utils/services/attach-to-location/attach-to-location.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';

@Component({
	selector: 'app-person-table-list',
	templateUrl: './person-table-list.component.html',
	styleUrls: ['./person-table-list.component.scss']
})
export class PersonTableListComponent implements OnInit {
	persons;
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
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	list(data: any = {}) {
		let params = {
			locationId: this.locationId,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}

		this.attachToLocationService.getPersons(params).subscribe(
			res => {
				if (res && res.data) {
					this.persons = res.data.items;
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

	openConfirmationDeleteModal(locationId: number, personId): void {
		forkJoin([
			this.translate.get('locations.remove'),
			this.translate.get('locations.remove-person-msg'),
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
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.detachPerson(locationId, personId), () => { });
	}

	detachPerson(locationId, personId) {
		this.locationService.detachPerson(locationId, personId).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('locations.succes-remove-person-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}
}
