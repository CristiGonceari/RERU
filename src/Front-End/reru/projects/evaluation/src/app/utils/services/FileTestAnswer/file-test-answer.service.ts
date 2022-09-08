import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileTestAnswerService extends AbstractService {
  private readonly urlRoute = 'FileTestAnswer';
  
  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

  create(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}

  getList(params): Observable<any> {
		return this.http.get<any>(`${this.baseUrl}/${this.urlRoute}/file`, { params });
	}

  getFile(fileId: string): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${fileId}`, { responseType: 'blob', observe: 'response' });
	}
}
