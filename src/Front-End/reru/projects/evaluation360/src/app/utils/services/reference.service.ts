import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class ReferenceService extends AbstractService {
	private readonly routeUrl = 'Reference';
	constructor(protected readonly configService: AppSettingsService, 
				private readonly http: HttpClient) {
		super(configService);
	}

	listDepartments(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/departments/select-items`);
	}

	listRoles(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/roles/select-items`);
	}

	listUserStatuses(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/user-status/select-items`);
	}
}
