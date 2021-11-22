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
		return this.http.post(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	editTest(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	getTest(id): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	getUserTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/mine`, { params });
	}

	getUserTestsWithoutEvent(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-tests-without-event`, { params });
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
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/create-mine-poll`, data);
	}

	finalizeTest(testId): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/finalize`, { testId: testId });
	}

	getTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	changeStatus(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/edit-status`, data);
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
}
