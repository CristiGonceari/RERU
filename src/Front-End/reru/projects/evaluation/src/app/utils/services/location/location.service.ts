import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';

import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class LocationService extends AbstractService {
	private readonly urlRoute = 'Locations';

	private userId: BehaviorSubject<any> = new BehaviorSubject(null);
	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

	setData = (value: any) => {
		this.userId.next(value);
	};

	getLocations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
	}

	addLocation(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/create`, data);
	}

	editLocation(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getLocation(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	getDetailsLocation(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/details`, { params });
	}

	deleteLocation(params): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	assignPerson(data) {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/assign-person`, data);
	}

	detachPerson(data) {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-person`, data);
	}

	getPersons(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/responsible`, { params });
	}

	getLocationsByEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-event`, { params });
	}

	getClients(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/clients`, { params });
	}

	detachClient(params) {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-client`, params);
	}
}
