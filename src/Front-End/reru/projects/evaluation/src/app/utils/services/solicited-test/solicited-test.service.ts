import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class SolicitedTestService extends AbstractService {
  private readonly urlRoute = 'SolicitedTest';
  constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
    super(appConfigService);
  }

  getSolicitedTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

  getSolicitedTest(id): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

  changeStatus(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/status`, data);
	}

  getMySolicitedTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-solicited-tests`, { params });
	}

  getUserSolicitedTests(params): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/user-solicited-test`, { params });
	}

  getMySolicitedTest(id): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/my-solicited-test/${id}`);
	}
  
  addMySolicitedTest(data): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.urlRoute}/add-my`, data);
	}

  editMySolicitedTest(data): Observable<any> {
		return this.http.patch(`${this.baseUrl}/${this.urlRoute}/edit-my`, data);
	}

  deleteMySolicitedTest(id): Observable<any>{
    return this.http.delete(`${this.baseUrl}/${this.urlRoute}/delete-my/${id}`);
  }

  print(data): Observable<any> {
		return this.http.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
