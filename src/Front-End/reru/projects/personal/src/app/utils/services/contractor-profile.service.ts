import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BulletinModel } from '../models/bulletin.model';

@Injectable({
	providedIn: 'root',
})
export class ContractorProfileService extends AbstractService {
  private readonly routeUrl: string = 'profile/ContractorProfile';
  constructor(protected appConfig: AppSettingsService, private http: HttpClient) {
    super(appConfig);
  }

  getProfile(data): Observable<ApiResponse<BulletinModel>> {
    return this.http.get<ApiResponse<BulletinModel>>(`${this.baseUrl}/${this.routeUrl}/profile`, { params: data });
  }
  
  getProfileAvatar(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/profile/Avatar`, { params: data });
  }

  getBulletin(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/bulletin`, { params: data });
  }

  getContract(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/contract`, { params: data });
  }

  getPositions(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/positions`, { params: data });
  }

  getStudies(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/studies`, { params: data });
  }

  getFiles(data): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/files`, { params: data });
  }

  getPermissions(): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/permissions`);
  }

  getRanks(): Observable<ApiResponse<any>> {
    return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.routeUrl}/ranks`);
  }

  getFamilyMember(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${this.routeUrl}/family-members`);
  }

  getProfileTimeSheetTable(data): Observable<any> {
    return this.http.get<Observable<any>>(`${this.baseUrl}/${this.routeUrl}/time-sheet-table`, {  params: data });
  }
  
}
