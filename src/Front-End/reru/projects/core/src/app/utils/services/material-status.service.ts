import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MaterialStatusModel } from '../models/material-status.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class MaterialStatusService extends AbstractService {
  private readonly urlRoute: string = 'MaterialStatus';
  
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<Response<MaterialStatusModel>> {
    return this.http.get<Response<MaterialStatusModel>>(`${this.coreUrl}/${this.urlRoute}/${id}`);
  }

  add(data: MaterialStatusModel): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.coreUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.coreUrl}/${this.urlRoute}`, data);
  }
}
