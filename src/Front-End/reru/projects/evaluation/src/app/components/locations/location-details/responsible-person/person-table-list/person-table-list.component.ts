import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { AttachToLocationService } from 'projects/evaluation/src/app/utils/services/attach-to-location/attach-to-location.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

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

	constructor(
		private locationService: LocationService,
		private attachToLocationService: AttachToLocationService,
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
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = `Are you sure you want to delete this person?`;
		modalRef.result.then(() => this.detachPerson(locationId, personId), () => { });
	}

	detachPerson(locationId, personId) {
		this.locationService.detachPerson(locationId, personId).subscribe(() => {
			this.notificationService.success('Success', 'Responsible person was successfully detached', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}
}
