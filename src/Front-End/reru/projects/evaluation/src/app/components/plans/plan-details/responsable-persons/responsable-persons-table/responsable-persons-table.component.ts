import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';

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

	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private planService: PlanService, 
		private router: Router, 
		public translate: I18nService,
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
		forkJoin([
			this.translate.get('plans.delete'),
			this.translate.get('plans.remove-person-msg'),
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
		modalRef.result.then(() => this.detachPerson(id, itemId), () => { });
	
	  }
	
	  detachPerson(id, itemId) {
		forkJoin([
			this.translate.get('modal.success'),
			this.translate.get('plans.succes-remove-person-msg'),
		  ]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
			});
		this.planService.detachPerson(id, itemId).subscribe(() => {
		  this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		  this.list();
		});
	  }
}
