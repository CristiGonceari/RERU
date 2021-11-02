import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class PositionService extends AbstractService {
	private readonly routeUrl: string = 'Position';
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	create(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}`, { data });
	}

	retrieveCurrentPosition(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/current/${id}`);
	}

	changeCurrentPosition(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}/current`, { data });
	}

	addPreviousPosition(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}/previous`, { data });
	}

	transferToNewPosition(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}/transfer`, { data });
	}

	dismissCurrent(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.routeUrl}/dismiss-current`, data);
	}

	list(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}
}
