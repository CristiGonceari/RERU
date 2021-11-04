import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class DepartmentRoleService extends AbstractService {
  private routeUrl: string = 'DepartmentRoleRelation';
  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
    super(appSettingsService);
   }

   list(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
   }
}
