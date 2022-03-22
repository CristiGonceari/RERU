import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuideService extends AbstractService {
	private readonly urlRoute = 'UserGuide';
	
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) { 
		super(appSettingsService);
	}
	
	get(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/ghid`, { responseType: 'blob', observe: 'response' });
	}
}

