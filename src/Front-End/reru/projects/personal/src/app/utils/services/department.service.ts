import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { DepartmentModel } from '../models/department.model';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService extends AbstractService {
  private readonly urlRoute = 'Department';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<ApiResponse<DepartmentModel>> {
    return this.http.get<ApiResponse<DepartmentModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: DepartmentModel): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  update(data: DepartmentModel): Observable<ApiResponse<any>> {
    return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  delete(id: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<DepartmentModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  list(data: any): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
}
