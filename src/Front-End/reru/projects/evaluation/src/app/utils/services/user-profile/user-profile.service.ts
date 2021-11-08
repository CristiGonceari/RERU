import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService extends AbstractService {

  private readonly urlRoute = 'UserProfile';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  create(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/create`, data);
  }

  getCurrentUser(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my`);
  }

  getUser(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getUserProfiles(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

  getUserProfilesByLocation(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-location`, { params });
  }

  getUserProfilesByEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-event`, { params });
  }

  getUserProfilesByPlan(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-plan`, { params });
  }

  getUserProfilesByAttachedUserEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-event-as-user`, { params });
  }

  getUserProfilesByEvaluatorEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-evaluator`, { params });
  }
}
