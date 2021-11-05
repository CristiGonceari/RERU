import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { ContractModel } from '../models/contract.model';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class InstructionService extends AbstractService {
	private readonly routeUrl: string = 'Instruction';

	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	get(id: number): Observable<ApiResponse<any>> {
		return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	add(data): Observable<ApiResponse<number>> {
		return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}`, data);
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
