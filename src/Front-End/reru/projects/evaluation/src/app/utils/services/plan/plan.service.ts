import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlanService extends AbstractService {
  private readonly urlRoute = 'Plan';
  private readonly urlRoute2 = 'PlanEvent';

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
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  edit(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  get(id): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  delete(params): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  events(id): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute2}/events-by-${id}`);
  }

  attachEvent(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute2}/assign-event`, data);
  }

  detachEvent(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute2}/unassign-event`, data);
  }

  persons(id): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute2}/responsible-persons-by-${id}`);
  }

  attachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute2}/assign-person`, data);
  }

  detachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute2}/unassign-person`, data);
  }
}
