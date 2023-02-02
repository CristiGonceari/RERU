import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SidebarItemType } from '@erp/shared';
import { Router } from '@angular/router';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { forkJoin } from 'rxjs';
import { AppSettingsService, IAppSettings, AuthenticationService, NavigationService } from '@erp/shared';
import { I18nService } from './utils/services/i18n.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
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
		},
		{
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			type: SidebarItemType.ITEM,
			url: '/evaluation',
			name: '',
			icon: ` 
			<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"> <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"> <rect x="0" y="0" width="24" height="24"></rect> <rect fill="#000000" x="4" y="4" width="7" height="7" rx="1.5"></rect> <path d="M5.5,13 L9.5,13 C10.3284271,13 11,13.6715729 11,14.5 L11,18.5 C11,19.3284271 10.3284271,20 9.5,20 L5.5,20 C4.67157288,20 4,19.3284271 4,18.5 L4,14.5 C4,13.6715729 4.67157288,13 5.5,13 Z M14.5,4 L18.5,4 C19.3284271,4 20,4.67157288 20,5.5 L20,9.5 C20,10.3284271 19.3284271,11 18.5,11 L14.5,11 C13.6715729,11 13,10.3284271 13,9.5 L13,5.5 C13,4.67157288 13.6715729,4 14.5,4 Z M14.5,13 L18.5,13 C19.3284271,13 20,13.6715729 20,14.5 L20,18.5 C20,19.3284271 19.3284271,20 18.5,20 L14.5,20 C13.6715729,20 13,19.3284271 13,18.5 L13,14.5 C13,13.6715729 13.6715729,13 14.5,13 Z" fill="#000000" opacity="0.3"></path> </g></svg>
			`,
		},
		{
			permissions: ['P05000001'],
			type: SidebarItemType.SECTION,
			url: '/help',
			name: '',
		},
		{
			permission: 'P05000001',
			type: SidebarItemType.ITEM,
			url: '/faq',
			name: '',
			icon: `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px"
            viewBox="0 0 24 24" version="1.1">
            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
            <rect x="0" y="0" width="24" height="24"/>
            <circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10"/>
            <path d="M12,16 C12.5522847,16 13,16.4477153 13,17 C13,17.5522847 12.5522847,18 12,18 C11.4477153,18 11,17.5522847 11,17 C11,16.4477153
            11.4477153,16 12,16 Z M10.591,14.868 L10.591,13.209 L11.851,13.209 C13.447,13.209 14.602,11.991 14.602,10.395 C14.602,8.799 13.447,7.581
            11.851,7.581 C10.234,7.581 9.121,8.799 9.121,10.395 L7.336,10.395 C7.336,7.875 9.31,5.922 11.851,5.922 C14.392,5.922 16.387,7.875 16.387,10.395
            C16.387,12.915 14.392,14.868 11.851,14.868 L10.591,14.868 Z" fill="#000000"/>
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
		private authenticationService: AuthenticationService,
		public navigation: NavigationService
	) {
		this.appSettings = this.appConfigService.settings;
		this.navigation.startSaveHistory();
	}

	get isError(): boolean {
		return this.router.url.includes('404') ||
			   this.router.url.includes('500');
	}

	set isError(value: boolean) {}

	get isNotHomeRoute(): boolean {
		return this.router.url !== '/';
	}

	set isNotHomeRoute(value) {}

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
			this.translate.get('sidebar.administration'),
			this.translate.get('sidebar.evaluations'),
			this.translate.get('faq.help'),
			this.translate.get('faq.faq'),
		]).subscribe(
			([
				home,
				administration,
				evaluations,
				help,
				faq
			]) => {
				this.sidebarItems[0].name = home;
				this.sidebarItems[1].name = administration;
				this.sidebarItems[2].name = evaluations;
				this.sidebarItems[3].name = help;
				this.sidebarItems[4].name = faq;
			}
		);
	}

	logout(): void {
		this.authenticationService.signout();
	}
}
