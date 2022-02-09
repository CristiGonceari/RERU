import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class LocationService extends AbstractService {
	private readonly urlRoute = 'Location';
	private readonly urlRoute2 = 'LocationResponsiblePerson';
	private readonly urlRoute3 = 'LocationComputer';

	private userId: BehaviorSubject<any> = new BehaviorSubject(null);
	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

	setData = (value: any) => {
		this.userId.next(value);
	};

	getLocations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	createLocation(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}
	
	editLocation(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getLocation(id: number): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	deleteLocation(id: number): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	assignPerson(data) {
		return this.http.post(`${this.baseUrl}/${this.urlRoute2}`, data);
	}

	detachPerson(locationId, personId) {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute2}/Location=${locationId}&&UserProfile=${personId}`);
	}

	getPersons(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute2}`, { params });
	}

	getLocationsByEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/EventLocation/no-assigned`, { params });
	}

	getComputers(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute3}`, { params });
	}

	unassignedComputer(id): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute3}/${id}`);
	}

	print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
