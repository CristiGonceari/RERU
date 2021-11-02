import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { ContractorType } from '../models/contractor-type.model';
import { AbstractService } from './abstract.service';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class ContractorTypeService extends AbstractService {
  private readonly routeUrl: string = 'ContractorType';
  constructor(protected appConfig: AppConfigService,
    private http: HttpClient) {
    super(appConfig);
  }

  createContractorType(data: ContractorType): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.routeUrl}`, { data });
  }

  get(id: number): Observable<ApiResponse<ContractorType>> {
    return this.http.get<ApiResponse<ContractorType>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }

  edit(data: ContractorType): Observable<ApiResponse<number>> {
    return this.http.patch<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}`, { data });
  }

  delete(id: number): Observable<ApiResponse<number>> {
    return this.http.delete<ApiResponse<number>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }

  list(data): Observable<ApiResponse<ContractorType[]>> {
    return this.http.get<ApiResponse<ContractorType[]>>(`${this.baseUrl}/${this.routeUrl}`, { params: data });
  }
}
