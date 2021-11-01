import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SidebarItemType } from './utils/models/sidebar.model';
import { I18nService } from './utils/services/i18n.service';
import { Router } from '@angular/router';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { forkJoin } from 'rxjs';
import { AppSettingsService, IAppSettings, AuthenticationService } from '@erp/shared';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {
	options: {
		animate: 'fromTop';
		position: ['top', 'right'];
		timeOut: 2000;
		lastOnBottom: true;
		showProgressBar: true;
	};
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
</svg>`,
		}, {
			type: SidebarItemType.ITEM,
			url: '/#',
			name: '#',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
        <rect x="0" y="0" width="24" height="24"/>
        <path d="M3.95709826,8.41510662 L11.47855,3.81866389 C11.7986624,3.62303967 12.2013376,3.62303967 12.52145,3.81866389 L20.0429,8.41510557 C20.6374094,8.77841684 21,9.42493654 21,10.1216692 L21,19.0000642 C21,20.1046337 20.1045695,21.0000642 19,21.0000642 L4.99998155,21.0000673 C3.89541205,21.0000673 2.99998155,20.1046368 2.99998155,19.0000673 L2.99999828,10.1216672 C2.99999935,9.42493561 3.36258984,8.77841732 3.95709826,8.41510662 Z M10,13 C9.44771525,13 9,13.4477153 9,14 L9,17 C9,17.5522847 9.44771525,18 10,18 L14,18 C14.5522847,18 15,17.5522847 15,17 L15,14 C15,13.4477153 14.5522847,13 14,13 L10,13 Z" fill="#000000"/>
    </g>
</svg>`,
		},
	];
	appSettings: IAppSettings;
	isCollapsed: boolean;
	constructor(
		public translate: I18nService,
		private router: Router,
		private localize: LocalizeRouterService,
		private appConfigService: AppSettingsService,
		private cd: ChangeDetectorRef,
		private authenticationService: AuthenticationService
	) {
		this.appSettings = this.appConfigService.settings;
	}

	ngOnInit(): void {
		this.translateData();
		this.subscribeForLanguageChange();
	}

	ngAfterViewInit(): void {
		this.cd.detectChanges();
	}

	subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

	navigate(index: number): void {
		this.sidebarItems[index] && this.router.navigate([this.localize.translateRoute(this.sidebarItems[index].url)]);
	}

	translateData(): void {
		forkJoin([
			this.translate.get('sidebar.home'),
		]).subscribe(
			([
				home,
			]) => {
				this.sidebarItems[0].name = home;
			}
		);
	}

	logout(): void {
		this.authenticationService.signout();
	}
}
