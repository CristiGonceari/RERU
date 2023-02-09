import { Injectable } from '@angular/core';
//import { AbstractService } from './abstract.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MyProfile } from '../models/user-profile.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { UserModuleAccessModel } from '../models/user-module-access.model';

@Injectable({
	providedIn: 'root',
})
export class ProfileService extends AbstractService {
	private readonly routeUrl: string = 'profile';

	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	getUserProfile(): Observable<Response<MyProfile>> {
		return this.http.get<Response<MyProfile>>(`${this.coreUrl}/${this.routeUrl}`);
	}

	getCandidateRegistrationSteps(): Observable<Response<any>> {
		return this.http.get<Response<any>>(`${this.coreUrl}/${this.routeUrl}/candidate-registration-steps`);
	}

	uploadAvatar(file): Observable<any> {
		return this.http.post<any>(`${this.coreUrl}/${this.routeUrl}/avatar`, file);
	}

	removeAvatar(): Observable<any> {
		return this.http.delete<any>(`${this.coreUrl}/${this.routeUrl}/remove-avatar`);
	}

	// getUserModuleAccess(id: number): Observable<Response<UserModuleAccessModel[]>> {
	// 	return this.http.get<Response<UserModuleAccessModel[]>>(`${this.coreUrl}/${this.routeUrl}/access/${id}`);
	// }

	print(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
