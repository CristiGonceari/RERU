import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MilitaryObligationModel } from '../models/military-obligation.model';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class MilitaryObligationService extends AbstractService {
  private readonly urlRoute: string = 'MilitaryObligation';
  
	constructor(private http: HttpClient, protected appSettingsService: AppSettingsService) {
		super(appSettingsService);
	}

	get(data): Observable<Response<MilitaryObligationModel[]>> {
		return this.http.get<Response<MilitaryObligationModel[]>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}

	add(data): Observable<Response<number>> {
		return this.http.post<Response<number>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	addMultiple(data): Observable<Response<number>> {
		return this.http.put<Response<number>>(`${this.baseUrl}/${this.urlRoute}/bulk-import`, data);
	}

	update(data: MilitaryObligationModel): Observable<Response<any>> {
		return this.http.patch<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	delete(id: number): Observable<Response<any>> {
		return this.http.delete<Response<any>>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	list(data: any): Observable<Response<any>> {
		return this.http.get<Response<any>>(`${this.baseUrl}/${this.urlRoute}`, { params: data });
	}
}
