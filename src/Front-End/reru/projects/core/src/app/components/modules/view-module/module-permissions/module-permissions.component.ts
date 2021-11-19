import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { PermissionModel } from 'projects/core/src/app/utils/models/permission.model';
import { ModulePermissionsService } from 'projects/core/src/app/utils/services/module-permissions.service';
import { ObjectUtil } from 'projects/core/src/app/utils/util/object.util';

@Component({
	selector: 'app-module-permissions',
	templateUrl: './module-permissions.component.html',
	styleUrls: ['./module-permissions.component.scss'],
})
export class ModulePermissionsComponent implements OnInit {
	isLoading = true;
	moduleId: number;
	permissions: PermissionModel[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	result: boolean;
	keyword: string;

	constructor(
		private permissionServise: ModulePermissionsService,
		private activatedRoute: ActivatedRoute
		) {}

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
				this.getPermissions();
			}
		});
	}
	
	getPermissions(data: any = {}): void {
		this.keyword = data.keyword;
		data = {
			...data,
			keyword: this.keyword,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: this.pagination.pageSize
		};
		this.permissionServise.get(this.moduleId, ObjectUtil.preParseObject(data)).subscribe(res => {
			if (res && res.data.items.length) {
				this.result = true;
				this.isLoading = false;
				this.permissions = res.data.items;
				this.pagination = res.data.pagedSummary;
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
				}
			} else {
				this.isLoading = false;
				this.result = false;
			}
		});
		this.isLoading = true;
	}

}
