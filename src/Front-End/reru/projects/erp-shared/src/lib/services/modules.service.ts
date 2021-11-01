import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAppSettings } from '../models/app-settings.model';
import { ApplicationUserModel } from '../models/application-user.model';
import { IconModel } from '../models/icon.model';
import * as data from '../assets/icons.json';

@Injectable({
	providedIn: 'root',
})
export class ModulesService {
	isSubscribed: boolean;
	authModel: ApplicationUserModel;
	icons: IconModel[] = (data as any).default;
	constructor(private http: HttpClient) {}

	adminList(config: IAppSettings): Observable<any> {
		return this.http.get(`${config.CORE_URL}/api/admin/module`);
	}
}
