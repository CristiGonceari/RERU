import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventTestTemplateService extends AbstractService {
  private readonly urlRoute = 'EventTestTemplate';

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}
  
  getTestTemplates(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, {params});
  }

  getTestTemplateByEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventTestTemplate/no-assigned`, { params });
  }

  attachTestTemplate(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  detachTestTemplate(eventId, testTemplateId){
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/Event=${eventId}&&TestTemplate=${testTemplateId}`);
  }

  getTestTemplateByEventsIds(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventTestTemplate/by-event`, { params });
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
