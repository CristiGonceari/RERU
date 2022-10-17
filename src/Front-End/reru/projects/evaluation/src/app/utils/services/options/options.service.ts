import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { OptionModel } from '../../models/options/option.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class OptionsService extends AbstractService {
	private readonly urlRoute = 'Option';
	public uploadOption: BehaviorSubject<void> = new BehaviorSubject(null);

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	getAll(questionUnitId?: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/?QuestionUnitId=${questionUnitId}`);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	delete(id: number): Observable<OptionModel> {
		return this.client.delete<OptionModel>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	create(data): Observable<any> {
		return this.client.post<OptionModel>(`${this.baseUrl}/${this.urlRoute}`, data, { 
			reportProgress: true,
			observe: 'events',
			responseType: 'blob' as 'json'
		});
	}

	edit(data): Observable<any> {
		return this.client.patch<OptionModel>(`${this.baseUrl}/${this.urlRoute}`, data, { 
			reportProgress: true,
			observe: 'events',
			responseType: 'blob' as 'json'
		});
	}

	deleteAllOptions(questionId: number): Observable<OptionModel> {
		return this.client.delete<OptionModel>(`${this.baseUrl}/${this.urlRoute}/${questionId}`);
	}

	getTemplate(questionType: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/excel-template/${questionType}`, {
			responseType: 'blob' as 'json',
			observe: 'response' as 'body',
		});
	}
	bulkUpload(file): Observable<any> {
		
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}/excel-template/upload`,file, {
			responseType: 'blob' as 'json',
			observe: 'response' as 'body',
		});
	}
}
