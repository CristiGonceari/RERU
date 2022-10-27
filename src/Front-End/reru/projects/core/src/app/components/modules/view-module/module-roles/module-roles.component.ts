import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { RoleModel } from 'projects/core/src/app/utils/models/role.model';
import { ModuleRolesService } from 'projects/core/src/app/utils/services/module-roles.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ConfirmModalComponent, PermissionCheckerService } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';

@Component({
	selector: 'app-module-roles',
	templateUrl: './module-roles.component.html',
	styleUrls: ['./module-roles.component.scss'],
})
export class ModuleRolesComponent implements OnInit {
	isLoading = true;
	roles: RoleModel[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	moduleId: number;
	viewPermissions: boolean = false;
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private roleService: ModuleRolesService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private router: Router,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		public permissionService: PermissionCheckerService
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
		this.checkPermission();
	}

	subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
				this.getRoles();
			}
		});
	}

	getRoles(data: any = {}): void {
		let params: any = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(params);
	}

	list(params) {
		this.isLoading = true;
		this.roleService.get(this.moduleId, params).subscribe(res => {
			if (res && res.data.items.length) {
				this.roles = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	openRemoveRoleModal(id: string, name): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('pages.roles.delete-msg'),
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
		modalRef.result.then(() => this.removeRole(id), () => { });
	}

	removeRole(id: string): void {
		this.isLoading = true;
		this.roleService.removeRole(id).subscribe(response => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('pages.modules.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}

	navigateToPermissions(id): void {
		this.router.navigate([`../../../roles/${id}/permissions`]);
	}

	navigateToUpdatePermissions(id): void {
		this.router.navigate([`../../../roles/${id}/update-permissions`]);
	}

	checkPermission(): void {
		if (this.permissionService.isGranted('P00000010')) 
      		this.viewPermissions = true;
	}

}
