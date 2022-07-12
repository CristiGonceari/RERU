import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecommendationForStudyModel } from '../models/recommendation-for-study.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class RecommendationForStudyService extends AbstractService {

  private readonly urlRoute: string = 'RecommendationForStudy';

	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(data): Observable<Response<RecommendationForStudyModel[]>> {
		return this.http.get<Response<RecommendationForStudyModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	add(data): Observable<Response<number>> {
		return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	addMultiple(data): Observable<Response<number>> {
		return this.http.put<Response<number>>(`${this.baseUrl}/${this.urlRoute}/bulk-import`, data);
	}

	update(data: RecommendationForStudyModel): Observable<Response<any>> {
		return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	delete(id: number): Observable<Response<any>> {
		return this.http.delete<Response<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	list(data: any): Observable<Response<any>> {
		return this.http.get<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}
}
