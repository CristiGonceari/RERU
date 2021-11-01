import { Injectable } from '@angular/core';
import {
	ActivatedRouteSnapshot,
	CanActivate,
	CanActivateChild,
	Router,
	RouterStateSnapshot,
	UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { PermissionCheckerService } from '../services/permission-checker.service';

@Injectable()
export class PermissionRouteGuard implements CanActivate, CanActivateChild {
	constructor(private _permissionChecker: PermissionCheckerService, private _router: Router) {}
	canActivateChild(
		childRoute: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
		return this.canActivate(childRoute, state);
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
		if (!route.data || !route.data['permission']) {
			return true;
		}
		if (this._permissionChecker.isGranted(route.data['permission'])) {
			return true;
		}
		this._router.navigate([this.selectBestRoute()]);
		return false;
	}

	selectBestRoute(): string {
		return '/';
	}
}
