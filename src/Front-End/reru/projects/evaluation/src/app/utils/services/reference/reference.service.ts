import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class ReferenceService extends AbstractService {

  private readonly urlRoute = 'Reference';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getTestTemplateStatuses(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-type-statuses/select-values`);
  }

  getTestTemplates(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-template/select-values`);
  }

  getQuestionType(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-types-value/select-values`);
  }

  getRequiredDocumentSelectValues(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/required-document/select-values`, { params });
  }

  getQuestionCategory(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-categories-value/select-values`);
  }

  getProcesses(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/processes-value/select-values`);
  }

  getUsers(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/users-value/select-values`, { params });
  }

  getEventLocations(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/event-locations/select-values`, { params });
  }

  getStatisticEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/statistics/select-values`);
  }

  getTestStatuses(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-statuses/select-values`);
  }

  getTestResults(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-results/select-values`);
  }

  getLocationType(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/locations/select-values`);
  }

  getEvents(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/events-value/select-values`);
  }

  getMode(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-type-mode/select-values`);
  }

  getTestTemplateEvaluator(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-type-with-evaluator-value/select-values`);
  }

  getEventEvaluator(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/events-with-evaluator-value/select-values`);
  }

  getQuestionStatus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-status/select-values`);
  }

  getScoreFormula(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/score-formula/select-values`);
  }

  getSolicitedTestStatus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/solicited-test-status/select-values`);
  }

  getDocumentTemplateType(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/document-template-type/select-values`);
  }
}
