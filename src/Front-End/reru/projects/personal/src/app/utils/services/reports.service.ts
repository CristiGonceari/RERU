import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class ReportsService extends AbstractService {
	private readonly urlRoute = 'Report';

	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	add(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, { data });
	}

	edit(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, { data });
	}

	delete(id: number) {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	list(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	print(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/reports-list`, {
			params: data,
			responseType: 'blob',
			observe: 'response',
		});
	}
}
