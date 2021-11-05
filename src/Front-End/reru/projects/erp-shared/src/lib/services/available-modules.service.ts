import { Injectable } from '@angular/core';
import { ApplicationUserModel } from '../models/application-user.model';
import { IconModel } from '../models/icon.model';
import * as data from '../assets/icons.json';
import { ApplicationUserService } from './application-user.service';
import { ApplicationUserModuleModel } from '../models/application-user-module.model';

@Injectable({
	providedIn: 'root',
})
export class AvailableModulesService {
	userModules: ApplicationUserModuleModel[] = [];
	isSubscribed: boolean;
	authModel: ApplicationUserModel;
	icons: IconModel[] = (data as any).default;
	constructor(private applicationUserService: ApplicationUserService) {
		this.initializeModules();
		this.subscribeForAuthChanges();
	}

	get(): ApplicationUserModuleModel[] {
		return this.userModules;
	}

	private initializeModules(): void {
		if (!this.isSubscribed) {
			const currentUser = this.applicationUserService.getCurrentUser();
			this.assignModules(currentUser);
		}
	}

	private subscribeForAuthChanges(): void {
		if (!this.isSubscribed) {
			this.applicationUserService.userChange.subscribe((response: ApplicationUserModel) => {
				if (response) {
					this.assignModules(response);
				}
			});
			this.isSubscribed = true;
		}
	}

	private assignModules(user: ApplicationUserModel) {
		this.userModules = (user && user.user && user.user.modules && this.parseIcons(user.user.modules)) || [];
	}

	private parseIcons(list: ApplicationUserModuleModel[]): ApplicationUserModuleModel[] {
		return list.map(el => {
			const item = this.icons.find(icon => icon.name === el.module.icon);
			el.module.icon = (item && item.icon) || el.module.icon;
			return el;
		});
	}
}
