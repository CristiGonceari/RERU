import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InternalGetTestIdService extends AbstractService {
  private readonly routeUrl: string = 'InternalGetTestId';

  constructor(
    protected appConfigService: AppSettingsService,
    private http: HttpClient) {
    super(appConfigService);
  }


  getTestIdForFastStart(): Observable<any> {
    let internalBaseUrl = this.baseUrl.replace("/api", "/internal/api");

    return this.http.get<any>(`${internalBaseUrl}/${this.routeUrl}`);
  }
}
