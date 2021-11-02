import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { ContractModel } from '../models/contract.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
@Injectable({
	providedIn: 'root',
})
export class ContractService extends AbstractService {
	private readonly routeUrl: string = 'Contract';
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	get(id: number): Observable<ApiResponse<any>> {
		return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	create(data: ContractModel): Observable<ApiResponse<any>> {
		return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	update(data: ContractModel): Observable<ApiResponse<any>> {
		return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	delete(id: number): Observable<ApiResponse<any>> {
		return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}
}
