import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class UserProfileService extends AbstractService {
	private readonly routeUrl: string = 'UserProfile';
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	addAccess(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
	}

  resetPassword(id: number): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}`, {});
  }

  listModules(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}/module-roles`);
  }
}
