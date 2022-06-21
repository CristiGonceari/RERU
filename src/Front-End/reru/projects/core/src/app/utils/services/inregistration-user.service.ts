import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InregistrationUserService  extends AbstractService {
  private readonly routeUrl = 'InregistrateUser';
  private transport = new BehaviorSubject('');
  public code = this.transport.asObservable();
	
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) { 
		super(appSettingsService);
	}

	shareCode(value: any) {
		this.transport.next(value)
	}

  	inregistrateUser(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}`, data);
	}

	verifyEmail(data): Observable<any> {
		return this.http.post(`${this.coreUrl}/${this.routeUrl}/verify-code`, data);
	}

	getPersonalFile(): Observable<any> {
		return this.http.get(`${this.coreUrl}/${this.routeUrl}/personal-file`, { responseType: 'blob', observe: 'response' });
	}
}
