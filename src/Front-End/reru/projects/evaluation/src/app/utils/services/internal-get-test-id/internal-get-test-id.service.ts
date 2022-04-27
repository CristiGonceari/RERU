import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InternalGetTestIdService extends AbstractService {
  private readonly urlRoute: string = 'InternalNotifications';

  constructor(
    protected appConfigService: AppSettingsService,
    private http: HttpClient) {
    super(appConfigService);
  }


  getTestIdForFastStart(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/test-notification`);
  }
}
