import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService extends AbstractService {
  private readonly urlRoute = 'Event';

  private userId: BehaviorSubject<any> = new BehaviorSubject(null);
  public uploadUsers: BehaviorSubject<void> = new BehaviorSubject(null);

	user = this.userId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

  setData = (value: any) => {this.userId.next(value)}

  getEvents(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getMyEvents(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-events`, { params });
  }

  getMyEventsCount(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-events-count`, { params });
  }

  getMyEventsByDate(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-events-by-date`, { params });
  }

  addEvent(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  editEvent(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  getEvent(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  getDetailsEvent(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  deleteEvent(id): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  getResponsiblePersons(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/EventResponsiblePerson`, {params});
  }

  attachPerson(data){
    return this.http.post(`${this.baseUrl}/EventResponsiblePerson`, data);
  }

  detachPerson(eventId, userProfileId){
    return this.http.delete(`${this.baseUrl}/EventResponsiblePerson/Event=${eventId}&&UserProfile=${userProfileId}`);
  }

  getLocations(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/EventLocation`, {params});
  }

  attachLocation(data){
    return this.http.post(`${this.baseUrl}/EventLocation`, data);
  }

  detachLocation(eventId , locationId): Observable<any>{
    // console.log("data", data);
    return this.http.delete(`${this.baseUrl}/EventLocation/Event=${eventId}&&Location=${locationId}`);
  }

  getUsers(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/EventUser`, {params});
  }

  attachUser(data){
    return this.http.post(`${this.baseUrl}/EventUser`, data);
  }

  detachUser(eventId, userProfileId){
    return this.http.delete(`${this.baseUrl}/EventUser/Event=${eventId}&&User=${userProfileId}`);
  }

  eventsWihoutPlan(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/PlanEvent/not-assigned`, {params});
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
    return this.http.get(`${this.baseUrl}/EventEvaluator`, {params});
  }

  getNoAssignedEvaluators(params): Observable<any> {
    return this.http.get(`${this.baseUrl}EventEvaluator/no-assigned`, {params});
  }

  attachEvaluator(data) {
    return this.http.post(`${this.baseUrl}/EventEvaluator`, data);
  }

  detachEvaluator(eventId, evaluatorId) {
    return this.http.delete(`${this.baseUrl}/EventEvaluator/Event=${eventId}&&Evaluator=${evaluatorId}`);
  }

  getUserEvents(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-events`, { params });
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-events`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
