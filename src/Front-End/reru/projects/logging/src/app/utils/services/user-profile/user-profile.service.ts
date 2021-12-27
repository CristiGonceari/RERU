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

  getCurrentUser(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/my`);
  }

  getUser(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getUserProfiles(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

}
