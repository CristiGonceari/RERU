import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationUserService, NavigationService } from '@erp/shared'
import { ProfileService } from './utils/services/profile.service';

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
	
	constructor(public navigation: NavigationService) { this.navigation.startSaveHistory()}

}
