import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AttachUserModalComponent } from 'projects/evaluation/src/app/utils/components/attach-user-modal/attach-user-modal.component';
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
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	id: number;
	isLoading: boolean = true;
	attachedPersons = [];

	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private planService: PlanService, 
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
				this.attachedPersons = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	openUsersModal(): void {
		const modalRef: any = this.modalService.open(AttachUserModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.exceptUserIds = [];
		modalRef.componentInstance.attachedItems = this.attachedPersons.map(el => el.id);
		modalRef.componentInstance.inputType = 'checkbox';
		modalRef.result.then(() => {
			this.attachPersons(modalRef.result.__zone_symbol__value.attachedItems);
		}, () => { });
	}

	parse(users) {
		return {
			planId: +this.id,
			userProfileId: users || this.attachedPersons
		};
	}

	attachPersons(users) {
		this.planService.attachPerson(this.parse(users)).subscribe(() => {
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
