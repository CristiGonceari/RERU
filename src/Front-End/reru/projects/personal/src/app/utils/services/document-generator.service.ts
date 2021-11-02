import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import {DocumentsTemplate} from '../models/documents-templates.model';


@Injectable({
  providedIn: 'root'
})
export class DocumentGeneratorService extends AbstractService {

  private readonly urlRoute = 'DocumentsGenerator';

  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) 
  { 
    super(appConfigService); 
  }

  getById(id: number): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

  getDocuments(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}
}
