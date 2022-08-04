import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudyModel } from '../models/study.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';


@Injectable({
	providedIn: 'root',
})
export class StudyService extends AbstractService {
	private readonly urlRoute: string = 'Study';
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(data): Observable<Response<StudyModel[]>> {
		return this.http.get<Response<StudyModel[]>>(`${this.coreUrl}/${this.urlRoute}`, { params: data });
	}

	add(data): Observable<Response<number>> {
		return this.http.post<Response<number>>(`${this.coreUrl}/${this.urlRoute}`, data);
	}

	addMultiple(data): Observable<Response<number>> {
		return this.http.put<Response<number>>(`${this.coreUrl}/${this.urlRoute}/bulk-import`, data);
	}

	update(data: StudyModel): Observable<Response<any>> {
		return this.http.patch<Response<any>>(`${this.coreUrl}/${this.urlRoute}`, data);
	}

	delete(id: number): Observable<Response<any>> {
		return this.http.delete<Response<any>>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	list(data: any): Observable<Response<any>> {
		return this.http.get<Response<any>>(`${this.coreUrl}/${this.urlRoute}`, { params: data });
	}
}
