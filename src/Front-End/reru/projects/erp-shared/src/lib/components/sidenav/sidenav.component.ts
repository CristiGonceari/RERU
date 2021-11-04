import { Component, Input, OnInit } from '@angular/core';
import { IAppSettings } from '../../models/app-settings.model';
import { ApplicationUserModuleModel } from '../../models/application-user-module.model';
import { AvailableModulesService } from '../../services/available-modules.service';
import { ApplicationUserService } from '../../services/application-user.service';

@Component({
	selector: 'app-sidenav',
	templateUrl: './sidenav.component.html',
	styleUrls: ['./sidenav.component.scss'],
})
export class SidenavComponent implements OnInit {
	modules: ApplicationUserModuleModel[] = [];
	@Input() config: IAppSettings;
	@Input() logo: string;
	@Input() moduleId: number;
	isLoading = true;
	constructor(private moduleService: AvailableModulesService, private applicationUserService: ApplicationUserService) {}

	ngOnInit(): void {
		this.listModules();
		this.subscribeForAuthChanges();
	}

	subscribeForAuthChanges(): void {
		this.applicationUserService.userChange.subscribe(() => {
			this.isLoading = true;
			this.listModules();
		});
	}

	listModules(): void {
		this.modules = this.moduleService.get();
		this.isLoading = false;
	}
}
