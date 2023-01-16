import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class DepartmentContentService extends AbstractService {
  private readonly routeUrl: string = 'DepartmentRoleContent';
  constructor(protected appSettingsService: AppSettingsService,
              private http: HttpClient) {
    super(appSettingsService);
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
  }

  getTemplate(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}/template/${id}`);
  }

  getCalculated(id: number, type: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}/calculated/${id}/${type}`);
  }

  getDasboard(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}/dashboard/${id}`);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }
}
