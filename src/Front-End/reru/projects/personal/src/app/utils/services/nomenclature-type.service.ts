import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { NomenclatureTypeModel } from '../models/nomenclature-type.model';
import { NomenclatureModel } from '../models/nomenclature.model';

@Injectable({
  providedIn: 'root'
})
export class NomenclatureTypeService extends AbstractService {
  private readonly urlRoute = 'NomenclatureType';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

   get(id: number): Observable<ApiResponse<NomenclatureTypeModel>> {
    return this.http.get<ApiResponse<NomenclatureTypeModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: NomenclatureTypeModel): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  edit(data: NomenclatureTypeModel): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  disable(id: number): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}/${id}`, {});
  }

  list(data): Observable<ApiResponse<NomenclatureTypeModel[]>> {
    return this.http.get<ApiResponse<NomenclatureTypeModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }
}
