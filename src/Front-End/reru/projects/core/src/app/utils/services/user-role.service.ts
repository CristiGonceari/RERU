import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserRoleModel } from '../models/user-role.model';

@Injectable({
  providedIn: 'root'
})
export class UserRoleService extends AbstractService {
  private readonly urlRoute = 'Role';

  constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
    super(appConfigService);
  }

  get(id: number): Observable<any> {
    return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}/${id}`);
  }

  getList(params): Observable<any> {
    return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}`, { params });
  }

  delete(id: number): Observable<UserRoleModel> {
    return this.client.delete<UserRoleModel>(`${this.coreUrl}/${this.urlRoute}/${id}`);
  }

  create(data: UserRoleModel): Observable<UserRoleModel> {
    return this.client.post<UserRoleModel>(`${this.coreUrl}/${this.urlRoute}`, data);
  }

  edit(data: UserRoleModel): Observable<UserRoleModel> {
    return this.client.patch<UserRoleModel>(`${this.coreUrl}/${this.urlRoute}`, data);
  }

  print(data): Observable<any> {
    return this.client.put(`${this.coreUrl}/${this.urlRoute}/print`, data, {
      responseType: 'blob',
      observe: 'response',
    });
  }

  bulkImport(data): Observable<any> {
		return this.client.put(`${this.coreUrl}/${this.urlRoute}/excel-import`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
