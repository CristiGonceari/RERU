import { Component, OnInit } from '@angular/core';
import { SearchPipe } from '../../../utils/pipes/search.pipe';
import { SafeHtmlPipe } from '../../../utils/pipes/safe-html.pipe';
import { AdminModuleModel } from '../../../utils/models/admin-module.model';
import { ModulesService } from '../../../utils/services/modules.service';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ConfirmModalComponent, PermissionCheckerService } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
	selector: 'app-list-module',
	templateUrl: './list-module.component.html',
	styleUrls: ['./list-module.component.scss'],
	providers: [SearchPipe, SafeHtmlPipe],
})
export class ListModuleComponent implements OnInit {
	isLoading = true;
	modules: AdminModuleModel[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	viewDetails: boolean = false;
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private moduleService: ModulesService,
		public translate: I18nService,
		private router: Router, 
		private route: ActivatedRoute,
		public permissionService: PermissionCheckerService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
	) {}

	ngOnInit(): void {
		this.getModules();
		this.checkPermission();
	}

	getModules(data: any = {}): void {
		let params: any = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(params);
	}
	
	list(params){
		this.moduleService.moduleList(params).subscribe(res => {
			if(res && res.data) {
				this.modules = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	navigateToDetails(id): void {
		this.router.navigate(['../', id, 'overview'], {relativeTo: this.route});
	}

	navigate(url: string): void {
		window.open(url, '_blank');
	}

	checkPermission(): void {
		if (this.permissionService.isGranted('P00000004')) 
      		this.viewDetails = true;
	}

	openRemoveModal(id: number, name): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('pages.modules.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = `${this.description} (${name})?`;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.removeModule(id, name), () => {});
	}

	removeModule(id: number, name): void {
		this.moduleService.delete(id).subscribe(res => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('modules.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title,`${name} ${this.description}`, NotificationUtil.getDefaultMidConfig(),
		 			this.getModules()
				);
			},
			err => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

}
