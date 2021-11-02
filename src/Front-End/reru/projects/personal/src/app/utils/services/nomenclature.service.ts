import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class NomenclatureService extends AbstractService {
  private readonly urlRoute = 'Nomenclature';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(data: {nomenclatureType: string, nomenclatureId: string}): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }

  add(data): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  edit(data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, { data });
  }

  delete(data): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params: data });
  }

  nomenclatureTypeList(type: string, data): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${type}`, { params: data });
  }
}
