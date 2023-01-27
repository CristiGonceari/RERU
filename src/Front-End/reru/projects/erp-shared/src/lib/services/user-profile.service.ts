import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService } from './abstract.service';
import { AppSettingsService } from './app-settings.service';

/**
 * Core Module UserProfileService
 */
@Injectable({
  providedIn: 'root'
})
export class UserProfileService extends AbstractService {
  private readonly urlRoute = 'UserProfile';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  get(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params })
  }
}
