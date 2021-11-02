import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BulletinModel } from '../../utils/models/bulletin.model';

@Injectable({
  providedIn: 'root'
})
export class BulletinService extends AbstractService {
  private readonly urlRoute: string = 'Bulletin';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<ApiResponse<BulletinModel>> {
    return this.http.get<ApiResponse<BulletinModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: BulletinModel): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<ApiResponse<any>> {
    return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  delete(id: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  list(data: any): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
}
