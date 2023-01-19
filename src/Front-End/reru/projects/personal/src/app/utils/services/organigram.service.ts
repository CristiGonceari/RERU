import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class OrganigramService extends AbstractService {
  private readonly urlRoute = 'OrganizationalChart';
  private readonly relationRoute = 'DepartmentRoleRelation';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  edit(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  list(data): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }

  head(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.relationRoute}/head`, data);
  }

  chart(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.relationRoute}/organizational-chart-content/${id}`);
  }

  relation(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.relationRoute}`, { data });
  }

  deleteRelation(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.relationRoute}/${id}`);
  }

  excelImport(data, id): Observable<any> {
    return this.http.put(`${this.baseUrl}/${this.relationRoute}/excel-import/${id}`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
