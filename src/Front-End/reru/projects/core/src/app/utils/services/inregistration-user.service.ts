import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InregistrationUserService  extends AbstractService {
  private readonly routeUrl = 'InregistrateUser';
	
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) { 
		super(appSettingsService);
	}

  	inregistrateUser(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}`, data);
	}

	getPersonalFile(): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/personal-file`, { responseType: 'blob', observe: 'response' });
	}
}
