import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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

  getCurrentUser(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my`);
  }

  getUser(id): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  get(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params })
  }

  getByTestTemplate(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-roles`, { params })
  }

  getUserProfiles(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

  getUserProfilesByLocation(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/LocationResponsiblePerson/no-assigned`, { params });
  }

  getUserProfilesByEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventResponsiblePerson/no-assigned`, { params });
  }

  getUserProfilesByPlan(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-plan`, { params });
  }

  getUserProfilesByAttachedUserEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventUser/no-assigned`, { params });
  }

  getUserProfilesByEvaluatorEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventEvaluator/no-assigned`, { params });
  }
}
