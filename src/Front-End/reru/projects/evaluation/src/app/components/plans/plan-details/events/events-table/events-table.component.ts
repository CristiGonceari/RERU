import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

@Component({
  selector: 'app-events-table',
  templateUrl: './events-table.component.html',
  styleUrls: ['./events-table.component.scss']
})
export class EventsTableComponent implements OnInit {
  events: [] = [];
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	planId: number;
	isLoading: boolean = true;

	constructor(private service: PlanService,
	    private router: Router, 
		private route: ActivatedRoute,
		private modalService: NgbModal,
    	private notificationService: NotificationsService,
		) { }

	ngOnInit(): void {
		this.route.parent.params.subscribe(params => {
			this.planId = params.id;
		});
		this.list();
	}

	list(data: any = {}) {
		let params = {
		  	planId: this.planId,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		}

		this.service.events(params).subscribe(
			res => {
				if (res && res.data) {
					this.events = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

	openConfirmationDeleteModal(planId: number, itemId): void {
		const params = {
		   planId: +planId,
		   eventId: itemId
		}
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = `Are you sure you want to delete this event ?`;
		modalRef.result.then(() => this.detachLocation(params), () => { });
	
	  }
	
	  detachLocation(params) {
		this.service.detachEvent(params).subscribe(() => {
		  this.notificationService.success('Success', 'Event was successfully detached', NotificationUtil.getDefaultMidConfig());
		  this.list();
		});
	  }

}
