import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

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

	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(private service: PlanService,
	    private router: Router, 
		private route: ActivatedRoute,
		public translate: I18nService,
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
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('plans.remove-event-msg'),
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
		modalRef.result.then(() => this.detachLocation(params), () => { });
	
	  }
	
	  detachLocation(params) {
		forkJoin([
			this.translate.get('modal.success'),
			this.translate.get('plans.succes-remove-event-msg'),
		  ]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
			});
		this.service.detachEvent(params).subscribe(() => {
		  this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		  this.list();
		});
	  }

}
