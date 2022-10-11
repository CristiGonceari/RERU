import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
@Injectable({
	providedIn: 'root'
})
export class CandidatePositionNotificationService extends AbstractService {
	private readonly urlRoute = 'CandidatePositionNotification';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	getUserIds(candidatePositionId: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${candidatePositionId}`);
	}
}
