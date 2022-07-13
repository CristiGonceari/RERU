import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfileGeneralDataModel } from '../../utils/models/user-profile-general-data.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})

export class UserProfileGeneralDataService extends AbstractService {
  private readonly urlRoute: string = 'UserProfileGeneralData';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
  }

  get(id: number): Observable<Response<UserProfileGeneralDataModel>> {
    return this.http.get<Response<UserProfileGeneralDataModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  add(data: UserProfileGeneralDataModel): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
  }
  
}
