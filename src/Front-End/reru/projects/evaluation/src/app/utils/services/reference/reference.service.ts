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

  getTestTypeStatuses(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-type-statuses/select-values`);
  }

  getTestTypes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-template/select-values`);
  }

  getQuestionTypeStatuses(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-types-value/select-values`);
  }

  getQuestionCategory(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-categories-value/select-values`);
  }

  getUsers(params): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/users-value/select-values`, { params });
  }

  getStatisticEnum(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/statistics/select-values`);
  }

  getTestStatuses(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-statuses/select-values`);
  }

  getQuestionStatus(): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/question-status-value/select-values`);
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

  getTestTypeEvaluator(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/test-type-with-evaluator-value/select-values`);
  }

  getEventEvaluator(): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/events-with-evaluator-value/select-values`);
  }
}
