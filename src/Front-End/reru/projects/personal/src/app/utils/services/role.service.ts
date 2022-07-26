import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { RoleModel } from '../models/role.model';
import { AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class RoleService extends AbstractService {
	private urlRoute: string = 'OrganizationRole';
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(id: number): Observable<ApiResponse<RoleModel>> {
		return this.http.get<ApiResponse<RoleModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	// add(data: RoleModel): Observable<ApiResponse<any>> {
	// 	return this.http.post<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
	// }

	// update(data: RoleModel): Observable<ApiResponse<any>> {
	// 	return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { data });
	// }

	// delete(id: number): Observable<ApiResponse<any>> {
	// 	return this.http.delete<ApiResponse<RoleModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	// }

	list(data: any): Observable<ApiResponse<any>> {
		return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	bulkImport(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/excel-import`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
