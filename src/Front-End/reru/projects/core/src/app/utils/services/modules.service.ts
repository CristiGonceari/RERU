import { Injectable } from '@angular/core';
// import { AbstractService } from './abstract.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Paginated } from '../models/paginated.model';
import { AdminModuleModel } from '../models/admin-module.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class ModulesService extends AbstractService {
	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	add(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/admin/module`, data);
	}

	get(id: number): Observable<Response<any>> {
		return this.http.get<Response<any>>(`${this.coreUrl}/admin/module/${id}`);
	}

	getForEdit(id: number): Observable<any> {
		return this.http.get(`${this.coreUrl}/admin/module/${id}/edit`);
	}

	edit(data: any): Observable<any> {
		return this.http.put(`${this.coreUrl}/admin/module`, data);
	}

	delete(id: number) {
		return this.http.delete(`${this.coreUrl}/admin/module/${id}`);
	}

	moduleList(params: any): Observable<Response<Paginated<AdminModuleModel>>> {
		return this.http.get<Response<Paginated<AdminModuleModel>>>(`${this.coreUrl}/admin/module`, { params });
	}

	list(): Observable<any> {
		return this.http.get(`${this.coreUrl}`);
	}

	print(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/admin/module/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
