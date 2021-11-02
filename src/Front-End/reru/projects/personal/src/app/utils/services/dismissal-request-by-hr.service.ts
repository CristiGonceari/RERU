import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { RequestProfileModel } from '../models/request-profile.model';

@Injectable({
  providedIn: 'root'
})
export class DismissalRequestByHrService extends AbstractService{

  private readonly urlRoute = 'DismissalFlux';

  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

  get(data): Observable<ApiResponse<RequestProfileModel[]>> {
    return this.http.get<ApiResponse<RequestProfileModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }

  create(data: RequestProfileModel): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`,  {data} );
  }
}
