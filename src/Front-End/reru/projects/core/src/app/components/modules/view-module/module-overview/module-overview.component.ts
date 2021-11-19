import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModulesService } from '../../../../utils/services/modules.service';
import { AdminModuleModel } from '../../../../utils/models/admin-module.model';

@Component({
	selector: 'app-module-overview',
	templateUrl: './module-overview.component.html',
	styleUrls: ['./module-overview.component.scss'],
})
export class ModuleOverviewComponent implements OnInit {
	isLoading = true;
	moduleData: AdminModuleModel[] = [];

	constructor(private moduleService: ModulesService, private activatedRoute: ActivatedRoute) {}

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.getModuleInfo(params.id);
			}
		});
	}

	getModuleInfo(id): void {
		this.moduleService.get(id).subscribe(res => {
			if (res && res.data) {
				this.moduleData = res.data;
				this.isLoading = false;
			}
		});
	}
}
