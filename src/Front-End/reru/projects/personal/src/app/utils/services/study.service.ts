import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { StudyModel } from '../models/study.model';

@Injectable({
	providedIn: 'root',
})
export class StudyService extends AbstractService {
	private readonly urlRoute: string = 'Study';
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(data): Observable<ApiResponse<StudyModel[]>> {
		return this.http.get<ApiResponse<StudyModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	add(data): Observable<ApiResponse<number>> {
		return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	addMultiple(data): Observable<ApiResponse<number>> {
		return this.http.put<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}/bulk-import`, data);
	}

	update(data: StudyModel): Observable<ApiResponse<any>> {
		return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	delete(id: number): Observable<ApiResponse<any>> {
		return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	list(data: any): Observable<ApiResponse<any>> {
		return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}
}
