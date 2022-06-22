import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { AbstractService } from './abstract.service';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { User } from '../models/user.model';
import { UserModuleAccessModel } from '../models/user-module-access.model';


@Injectable({
	providedIn: 'root'
})
export class UserProfileService extends AbstractService {
	private readonly routeUrl: string = 'UserProfile';

	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	getUserProfile(id: number): Observable<Response<User>> {
		return this.http.get<Response<User>>(`${this.coreUrl}/${this.routeUrl}/${id}`);
	}

	getUserProfiles(data): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}`, { params: data });
	}

	getUserModuleAccess(id: number): Observable<Response<UserModuleAccessModel[]>> {
		return this.http.get<Response<UserModuleAccessModel[]>>(`${this.coreUrl}/${this.routeUrl}/access/${id}`);
	}

	getModuleAccessRole(data: any): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/${data.userId}/module-access/${data.moduleId}`);
	}

	giveModuleAccessRole(params: any): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/module-access`, params );
	}

	removeModuleAccessRole(data: any): Observable<any> {
		return this.http.delete(`${this.coreUrl}/${this.routeUrl}/${data.userId}/module-access/${data.moduleId}`);
	}

	getUserStatus(): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/user-status/select-values`);
	}

	getAccessMode(): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/access-mode/select-values`);
	}

	print(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
