import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlanService extends AbstractService {
  private readonly urlRoute = 'Plans';
  private eventId: BehaviorSubject<any> = new BehaviorSubject(null);
	event = this.eventId.asObservable();
  private userId: BehaviorSubject<any> = new BehaviorSubject(null);
	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

  setEvent = (value: any) => {this.eventId.next(value)}
  setUser = (value: any) => {this.userId.next(value)}

  list(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/create`, data);
  }

  edit(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  get(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  details(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/details`, { params });
  }

  delete(params): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  events(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/events`, { params });
  }

  attachEvent(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/assign-event`, data);
  }

  detachEvent(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-event`, data);
  }

  persons(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/responsible`, { params });
  }

  attachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/assign-person`, data);
  }

  detachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-person`, data);
  }
}
