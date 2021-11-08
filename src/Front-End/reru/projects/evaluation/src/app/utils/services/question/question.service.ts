import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';

import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { PaginatedListModel } from '../../models/paginated-list.model';
import { QuestionUnit } from '../../models/question-units/question-unit.model';

export class PaginatedResponse {
	page?: number;
	itemsPerPage?: number;
	orderField?: string;
	fields?: string;
	searchBy?: string;
}

@Injectable({
	providedIn: 'root',
})
export class QuestionService extends AbstractService {
	private readonly urlRoute = 'QuestionUnit';
	public uploadQuestions: BehaviorSubject<void> = new BehaviorSubject(null);

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	// getList(question?: string, id?: number, pagination?: PaginatedResponse) {
	// 	return this.client.get<PaginatedListModel<QuestionUnit>>(
	// 		`${this.baseUrl}/${this.urlRoute}/list?Input.Filter=${question}&Input.QuestionCategoryId=${id}&Page=${
	// 			pagination?.page ?? ''
	// 		}&ItemsPerPage=${pagination?.itemsPerPage ?? ''}&OrderField=${pagination?.orderField ?? ''}
	// 			&Fields=${pagination?.fields ?? ''}&SearchBy=${pagination?.searchBy ?? ''}`
	// 	);
	// }

	getAll(params): Observable<any> {
		return this.client.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	getTemplate(questionType: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/excel-template/${questionType}`, {
			responseType: 'blob' as 'json',
			observe: 'response' as 'body',
		});
	}

	delete(id: number): Observable<QuestionUnit> {
		return this.client.delete<QuestionUnit>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	create(data): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	edit(data): Observable<any> {
		return this.client.patch<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	editStatus(data): Observable<any> {
		return this.client.patch<any>(`${this.baseUrl}/${this.urlRoute}/edit-status`, data);
	}

	bulkUpload(file): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}/excel-template/upload`, file, {
			responseType: 'blob' as 'json',
			observe: 'response' as 'body',
		});
	}

	getTags(params): Observable<any> {
		return this.client.get(`${this.baseUrl}/${this.urlRoute}/tags`, { params });
	}
}
