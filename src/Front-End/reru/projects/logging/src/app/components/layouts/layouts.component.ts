import { Component, OnInit } from '@angular/core';
import { I18nService } from '../../utils/services/i18n/i18n.service';
import { Router } from '@angular/router';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { forkJoin } from 'rxjs';
import { SidebarItemType } from '../../utils/models/sidebar.model';
import { AppSettingsService, IAppSettings } from '@erp/shared';
// import { IAppConfig } from '../../utils/models/app-config.model';

@Component({
	selector: 'app-layouts',
	templateUrl: './layouts.component.html',
	styleUrls: ['./layouts.component.scss']
})
export class LayoutsComponent implements OnInit {

	sidebarItems: any[] = [
		{
			type: SidebarItemType.ITEM,
			url: '/',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
			<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
				<rect x="0" y="0" width="24" height="24"/>
				<path d="M3.95709826,8.41510662 L11.47855,3.81866389 C11.7986624,3.62303967 12.2013376,3.62303967 12.52145,3.81866389 L20.0429,8.41510557 C20.6374094,8.77841684 21,9.42493654 21,10.1216692 L21,19.0000642 C21,20.1046337 20.1045695,21.0000642 19,21.0000642 L4.99998155,21.0000673 C3.89541205,21.0000673 2.99998155,20.1046368 2.99998155,19.0000673 L2.99999828,10.1216672 C2.99999935,9.42493561 3.36258984,8.77841732 3.95709826,8.41510662 Z M10,13 C9.44771525,13 9,13.4477153 9,14 L9,17 C9,17.5522847 9.44771525,18 10,18 L14,18 C14.5522847,18 15,17.5522847 15,17 L15,14 C15,13.4477153 14.5522847,13 14,13 L10,13 Z" fill="#000000"/>
			</g>
		</svg>`
		}
	];
	appSettings: IAppSettings;
	constructor(
		private router: Router,
		public translate: I18nService,
		private localize: LocalizeRouterService,
		private appSettingsService: AppSettingsService
	) {
		this.appSettings = this.appSettingsService.settings;
	}

	ngOnInit(): void {
		this.translateData();
		this.translate.change.subscribe(() => this.translateData());
	}

	navigate(index: number): void {
		this.sidebarItems[index] && this.router.navigate([this.localize.translateRoute(this.sidebarItems[index].url)]);
	}

	translateData(): void {
		forkJoin([
			this.translate.get('sidebar.home'),
			this.translate.get('dashboard.my-activities'),
			this.translate.get('sidebar.settings'),
			this.translate.get('sidebar.manage-categories'),
			this.translate.get('sidebar.manage-questions'),
			this.translate.get('sidebar.manage-tests'),
			this.translate.get('sidebar.test'),
			this.translate.get('verify-test.title'),
			this.translate.get('statistics.statistics'),
			this.translate.get('sidebar.events'),
			this.translate.get('locations.locations'),
			this.translate.get('events.events'),
			this.translate.get('plans.plans'),
			this.translate.get('faq.help'),
			this.translate.get('faq.faq'),
		]).subscribe(([home, activities, settings, categories, questions, tests, test, verifyTest,statistic, event, location, events, plan, help, faq]) => {
			this.sidebarItems[0].name = home;
			this.sidebarItems[1].name = activities;
			this.sidebarItems[2].name = settings;
			this.sidebarItems[3].name = categories;
			this.sidebarItems[4].name = questions;
			this.sidebarItems[5].name = tests;
			this.sidebarItems[6].name = test;
			this.sidebarItems[7].name = verifyTest;
			this.sidebarItems[8].name = statistic;
			this.sidebarItems[9].name = event;
			this.sidebarItems[10].name = location;
			this.sidebarItems[11].name = events;
			this.sidebarItems[12].name = plan;
			this.sidebarItems[13].name = help;
			this.sidebarItems[14].name = faq;
		});
	}

	logout() {
		localStorage.clear();
		window.location.assign('/');
	}
}
