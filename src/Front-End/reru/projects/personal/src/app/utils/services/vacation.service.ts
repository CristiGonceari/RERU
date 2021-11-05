import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class VacationService extends AbstractService {
	private readonly routeUrl: string = 'vacation';
	constructor(private http: HttpClient, protected configService: AppSettingsService) {
		super(configService);
	}

	get(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	edit(data: FormData): Observable<ApiResponse<any>> {
		return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	add(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	delete(id: number): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	list(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}

	downloadRequest(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/file/${id}`, { responseType: 'blob', observe: 'response' });
	}

	updateFile(data: FormData): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}/file-update`, data);
	}

	retrieveAvailableDays(): Observable<ApiResponse<number>> {
		return this.http.get<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}/available`);
	}
}
