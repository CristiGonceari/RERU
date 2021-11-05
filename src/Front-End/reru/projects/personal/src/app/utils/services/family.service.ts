import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { FamilyModel } from '../models/family.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
@Injectable({
  providedIn: 'root'
})
export class FamilyService extends AbstractService {
  private readonly urlRoute = 'FamilyMember';
  constructor(private http: HttpClient, protected configService: AppSettingsService) {
    super(configService);
  }

  get(id: number): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  create(data: FamilyModel): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  update(data: FamilyModel): Observable<ApiResponse<any>> {
    return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  delete(id: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  list(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
}
