import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { PermissionModel } from 'projects/core/src/app/utils/models/permission.model';
import { ModuleRolePermissionsService } from 'projects/core/src/app/utils/services/module-role-permissions.service';


@Component({
	selector: 'app-role-permissions-list',
	templateUrl: './role-permissions-list.component.html',
	styleUrls: ['./role-permissions-list.component.scss']
})
export class RolePermissionsListComponent implements OnInit {
	isLoading = true;
	roleId: number;
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	permissions: PermissionModel[];

	constructor(private permissionServise: ModuleRolePermissionsService, private activatedRoute: ActivatedRoute, private router: Router,
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.roleId = params.id;
				this.getPermissions();
			}
		});
	}

	getPermissions(): void {
		let params: any = {
			page: this.pagination.currentPage,
			itemsPerPage: this.pagination.pageSize || 10
		};
		this.list(params);
	}

	list(params){
		this.permissionServise.get(this.roleId, params).subscribe(res => {
			if (res) {
				this.isLoading = false;
				this.permissions = res.data.items;
				this.pagination = res.data.pagedSummary;
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
				}
			}
		});
		this.isLoading = true;
	}

	navigateToUpdatePermissions(): void {
		this.router.navigate([`../../../roles/${this.roleId}/update-permissions`]);
	}
}
