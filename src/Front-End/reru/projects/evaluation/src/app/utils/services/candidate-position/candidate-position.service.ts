import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { CandidatePositionModel } from '../../models/candidate-position.model';

@Injectable({
  providedIn: 'root'
})
export class CandidatePositionService extends AbstractService{

  private readonly urlRoute = 'CandidatePosition';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	getDiagram(params): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/diagram`, {params});
	}

	getList(params): Observable<any> {
		return this.client.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

  	getPositionValues(params): Observable<any> {
    	return this.client.get(`${this.baseUrl}/${this.urlRoute}/select-values`, { params });
  	}

	delete(id: number): Observable<CandidatePositionModel> {
		return this.client.delete<CandidatePositionModel>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	create(data: CandidatePositionModel): Observable<CandidatePositionModel> {
		return this.client.post<CandidatePositionModel>(`${this.baseUrl}/${this.urlRoute}`, {data});
	}

  	editPosition(data: CandidatePositionModel): Observable<CandidatePositionModel> {
		return this.client.patch<CandidatePositionModel>(`${this.baseUrl}/${this.urlRoute}`, {data});
	}

	print(data): Observable<any> {
		return this.client.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
