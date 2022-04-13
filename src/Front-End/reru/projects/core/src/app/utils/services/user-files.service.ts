import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { UserFilesModel } from '../models/user-files.model';

@Injectable({
  providedIn: 'root'
})
export class UserFilesService extends AbstractService{

  private readonly urlRoute = 'UserFiles';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(fileId: string): Observable<any> {
		return this.client.get(`${this.coreUrl}/${this.urlRoute}/${fileId}`, { responseType: 'blob', observe: 'response' });
	}

	getList(params): Observable<any> {
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}/files`, { params });
	}

	delete(fileId: string): Observable<UserFilesModel> {
		return this.client.delete<UserFilesModel>(`${this.coreUrl}/${this.urlRoute}/${fileId}`);
	}

	create(data): Observable<any> {
		return this.client.post<any>(`${this.coreUrl}/${this.urlRoute}`, data);
	}
}
