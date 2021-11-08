import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class GlobalSettingsService extends AbstractService {
  private readonly urlRoute = 'GlobalSettings';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

  getSettings(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`);
	}

  editSettings(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}

  getUserSettingsPermission(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/show-username-permission`);
	}
}
