import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { AbstractService } from './abstract.service';
import { PermissionModel } from '../models/permission.model'
import { Paginated } from '../models/paginated.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class ModulePermissionsService extends AbstractService {
  private readonly routeUrl: string = 'ModulePermission';

  constructor(protected configService: AppSettingsService, private http: HttpClient) {
    super(configService);
  }

  get(id: number, data: any): Observable<Response<Paginated<PermissionModel>>> {
		return this.http.get<Response<Paginated<PermissionModel>>>(`${this.coreUrl}/${this.routeUrl}?ModuleId=${id}`, { params: data });
	}

  print(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
