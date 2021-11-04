import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { VacationProfileModel } from '../models/vacation-profile.model';

@Injectable({
	providedIn: 'root',
})
export class VacationProfileService extends AbstractService {
	private readonly routeUrl: string = 'profile/VacationProfile';

	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	get(data): Observable<ApiResponse<VacationProfileModel[]>> {
		return this.http.get<ApiResponse<VacationProfileModel[]>>(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}

	create(data: VacationProfileModel): Observable<ApiResponse<any>> {
		return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	update(data): Observable<ApiResponse<any>> {
		return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	getAvailableDays(data): Observable<ApiResponse<number>> {
		return this.http.get<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}/available`, {params: data});
	}
	
	getAvailableDaysForSelectedContractor(data): Observable<ApiResponse<number>> {
		return this.http.get<ApiResponse<number>>(`${this.baseUrl}/Vacation/available`, {params: data});
	}

	getIntervalDays(data): Observable<ApiResponse<number>> {
		return this.http.get<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}/interval`, { params: data });
	}

	getSubordinateVacations(data): Observable<ApiResponse<VacationProfileModel[]>> {
		return this.http.get<ApiResponse<VacationProfileModel[]>>(
			`${this.baseUrl}/${this.routeUrl}/subordinate-vacations`,
			{ params: data }
		);
	}
}
