import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { AbstractService } from './abstract.service';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { Paginated } from '../models/paginated.model';
import { RoleModel } from '../models/role.model';

@Injectable({
	providedIn: 'root',
})
export class ModuleRolesService extends AbstractService {
	private readonly routeUrl: string = 'ModuleRole';

	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	get(id: number, params: any): Observable<Response<Paginated<RoleModel>>> {
		return this.http.get<Response<Paginated<RoleModel>>>(`${this.coreUrl}/ModuleRole?ModuleId=${id}`, { params });
	}

	removeRole(id: string): Observable<any> {
		return this.http.delete(`${this.coreUrl}/${this.routeUrl}/${id}`, {});
	}

	getById(id: number): Observable<any> {
		return this.http.get<any>(`${this.coreUrl}/${this.routeUrl}/${id}`);
	}

	getEditRoleById(id: number): Observable<any> {
		return this.http.get<any>(`${this.coreUrl}/${this.routeUrl}/${id}/edit`);
	}

	addRole(params: RoleModel): Observable<RoleModel> {
		return this.http.post<RoleModel>(`${this.coreUrl}/${this.routeUrl}`, params);
	}

	editRole(params: RoleModel): Observable<RoleModel> {
		return this.http.put<RoleModel>(`${this.coreUrl}/${this.routeUrl}`, params);
	}

	selectModuleRole(id: number): Observable<Response<any>> {
		return this.http.get<any>(`${this.coreUrl}/${this.routeUrl}/${id}/select-items`);
	}
}
