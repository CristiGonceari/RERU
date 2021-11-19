import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/core/src/app/utils/modals/confirm-modal/confirm-modal.component';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { RoleModel } from 'projects/core/src/app/utils/models/role.model';
import { ModuleRolesService } from 'projects/core/src/app/utils/services/module-roles.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { PermissionCheckerService } from '@erp/shared';

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

	constructor(
		private roleService: ModuleRolesService,
		private activatedRoute: ActivatedRoute,
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

	getRoles(page?): void {
		let params: any = {
			page,
			itemsPerPage: this.pagination.pageSize,
		};
		this.roleService.get(this.moduleId, params).subscribe(res => {
			this.isLoading = false;
			if (res && res.data.items.length) {
				this.roles = res.data.items;
				this.pagination = res.data.pagedSummary;
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
				}
			}
		});
		this.isLoading = true;
	}

	openRemoveRoleModal(id: string): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Remove';
		modalRef.componentInstance.description = 'Are you sure you want to delete this role?';
		modalRef.result.then(() => this.removeRole(id), () => { });
	}

	removeRole(id: string): void {
		this.isLoading = true;
		this.roleService.removeRole(id).subscribe(response => {
			this.notificationService.success('Success', 'Role has been removed',
				NotificationUtil.getDefaultMidConfig());
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
