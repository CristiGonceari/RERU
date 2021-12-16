import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrintTemplateService extends AbstractService {
  private readonly urlRoute = 'TestPrintHtmlPage';

  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) {
		super(appConfigService);
	}

  getTestPdf(testId): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-pdf/${testId}`, { responseType: 'blob', observe: 'response' });
  }

  getPerformingTestPdf(testId): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/performing-test-pdf/${testId}`, { responseType: 'blob', observe: 'response' });
  }

  getTestTemplatePdf(testTemplateId): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/print-test-type-${testTemplateId}`, { responseType: 'blob', observe: 'response' });
  }

  getQuestionUnitPdf(questionId): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-pdf/${questionId}`, { responseType: 'blob', observe: 'response' });
  }
}
