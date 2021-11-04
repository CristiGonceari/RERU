import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
// import { AppConfigService } from './app-config.service';
import { VacationConfigurationModel } from '../models/vacation-configuration.model';

@Injectable({
  providedIn: 'root'
})
export class VacationConfigurationService extends AbstractService {
  private readonly routeUrl: string = 'vacationConfigurations';
  constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
    super(appSettingsService);
  }

  get(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}`);
  }

  add(data: VacationConfigurationModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
  }
}
