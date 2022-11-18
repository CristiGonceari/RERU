import { Component } from '@angular/core';
import { NavigationService } from '@erp/shared'

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
