import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Contractor } from '../models/contractor.model';
import { PermissionModel } from '../models/permission.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
	providedIn: 'root',
})
export class ContractorService extends AbstractService {
	private readonly routeUrl: string = 'Contractor';
	public fetchContractor: BehaviorSubject<void> = new BehaviorSubject(null);
	public contractor: BehaviorSubject<Contractor> = new BehaviorSubject(null);
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	createContractor(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}`, { data });
	}

	get(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}
	
    getAvatar(id: number):Observable<any>{
        return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}/avatar`)
	}

	update(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}`, { data });
	}

	updateName(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}/name`, { data });
	}

	delete(id: number): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	list(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}

	getDocuments(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/files`, { params: data });
	}

	uploadPhoto(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}/avatar`, data);
	}

	updatePermissions(data: PermissionModel): Observable<ApiResponse<number>> {
		return this.http.put<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}/contractor-permissions`, data);
	}

	getPermissions(id: number): Observable<ApiResponse<PermissionModel>> {
		return this.http.get<ApiResponse<PermissionModel>>(`${this.baseUrl}/${this.routeUrl}/contractor-permissions/${id}`);
	}
}
