import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { CandidatePositionModel } from '../models/candidate-position.model';

@Injectable({
  providedIn: 'root'
})
export class CandidatePositionService extends AbstractService{

  private readonly urlRoute = 'CandidatePosition';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	getList(params): Observable<any> {
		return this.client.get(`${this.coreUrl}/${this.urlRoute}`, {params});
	}

  	getPositionValues(): Observable<any> {
    	return this.client.get(`${this.coreUrl}/${this.urlRoute}/select-values`);
  	}

	delete(id: number): Observable<CandidatePositionModel> {
		return this.client.delete<CandidatePositionModel>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	create(data: CandidatePositionModel): Observable<CandidatePositionModel> {
		return this.client.post<CandidatePositionModel>(`${this.coreUrl}/${this.urlRoute}`, {data});
	}

  	editPosition(data: CandidatePositionModel): Observable<CandidatePositionModel> {
		return this.client.patch<CandidatePositionModel>(`${this.coreUrl}/${this.urlRoute}`, {data});
	}
}
