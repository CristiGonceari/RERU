import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { RankModel } from '../models/rank.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
@Injectable({
  providedIn: 'root'
})
export class RankService extends AbstractService {
  private readonly urlRoute = 'Rank';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  create(data: RankModel): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  update(data: RankModel): Observable<ApiResponse<number>> {
    return this.http.patch<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  delete(id: number): Observable<ApiResponse<number>> {
    return this.http.delete<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  list(data): Observable<ApiResponse<RankModel[]>> {
    return this.http.get<ApiResponse<RankModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }

}
