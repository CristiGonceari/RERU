import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class StatisticService extends AbstractService {
  private readonly urlRoute = 'Statistic';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getTestTemplateQuestions(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/questions-test-type`, { params });
  }

  getCategoryQuestions(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/questions-category`, { params });
  }

  printByTestTemplate(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-test-question-statistic`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}

  printByCategory(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print-category-question-statistic`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
  
}
