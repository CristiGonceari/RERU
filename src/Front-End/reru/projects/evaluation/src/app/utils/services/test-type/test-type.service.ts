import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})

export class TestTypeService extends AbstractService {

  private readonly urlRoute = 'TestTemplate';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getActiveTestTypes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`);
  }

  addTestType(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  editTestType(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  getTestType(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}` );
  }

  getTestTypes(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  deleteTestType(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  addRules(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/rules`, data);
  }

  getRules(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/rules/${id}`);
  }

  validateTestType(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/validate`, {params});
  }

  addEditTestTypeSettings(data): Observable<any>{
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/settings`, data);
  }

  getTestTypeSettings(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/settings`, { params });
  }

  changeStatus(data): Observable<any>{
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/status`, data);
  }

  getTestTypeByEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/EventTestType/no-assigned`, { params });
  }

  clone(testTypeId: number): Observable<any>{
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/clone`, { testTypeId });
  }

  getTestTypeByStatus( params ): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/status`, { params });
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
