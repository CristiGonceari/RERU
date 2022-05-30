import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RegistrationPageService extends AbstractService {

  private readonly routeUrl: string = 'RegistrationPage';

  constructor(protected configService: AppSettingsService, private client: HttpClient) {
    super(configService);
  }

  getMessage(): Observable<any> {
    return this.client.get<any>(`${this.coreUrl}/${this.routeUrl}/message`);
  }

  editMessage(data): Observable<any> {
    return this.client.patch(`${this.coreUrl}/${this.routeUrl}`, data);
  }
}
