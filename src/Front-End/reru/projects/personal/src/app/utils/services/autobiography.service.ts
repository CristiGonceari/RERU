import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AutobiographyModel } from '../models/autobiography.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class AutobiographyService extends AbstractService {
  private readonly urlRoute: string = 'Autobiography';
  
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<Response<AutobiographyModel>> {
    return this.http.get<Response<AutobiographyModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: AutobiographyModel): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }
}
