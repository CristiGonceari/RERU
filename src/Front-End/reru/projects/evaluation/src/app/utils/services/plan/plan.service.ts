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
  private readonly urlRoute3 = 'PlanResponsiblePerson';

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

  delete(id): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  events(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute2}`, {params});
  }

  attachEvent(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute2}`, data);
  }

  detachEvent(data){
    return this.http.patch(`${this.baseUrl}/${this.urlRoute2}`, data);
  }

  persons(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute3}`, {params});
  }

  attachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute3}`, data);
  }

  detachPerson(id, itemId){
    return this.http.delete(`${this.baseUrl}/${this.urlRoute3}/Plan=${id}&&UserProfile=${itemId}`);
  }

  getNoAssignedPersonToPlans(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute3}/no-assigned`, {params});
  }
}
