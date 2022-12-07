import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoggingService extends AbstractService {
  private readonly urlRoute = 'Log';

  constructor(protected appConfigService: AppSettingsService, public http: HttpClient)
   {
    super(appConfigService);
  }
 
  getProjectSelectItem() : Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/project-select-values`);
  }

  getEventSelectItem() : Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/event-select-values`);
  }

  getLoggingValues(params) : Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  deleteLogs(years) : Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${years}`);
  }
  
}
