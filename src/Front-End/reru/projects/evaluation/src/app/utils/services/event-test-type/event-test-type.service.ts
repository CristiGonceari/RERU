import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventTestTypeService extends AbstractService {
  private readonly urlRoute = 'EventTestTemplate';

	constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}
  
  getTestTypes(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, {params});
  }

  attachTestType(data){
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  detachTestType(eventId, testTypeId){
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/Event=${eventId}&&TestType=${testTypeId}`);
  }
}
