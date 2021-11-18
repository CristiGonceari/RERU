import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';

import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttachToLocationService extends AbstractService  {

  private readonly urlRouteLocationComputer = 'LocationComputer';
  private readonly urlRouteLocationResponsiblePerson = 'LocationResponsiblePerson';

	private userId: BehaviorSubject<any> = new BehaviorSubject(null);
	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

	setData = (value: any) => {
		this.userId.next(value);
	};

  	assignPerson(data) {
		return this.http.post(`${this.baseUrl}/${this.urlRouteLocationResponsiblePerson}/assign-person`, data);
	}

	detachPerson(data) {
		return this.http.post(`${this.baseUrl}/${this.urlRouteLocationResponsiblePerson}/detach-person`, data);
	}

	getPersons(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRouteLocationResponsiblePerson}`, { params });
	}

	getComputers(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRouteLocationComputer}/computers`, { params });
	}

	detachComputer(params) {
		return this.http.post(`${this.baseUrl}/${this.urlRouteLocationComputer}/detach-computer`, params);
	}
}
