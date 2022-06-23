import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequiredDocumentService extends AbstractService{
  private readonly urlRoute = 'RequiredDocument';

  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getRequiredDocument(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  getRequiredDocuments(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  delete(id: number){
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

}
