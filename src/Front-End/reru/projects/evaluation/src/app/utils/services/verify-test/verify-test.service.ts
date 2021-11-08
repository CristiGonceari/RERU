import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VerificationSummaryModel } from '../../models/verification-summary.model';
import { TestModel } from '../../models/test.model';

@Injectable({
	providedIn: 'root',
})
export class VerifyTestService extends AbstractService {
	private readonly urlRoute = 'VerifyTest';
	constructor(protected appConfigService: AppSettingsService, public client: HttpClient) {
		super(appConfigService);
	}

	getSummary(testId): Observable<any> {
		return this.client.get<VerificationSummaryModel>(`${this.baseUrl}/${this.urlRoute}/summary/${testId}`);
	}

	verify(data): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getTest(data): Observable<any> {
		return this.client.get<TestModel>(
			`${this.baseUrl}/${this.urlRoute}?TestId=${data.testId}&QuestionIndex=${data.questionIndex}`
		);
	}

	setVerified(testId): Observable<TestModel> {
		return this.client.post<TestModel>(`${this.baseUrl}/${this.urlRoute}/${testId}/verified`, testId);
	}

	getProgress(testId): Observable<TestModel> {
		return this.client.get<TestModel>(`${this.baseUrl}/${this.urlRoute}/${testId}/progress`);
	}
}
