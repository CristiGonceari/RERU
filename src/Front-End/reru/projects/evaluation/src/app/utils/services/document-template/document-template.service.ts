import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { DocumentTemplate } from '../../models/document-template/document-templates.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentTemplateService extends AbstractService {
  private readonly urlRoute = 'DocumentTemplate';
  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) 
  { 
    super(appConfigService); 
  }

  create(data: DocumentTemplate): Observable<DocumentTemplate> {
		return this.http.post<DocumentTemplate>(`${this.baseUrl}/${this.urlRoute}`, {data: data});
	}

  list(data): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
  getById(id: number): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

  getListOfKeys(data): Observable<any>{
    return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/keys`, {params: data });
  }

  edit(data: DocumentTemplate): Observable<DocumentTemplate> {
		return this.http.patch<DocumentTemplate>(`${this.baseUrl}/${this.urlRoute}`, {data: data});
	}
  
  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }
  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
