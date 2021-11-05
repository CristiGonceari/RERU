import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { NomenclatureRecordModel } from '../models/nomenclature-record.model';

@Injectable({
  providedIn: 'root'
})
export class NomenclatureRecordService extends AbstractService {
  private readonly urlRoute = 'NomenclatureRecord';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

   create(data: NomenclatureRecordModel): Observable<ApiResponse<NomenclatureRecordModel>> {
    return this.http.put<ApiResponse<NomenclatureRecordModel>>(`${this.baseUrl}/${this.urlRoute}/create`, data);
  }

   update(data: NomenclatureRecordModel): Observable<ApiResponse<NomenclatureRecordModel>> {
     return this.http.put<ApiResponse<NomenclatureRecordModel>>(`${this.baseUrl}/${this.urlRoute}/update`, data);
   }

   disable(id: number): Observable<ApiResponse<number>> {
     return this.http.patch<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}/disable/${id}`, {});
   }

   list(data): Observable<ApiResponse<NomenclatureRecordModel[]>> {
    return this.http.get<ApiResponse<NomenclatureRecordModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
}
