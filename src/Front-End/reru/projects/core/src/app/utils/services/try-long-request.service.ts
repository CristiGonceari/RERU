import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TryLongRequestService extends AbstractService {

  private readonly routeUrl: string = 'TryLongRequest';

  constructor(protected configService: AppSettingsService, private client: HttpClient) {
    super(configService);
  }

  getLongRequest(): Observable<string> {
    return this.client.get<string>(`${this.coreUrl}/${this.routeUrl}`);
  }
  
}
