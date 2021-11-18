import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VerificationTestQuestionSummary } from '../../models/verification-test/verification-test-question-summary.model';
import { Test } from '../../models/tests/test.model';
import { TestListComponent } from '../../../components/tests/test-list/test-list.component';

@Injectable({
	providedIn: 'root',
})
export class VerifyTestService extends AbstractService {
	private readonly urlRoute = 'VerifyTest';
	constructor(protected appConfigService: AppSettingsService, public client: HttpClient) {
		super(appConfigService);
	}

	getSummary(testId): Observable<any> {
		return this.client.get<VerificationTestQuestionSummary>(`${this.baseUrl}/${this.urlRoute}/summary/${testId}`);
	}

	verify(data): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getTest(data): Observable<any> {
		return this.client.get<TestListComponent>(
			`${this.baseUrl}/${this.urlRoute}?TestId=${data.testId}&QuestionIndex=${data.questionIndex}`
		);
	}

	setVerified(testId): Observable<Test> {
		return this.client.post<Test>(`${this.baseUrl}/${this.urlRoute}/${testId}/verified`, testId);
	}

	getProgress(testId): Observable<Test> {
		return this.client.get<Test>(`${this.baseUrl}/${this.urlRoute}/${testId}/progress`);
	}
}
