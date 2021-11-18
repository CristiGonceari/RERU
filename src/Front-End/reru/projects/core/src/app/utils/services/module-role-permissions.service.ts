import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { AbstractService } from './abstract.service';
import { PermissionModel } from '../models/permission.model'
import { Paginated } from '../models/paginated.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { ModuleRolePermissionsModel } from '../models/module-role-permissions.model';

@Injectable({
  providedIn: 'root'
})
export class ModuleRolePermissionsService extends AbstractService {
  private readonly routeUrl: string = 'ModuleRolePermission';

  constructor(protected configService: AppSettingsService, private http: HttpClient) {
    super(configService);
  }

  get(id: number, params: any): Observable<Response<Paginated<PermissionModel>>> {
    return this.http.get<Response<Paginated<PermissionModel>>>(`${this.coreUrl}/${this.routeUrl}?RoleId=${id}`, { params });
  }

  getForUpdate(id: number): Observable<Response<PermissionModel[]>> {
    return this.http.get<Response<PermissionModel[]>>(`${this.coreUrl}/${this.routeUrl}/${id}/edit`);
  }

  updateRolePermission(data: ModuleRolePermissionsModel): Observable<ModuleRolePermissionsModel> {
    return this.http.put<ModuleRolePermissionsModel>(`${this.coreUrl}/${this.routeUrl}`, data);
  }
}
