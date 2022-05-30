import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DepartmentModel } from '../models/department.model';

@Injectable({
	providedIn: 'root'
})
export class DepartmentService extends AbstractService {
	private readonly urlRoute = 'Department';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	getList(params): Observable<any> {
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}`, { params });
	}

	delete(id: number): Observable<DepartmentModel> {
		return this.client.delete<DepartmentModel>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	create(data: DepartmentModel): Observable<DepartmentModel> {
		return this.client.post<DepartmentModel>(`${this.coreUrl}/${this.urlRoute}`, data);
	}

	edit(data: DepartmentModel): Observable<DepartmentModel> {
		return this.client.patch<DepartmentModel>(`${this.coreUrl}/${this.urlRoute}`, data);
	}

	getValues(): Observable<any> {
		return this.client.get(`${this.coreUrl}/${this.urlRoute}/select-values`);
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
