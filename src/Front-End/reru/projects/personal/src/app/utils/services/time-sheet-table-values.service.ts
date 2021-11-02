import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { AddEditTimeSheetTableModel } from '../models/timesheet-data.model';


@Injectable({
	providedIn: 'root'
})
export class TimeSheetTableValuesService extends AbstractService {
	private readonly routeUrl: string = 'TimeSheetTable';
	private tableId = new BehaviorSubject(null);
	timeSheetTableValueId = this.tableId.asObservable();
	
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	sendData(id) {
		this.tableId.next(id);
	}

	list(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
	}

	addEditTimeSheetTable(contractorId): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}`, contractorId);
	}

	print(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/export-excel`, {
			params: data,
			responseType: 'blob' as 'json' ,
			observe: 'response',
		});
	}

	printAll(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/all-export-excel`, {
			params: data,
			responseType: 'blob' as 'json' ,
			observe: 'response',
		});
	}

	deleteTableValues(data): Observable<any>
	{
		return this.http.delete(`${this.baseUrl}/${this.routeUrl}/values`,{params: data})
	}
}