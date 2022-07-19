import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProcessService extends AbstractService {

  private readonly urlRoute = 'Process';
  
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getProgressHistory(): Observable<any> {
    return this.http.get(`${this.coreUrl}/${this.urlRoute}`);
  }

  closeAllProcesses(): Observable<any> {
    return this.http.get(`${this.coreUrl}/${this.urlRoute}/close`);
  }
}
