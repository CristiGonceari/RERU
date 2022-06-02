import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetBulkProgressHistoryService extends AbstractService {

  private readonly urlRoute = 'Process';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getProgressHistory(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`);
  }
}
