import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { Test } from '../../models/tests/test.model';
import { VerificationTestQuestionSummary } from '../../models/verification-test/verification-test-question-summary.model';

@Injectable({
	providedIn: 'root'
})
export class TestVerificationProcessService extends AbstractService {

	private readonly urlRoute = 'TestVerification';
	constructor(protected appConfigService: AppSettingsService, public client: HttpClient) {
		super(appConfigService);
	}

	getSummary(testId): Observable<any> {
		return this.client.get<VerificationTestQuestionSummary>(`${this.baseUrl}/${this.urlRoute}/summary/${testId}`);
	}

	getTest(data): Observable<any> {
		return this.client.get<Test>(
			`${this.baseUrl}/${this.urlRoute}?TestId=${data.testId}&QuestionIndex=${data.questionIndex}&ToEvaluate=${data.toEvaluate}`
		);
	}

	getEvaluationQuestion(params): Observable<any> {
		return this.client.get<Test>(`${this.baseUrl}/${this.urlRoute}/evaluation`, {params});
	}

	verify(data): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}`, {data});
	}

	setTestAsVerified(testId): Observable<Test> {
		return this.client.post<Test>(`${this.baseUrl}/${this.urlRoute}/${testId}/verified`, testId);
	}

	getProgress(testId): Observable<Test> {
		return this.client.get<Test>(`${this.baseUrl}/${this.urlRoute}/${testId}/progress`);
	}
}
