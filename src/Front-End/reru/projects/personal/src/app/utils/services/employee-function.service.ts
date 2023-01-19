import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { EmployeeFunctionModel } from '../models/employee-function.model';
import { AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class EmployeeFunctionService extends AbstractService {

	private urlRoute: string = 'EmployeeFunction';

  constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(id: number): Observable<ApiResponse<EmployeeFunctionModel>> {
		return this.http.get<ApiResponse<EmployeeFunctionModel>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

  list(data: any): Observable<ApiResponse<any>> {
		return this.http.get<ApiResponse<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	bulkImport(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/excel-import`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
