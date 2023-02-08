import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettingsService, AbstractService } from '@erp/shared';


@Injectable({
	providedIn: 'root'
})
export class MyProfileService extends AbstractService {
	private readonly routeUrl: string = 'MyProfile';

	constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

	getFiles(params): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/files`, { params });
	}

	addFile(params: any): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}/file`, params );
	}

	print(data): Observable<any> {
		return this.http.put(`${this.coreUrl}/${this.routeUrl}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

}
