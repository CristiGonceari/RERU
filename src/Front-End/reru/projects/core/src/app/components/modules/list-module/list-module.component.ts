import { Component, OnInit } from '@angular/core';
import { SearchPipe } from '../../../utils/pipes/search.pipe';
import { SafeHtmlPipe } from '../../../utils/pipes/safe-html.pipe';
import { AdminModuleModel } from '../../../utils/models/admin-module.model';
import { ModulesService } from '../../../utils/services/modules.service';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PermissionCheckerService } from '@erp/shared';

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
		public permissionService: PermissionCheckerService
	) {}

	ngOnInit(): void {
		this.getModules();
		this.checkPermission();
	}

	getModules(page?): void {
		let params: any = {
			page,
			itemsPerPage: this.pagination.pageSize,
		};
		this.isLoading = true;
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

}
