import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})

export class TestTypeService extends AbstractService {

  private readonly urlRoute = 'TestType';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getActiveTestTypes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/Active`);
  }

  addTestType(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  editTestType(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  getTestType(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  getTestTypes(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
  }

  deleteTestType(params): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  addRules(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/rules`, data);
  }

  getRules(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}/rules`);
  }

  validateTestType(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/validate`, {params});
  }

  addEditTestTypeSettings(data): Observable<any>{
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/settings`, data);
  }

  getTestTypeSettings(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/settings`, { params });
  }

  changeStatus(data): Observable<any>{
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/status`, data);
  }

  getTestTypeByEvent(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/not-attached-event`, { params });
  }

  clone(testTypeId: number): Observable<any>{
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/clone`, { testTypeId });
  }
}
