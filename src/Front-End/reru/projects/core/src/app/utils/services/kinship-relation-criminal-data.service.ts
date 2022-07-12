import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KinshipRelationCriminalDataModel } from '../models/kinship-relation-criminal-data.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class KinshipRelationCriminalDataService extends AbstractService {

  private readonly urlRoute: string = 'KinshipRelationCriminalData';
  
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<Response<KinshipRelationCriminalDataModel>> {
    return this.http.get<Response<KinshipRelationCriminalDataModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: KinshipRelationCriminalDataModel): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }
}
