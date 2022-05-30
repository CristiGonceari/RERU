import { Component, OnInit, ViewChild } from '@angular/core';
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
	code: string;
	description: string;

	@ViewChild('code') searchCode: any;
	@ViewChild('description') searchDescription: any;

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
		this.code = this.searchCode?.key;
		this.description = this.searchDescription?.key;
		
		data = {
			...data,
			code: this.code,
			description: this.description,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(data);
	}

	list(data): void {
		this.permissionServise.get(this.moduleId, ObjectUtil.preParseObject(data)).subscribe(res => {
			if (res && res.data.items.length) {
				this.isLoading = false;
				this.permissions = res.data.items;
				this.pagination = res.data.pagedSummary;
			} else {
				this.isLoading = false;
			}
		});
		this.isLoading = true;
	}

	clearFields() {
		this.searchCode.clear();
		this.searchDescription.clear();
	}
}
