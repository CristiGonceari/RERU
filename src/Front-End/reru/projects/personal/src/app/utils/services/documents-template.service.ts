import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import {DocumentsTemplate} from '../models/documents-templates.model';
import {DocumentsTemplateListOfKeys} from '../models/DocumentsTemplatesListOfKeys';

@Injectable({
  providedIn: 'root'
})
export class DocumentsTemplateService extends AbstractService {
  private readonly urlRoute = 'DocumentTemplate';
  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) 
  { 
    super(appConfigService); 
  }

  create(data: DocumentsTemplate): Observable<DocumentsTemplate> {
		return this.http.post<DocumentsTemplate>(`${this.baseUrl}/${this.urlRoute}`, {data: data});
	}

  list(data): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
  getById(id: number): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}
  getListOfKeys(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/keys`);
  }

  edit(data: DocumentsTemplate): Observable<DocumentsTemplate> {
		return this.http.patch<DocumentsTemplate>(`${this.baseUrl}/${this.urlRoute}`, {data: data});
	}
  
  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }
}
