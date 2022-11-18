import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApplicationUserService } from '../../../services/application-user.service';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
	selector: 'app-authentication-callback',
	templateUrl: './authentication-callback.component.html',
	styleUrls: ['./authentication-callback.component.scss'],
})
export class AuthenticationCallbackComponent implements OnInit {
	error: boolean;

	constructor(
		private authService: AuthenticationService,
		private applicationUserService: ApplicationUserService,
		private router: Router
	) {}

	async ngOnInit() {
		// if (this.route.params.subscribe(p => p).indexOf('error') >= 0) {
		// 	this.error = true;
		// 	return;
		// }

		// if(this.route.params.subscribe(response=> {
		//   if (response.errpr && response.id !== 'undefined') {
		// 		this.error = true;

		// 	}

		// }))

		await this.authService.completeAuthentication();
		this.applicationUserService.loadCurrentUser();
		this.router.navigate(['/']);
	}
}
