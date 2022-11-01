import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

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
export class TestService extends AbstractService {
	private readonly urlRoute = 'Test';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

	createTest(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/tests`, data);
	}

	createEvaluations(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/evaluations`, data);
	}

	startAddProcess(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/process`, data);
	}

	getImportProcess(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/process/${id}`);
	}

	getImportResult(fileId: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/process-result/${fileId}`, {
			responseType: 'blob' as 'json' ,
			observe: 'response',
		});
	}

	editTest(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getTest(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	getTestSettings(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/setting/${id}`);
	}

	getUserTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/mine`, { params });
	}

	getUserTestsWithoutEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-without-event`, { params });
	}

	getUserTestsWithoutEventByDate(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-without-event-by-date`, { params });
	}

	getUserTestsWithoutEventCount(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-without-event-count`, {params});
	  }

	getUserTestsByEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-by-event`, { params });
	}

	pollProgress(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/poll-result`, { params });
	}

	getUserPollsByEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-polls-by-event`, { params });
	}

	createMinePoll(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/create-my-poll`, data);
	}

	sendEmailNotification(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/send-notification`, data);
	}

	finalizeTest(testId): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/finalize`, { testId: testId });
	}

	finalizeEvaluation(testId): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/finalize-evaluation`, { testId: testId });
	}

	getTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	getEvaluations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/get-evaluations`, { params });
	}

	changeStatus(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/edit-status`, data);
	}

	setResult(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/edit-result`, data);
	}

	startTest(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/start-test`, data);
	}

	startEvaluation(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/start-evaluation`, data);
	}

	deleteTest(params): Observable<any> {
		return this.http.delete(`${this.baseUrl}/${this.urlRoute}`,  { params } );
	}

	export(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/export`, {
			responseType: 'blob' as 'json',
			observe: 'response',
		});
	}

	allow(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/allow`, data);
	}

	getUsersTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-tests`, { params });
	}

	getUsersEvaluations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-evaluations`, { params });
	}

	getUsersEvaluatedEvaluations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-received-evaluations`, { params });
	}

	getUsersTestsByEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-tests-by-event`, { params });
	}

	getUsersPolls(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-polls`, { params });
	}

	getUserEvaluatedTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-evaluated-tests`, { params });
	}

	getMyEvaluatedTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-evaluated`, { params });
	}

	getMyEvaluations(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-evaluations`, { params });
	}

	getMyEvaluatedTestsCount(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-evaluated-tests-count`, { params });
	}

	getMyEvaluatedTestsByDate(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-evaluated-by-date`, { params });
	}

	print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-tests`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printEvaluations(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-evaluations`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserEvaluations(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-evaluations`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserEvaluatedEvaluations(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-received-evaluations`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserTests(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-tests`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserPolls(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-polls`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserTestsByEvent(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-tests-by-event`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	printUserEvaluatedTests(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-user-evaluated-tests`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	getTestDocumentReplacedKeys(params): Observable<any>{
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/getTestRelacedKeys`, {params});
	}
	
	printDocument(data): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/getPDF?source=${encodeURIComponent(data.source)}&testName=${data.testName}`, {
			responseType: 'blob',
			observe: 'response',
		});
	}

	getMyTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-activities/my-tests`, { params });
	}

	getMyTestsCount(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-activities/my-tests-count`, {params});
	}

	getMyPolls(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-activities/my-polls`, { params });
	}

	getMyPollsCount(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-activities/my-polls-count`, {params});
	}
}
