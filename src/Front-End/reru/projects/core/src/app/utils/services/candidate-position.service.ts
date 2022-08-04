import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CandidatePositionService extends AbstractService{
  private readonly urlRoute = 'CandidatePosition';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	getList(params): Observable<any> {
		return this.client.get(`${this.coreUrl}/${this.urlRoute}`, { params });
	}
}
