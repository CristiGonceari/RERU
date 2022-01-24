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
export class AppComponent {
	options: {
		animate: 'fromTop';
		position: ['top', 'right'];
		timeOut: 2000;
		lastOnBottom: true;
		showProgressBar: true;
	};
	constructor() {}
}
