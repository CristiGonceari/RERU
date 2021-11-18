import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

@Component({
  selector: 'app-responsable-persons-table',
  templateUrl: './responsable-persons-table.component.html',
  styleUrls: ['./responsable-persons-table.component.scss']
})
export class ResponsablePersonsTableComponent implements OnInit {
    persons;
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	id: number;
	isLoading: boolean = true;

	constructor(private planService: PlanService, 
		private router: Router, 
		private route: ActivatedRoute,
		private modalService: NgbModal,
    	private notificationService: NotificationsService,) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams(): void {
		this.route.parent.params.subscribe(params => {
			this.id = params.id;
			if (this.id) {
				this.list();
			}
		});
	}

	list(data: any = {}) {
		let params = {
			planId: this.id,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		}

		this.planService.persons(params).subscribe( res => {
			if (res && res.data) {
				this.persons = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
				
		});
	}

	openConfirmationDeleteModal(id: number, itemId): void {
		const params = {
		   planId: +id,
		   personId: itemId
		}
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = `Are you sure you want to delete this person ?`;
		modalRef.result.then(() => this.detachPerson(id, itemId), () => { });
	
	  }
	
	  detachPerson(id, itemId) {
		this.planService.detachPerson(id, itemId).subscribe(() => {
		  this.notificationService.success('Success', 'Event was successfully detached', NotificationUtil.getDefaultMidConfig());
		  this.list();
		});
	  }
}
