import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root'
})

export class SolicitedVacantPositionUserFileService extends AbstractService {

	private readonly urlRoute = 'SolicitedVacantPositionUserFile';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

	addFile(data): Observable<any> {
		return this.http.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	create(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data, { 
			reportProgress: true,
			observe: 'events',
			responseType: 'blob' as 'json'
		});
	}

	get(fileId: string): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${fileId}`, { responseType: 'blob', observe: 'response' });
	}

	getList(params): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/files`, { params });
	}

	getCheckedFiles(params): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/check-files`, { params });
	}

	deleteFile(id): Observable<any> {
		return this.http.delete<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

}
