import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { saveAs } from 'file-saver';

@Injectable({
  providedIn: 'root'
})

export class TestTemplateService extends AbstractService {

  private readonly urlRoute = 'TestTemplate';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getActiveTestTemplates(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`);
  }

  addTestTemplate(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  editTestTemplate(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  changeTestTesmplateCanBeSolicited(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/can-be-solicited`, data);
  }

  getTestTemplate(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}` );
  }

  getTestTemplates(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  deleteTestTemplate(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  addRules(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/rules`, data);
  }

  getRules(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/rules/${id}`);
  }

  validateTestTemplate(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/validate`, {params});
  }

  addEditTestTemplateSettings(data): Observable<any>{
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/settings`, data);
  }

  getTestTemplateSettings(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/settings`, { params });
  }

  changeStatus(data): Observable<any>{
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/status`, data);
  }

  clone(testTemplateId: number): Observable<any>{
    return this.http.post(`${this.baseUrl}/${this.urlRoute}/clone`, { testTemplateId: testTemplateId });
  }

  getTestTemplateByStatus( params ): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/status`, { params });
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

  getTestTemplateDocumentReplacedKeys(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/getTestTemplateRelacedKeys`, {params});
  }

  printDocument(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/getPDF?source=${encodeURIComponent(data.source)}&testTemplateName=${data.testTemplateName}`, {
			responseType: 'blob',
			observe: 'response',
		});
	}

}
