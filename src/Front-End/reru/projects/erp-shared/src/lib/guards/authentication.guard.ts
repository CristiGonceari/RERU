import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
	providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
	constructor(private authenticationService: AuthenticationService) // private router: Router,
	// private localize: LocalizeRouterService
	{}
	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		console.log('Authentication guard ', next.url);
		if (this.authenticationService.isAuthenticated()) {
			return true;
		}
		this.authenticationService.login();
		return false;
	}
}
