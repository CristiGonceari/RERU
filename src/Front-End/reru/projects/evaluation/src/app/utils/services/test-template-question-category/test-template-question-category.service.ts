import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class TestTemplateQuestionCategoryService extends AbstractService {
	private readonly urlRoute = 'TestTemplateQuestionCategory';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

	assignQuestionCategoryToTestTemplate(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getQuestionCategoryByTestTemplateId(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	deleteTestTemplateQuestionCategory(params): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	categorySequenceType(params): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/sequence-type`, params);
	}

	add(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, { data });
	}

	preview(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/preview`, { data });
	}

	setSequence(input): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/set-sequence`, input);
	}
	
}
