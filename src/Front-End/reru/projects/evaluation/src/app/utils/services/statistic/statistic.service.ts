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

  getTestTypeQuestions(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/questions-test-type`, { params });
  }

  getCategoryQuestions(params): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/questions-category`, { params });
  }
}
