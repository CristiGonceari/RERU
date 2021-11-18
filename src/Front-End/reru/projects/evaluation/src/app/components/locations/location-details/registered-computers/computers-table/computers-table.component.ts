import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { AttachToLocationService } from '../../../../../utils/services/attach-to-location/attach-to-location.service';

@Component({
	selector: 'app-computers-table',
	templateUrl: './computers-table.component.html',
	styleUrls: ['./computers-table.component.scss']
})
export class ComputersTableComponent implements OnInit {

	clients = [];
	pagination: PaginationModel = new PaginationModel();
	locationId: number;
	isLoading: boolean = true;

	constructor(
		private locationService: LocationService,
		private route: ActivatedRoute,
		private modalService: NgbModal,
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

		this.locationService.getComputers(params).subscribe(
			res => {
				if (res && res.data) {
					this.clients = res.data.items;
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

	openConfirmationDeleteModal(id: number): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = `Are you sure you want to unassign this computer?`;
		modalRef.result.then(() => this.unassignedComputer(id), () => { });
	}

	unassignedComputer(id) {
		this.locationService.unassignedComputer(id).subscribe(() => {
			this.notificationService.success('Success', 'Computer was successfully unassigned', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}
}
