import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BulletinModel } from '../../utils/models/bulletin.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';


@Injectable({
  providedIn: 'root'
})
export class BulletinService extends AbstractService {
  private readonly urlRoute: string = 'Bulletin';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<Response<BulletinModel>> {
    return this.http.get<Response<BulletinModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: BulletinModel): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }
  
}
