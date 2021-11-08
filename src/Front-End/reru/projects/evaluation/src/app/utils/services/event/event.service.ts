import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService extends AbstractService {
  private readonly urlRoute = 'EventsConstroller';

  private userId: BehaviorSubject<any> = new BehaviorSubject(null);
  public uploadUsers: BehaviorSubject<void> = new BehaviorSubject(null);

	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

  setData = (value: any) => {this.userId.next(value)}

  getEvents(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

  getMyEvents(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/mine`, { params });
  }

  addEvent(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/create`, data);
  }

  editEvent(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  getEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getDetailsEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/details`, { params });
  }

  deleteEvent(params): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getResponsiblePersons(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/responsible`, {params});
  }

  attachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/assign-person`, data);
  }

  detachPerson(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-person`, data);
  }

  getLocations(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/locations`, {params});
  }

  attachLocation(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/attach-location`, data);
  }

  detachLocation(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-location`, data);
  }

  getUsers(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/users`, {params});
  }

  attachUser(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/assign-user`, data);
  }

  detachUser(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-user`, data);
  }

  getTestTypes(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/testtypes`, {params});
  }

  attachTestType(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/attach-testtype`, data);
  }

  detachTestType(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-testtype`, data);
  }

  eventsWihoutPlan(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-plan`, {params});
  }

  getTemplate(): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/bulk-template`, {
      responseType: 'blob' as 'json',
			observe: 'response' as 'body',
		});
	}

  bulkUpload(id, file): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/${this.urlRoute}/${id}/bulk-assign-user`, file, {
    responseType: 'blob' as 'json',
    observe: 'response' as 'body',
  });
  }

  getEvaluators(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/evaluators`, {params});
  }

  attachEvaluator(data) {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/attach-evaluator`, data);
  }

  detachEvaluator(data) {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/detach-evaluator`, data);
  }
}
