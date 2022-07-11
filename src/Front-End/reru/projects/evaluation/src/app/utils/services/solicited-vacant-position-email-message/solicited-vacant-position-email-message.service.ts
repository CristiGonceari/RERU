import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SolicitedVacantPositionEmailMessageService extends AbstractService{
  private readonly urlRoute = 'SolicitedVacantPositionEmailMessage';

  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) { 	
    super(appConfigService);
  }

  getMessage(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  changeStatusAndSendEmail(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}
}
