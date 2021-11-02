import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { NomenclatureColumnModel } from '../models/nomenclature-columns.model';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class NomenclatureColumnService extends AbstractService {
  private readonly urlRoute = 'NomenclatureColumn';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

   get(id: number): Observable<ApiResponse<NomenclatureColumnModel>> {
     return this.http.get<ApiResponse<NomenclatureColumnModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
   }

   update(data: NomenclatureColumnModel): Observable<ApiResponse<NomenclatureColumnModel>> {
     return this.http.put<ApiResponse<NomenclatureColumnModel>>(`${this.baseUrl}/${this.urlRoute}`, data);
   }
}
