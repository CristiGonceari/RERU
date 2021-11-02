import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { VacationProfileModel } from '../models/vacation-profile.model';
import { VacantionModel } from '../models/vacantion.model';

@Injectable({
	providedIn: 'root',
})
export class VacantionService extends AbstractService {
	private readonly routeUrl: string = 'Vacation';

	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	create(data: VacantionModel): Observable<ApiResponse<any>> {
		return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

    get(data): Observable<ApiResponse<VacantionModel[]>> {
         	return this.http.get<ApiResponse<VacantionModel[]>>(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}
}
