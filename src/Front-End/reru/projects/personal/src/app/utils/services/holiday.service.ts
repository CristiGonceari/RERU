import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { HolidayModel } from '../models/holiday.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
// import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class HolidayService extends AbstractService {
  private readonly routeUrl: string = 'holidays';
  constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
    super(appSettingsService);
  }

  get(data): Observable<ApiResponse<HolidayModel[]>> {
    return this.http.get<ApiResponse<HolidayModel[]>>(`${this.baseUrl}/${this.routeUrl}`, { params: data });
  }

  add(data: HolidayModel): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}`, data);
  }

  update(data: HolidayModel): Observable<ApiResponse<Object>> {
    return this.http.patch<ApiResponse<Object>>(`${this.baseUrl}/${this.routeUrl}`, data);
  }

  delete(id: number): Observable<ApiResponse<Object>> {
    return this.http.delete<ApiResponse<Object>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }
}
