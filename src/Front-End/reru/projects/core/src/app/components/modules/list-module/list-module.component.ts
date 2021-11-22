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

@Component({
	selector: 'app-list-module',
	templateUrl: './list-module.component.html',
	styleUrls: ['./list-module.component.scss'],
	providers: [SearchPipe, SafeHtmlPipe],
})
export class ListModuleComponent implements OnInit {
	isLoading = false;
	modules: AdminModuleModel[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	viewDetails: boolean = false;

	constructor(
		private moduleService: ModulesService,
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

	getModules(): void {
		let params: any = {
			page:  this.pagination.currentPage,
			itemsPerPage: this.pagination.pageSize || 10
		};
		this.isLoading = true;
		this.list(params);
	}
	
	list(params){
		this.moduleService.moduleList(params).subscribe(res => {
			if(res && res.data) {
				this.modules = res.data.items;
				this.pagination = res.data.pagedSummary;
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
				}
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
		// if (this.permissionService.isGranted('P00000004')) 
      	// 	this.viewDetails = true;
	}

	openRemoveModal(id: number, name): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Remove';
		modalRef.componentInstance.description = `Are you sure you want to delete this ${name}?`;
		modalRef.result.then(() => this.removeModule(id, name), () => {});
	}

	removeModule(id: number, name): void {
		this.moduleService.delete(id).subscribe(
			res => {
				this.notificationService.success(
					'Success',
					`Module ${name} has been removed successfully!`,
					NotificationUtil.getDefaultMidConfig(),
		 			this.getModules()
				);
			},
			err => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

}
