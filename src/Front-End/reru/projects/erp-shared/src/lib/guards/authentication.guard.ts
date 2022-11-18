import { Injectable } from '@angular/core';
import { CanActivate, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
	providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
	constructor(private authenticationService: AuthenticationService) // private router: Router,
	// private localize: LocalizeRouterService
	{}
	canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		if (this.authenticationService.isAuthenticated()) {
			return true;
		}
		this.authenticationService.login();
		return false;
	}
}
