import { Injectable } from '@angular/core';
import { ApplicationUserService } from './application-user.service';

@Injectable({
	providedIn: 'root',
})
export class PermissionCheckerService {
	constructor(private applicationUserService: ApplicationUserService) {}

	public isGranted(permission: string): boolean {
		var currentUser = this.applicationUserService.getCurrentUser();
		return currentUser.user && currentUser.user.permissions.indexOf(permission) != -1;
	}

	public areGranted(permissions: Array<string>): boolean {
		var currentUser = this.applicationUserService.getCurrentUser();
		return currentUser.user && currentUser.user.permissions.some(r=> permissions.includes(r));
	}
}
