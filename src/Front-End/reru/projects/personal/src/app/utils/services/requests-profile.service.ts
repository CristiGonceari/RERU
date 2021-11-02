import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { RequestProfileModel } from '../models/request-profile.model';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class RequestsProfileService extends AbstractService {
  private readonly urlRoute = 'requestProfile';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

   get(data): Observable<ApiResponse<RequestProfileModel[]>> {
     return this.http.get<ApiResponse<RequestProfileModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
   }

   create(from: string): Observable<ApiResponse<number>> {
     return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, { from });
   }

   update(data): Observable<ApiResponse<any>> {
    return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

   getSubordinateRequests(data): Observable<ApiResponse<RequestProfileModel[]>> {
     return this.http.get<ApiResponse<RequestProfileModel[]>>(`${this.baseUrl}/${this.urlRoute}/subordinate-requests`, { params: data });
   }
}
