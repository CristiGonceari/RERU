import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
	providedIn: 'root',
})
export class TestQuestionService extends AbstractService {
	private disabled: BehaviorSubject<boolean> = new BehaviorSubject(null);
	isDisabled = this.disabled.asObservable();

	answerSubject = new BehaviorSubject<any>(null);

	private readonly urlRoute = 'TestQuestion';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

	setData = (value: boolean) => {
		this.disabled.next(value);
	};

	generate(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/generate/${id}`);
	}

	getTestQuestion(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	getTestQuestions(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/multiple-per-page`, { params });
	}

	postTestQuestions(data): Observable<any> {
		return this.http.post<any>(`${this.baseUrl}/${this.urlRoute}`, {data});
	}

	summary(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/summary/${id}`);
	}
}
