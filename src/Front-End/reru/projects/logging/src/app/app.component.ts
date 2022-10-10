import { Component } from '@angular/core';
import { I18nService } from './utils/services/i18n/i18n.service';
import { Router } from '@angular/router';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { forkJoin } from 'rxjs';
import { SidebarItemType } from './utils/models/sidebar.model';
import { AppSettingsService, IAppSettings, AuthenticationService, NavigationService } from '@erp/shared';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  options: {
    animate: 'fromTop';
    position: ['top', 'right'];
    timeOut: 2000;
    lastOnBottom: true;
    showProgressBar: true;
  };

  title = 'logging';

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
    },
    {
			type: SidebarItemType.SECTION,
			url: '',
			name: '',
		},
		{
			permission: 'P04000002',
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
		}
  ];
  appSettings: IAppSettings;
  constructor(
    private router: Router,
    public translate: I18nService,
    private localize: LocalizeRouterService,
    private appSettingsService: AppSettingsService,
    private authenticationService: AuthenticationService,
    public navigation: NavigationService
  ) {
    this.appSettings = this.appSettingsService.settings;
    this.navigation.startSaveHistory();
  }

  ngOnInit(): void {
    this.translateData();
    this.translate.change.subscribe(() => this.translateData());
  }
  
  getCurrentLocation(){
		if(this.router.url === '/'){
			return true;
		} else {
			return false;
		}
	}

  navigate(index: number): void {
    this.sidebarItems[index] && this.router.navigate([this.localize.translateRoute(this.sidebarItems[index].url)]);
  }

  translateData(): void {
    forkJoin([
      this.translate.get('sidebar.home'),
      this.translate.get('faq.help'),
			this.translate.get('faq.faq')

    ]).subscribe(([home, help, faq]) => {
      this.sidebarItems[0].name = home;
      this.sidebarItems[1].name = help;
			this.sidebarItems[2].name = faq;
    });
  }

  logout() {
    this.authenticationService.signout();
  }
}
