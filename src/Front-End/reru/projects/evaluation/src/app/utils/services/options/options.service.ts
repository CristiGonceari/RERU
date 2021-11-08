import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { OptionModel } from '../../models/option.model';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class OptionsService extends AbstractService {
	private readonly urlRoute = 'Options';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	getAll(questionUnitId?: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/list?QuestionUnitId=${questionUnitId}`);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	delete(id: number): Observable<OptionModel> {
		return this.client.delete<OptionModel>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	create(data: OptionModel): Observable<OptionModel> {
		return this.client.post<OptionModel>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	edit(data: OptionModel): Observable<OptionModel> {
		return this.client.patch<OptionModel>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	deleteAllOptions(questionId: number): Observable<OptionModel> {
		return this.client.delete<OptionModel>(`${this.baseUrl}/${this.urlRoute}/${questionId}`);
	}
}
