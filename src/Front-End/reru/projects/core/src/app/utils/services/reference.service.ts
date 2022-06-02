import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReferenceService extends AbstractService{

  private readonly urlRoute = 'Reference';

  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getProcesses(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/processes-value/select-values`);
  }
  
}
