import { Component, OnInit } from '@angular/core';
import { SidebarView } from '../../utils/models/sidebar.model';
import { ApplicationUserService, AvailableModulesService, ApplicationUserModuleModel } from '@erp/shared';

@Component({
	selector: 'app-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
	sidebarView = SidebarView;
	modules: ApplicationUserModuleModel[];
	constructor(private moduleService: AvailableModulesService, private userSubject: ApplicationUserService) {}

	ngOnInit(): void {
		this.list();
		this.subscribeForAuthChange();
	}

	subscribeForAuthChange(): void {
		this.userSubject.userChange.subscribe(() => (this.modules = this.moduleService.get()));
	}

	list(): void {
		this.modules = this.moduleService.get();
		console.log("this.module", this.modules);
		
	}
}
