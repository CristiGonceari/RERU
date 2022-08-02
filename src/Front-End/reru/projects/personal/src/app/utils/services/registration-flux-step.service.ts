import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response, AppSettingsService, AbstractService } from '@erp/shared';
import { Observable } from 'rxjs';
import { RegistrationFluxStep } from '../models/registrationFluxStep.model';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFluxStepService extends AbstractService {
  private readonly routeUrl: string = 'RegistrationFluxStep';

  constructor(protected configService: AppSettingsService, private http: HttpClient) {
		super(configService);
	}

  get(data): Observable<Response<any>> {
    return this.http.get<Response<any>>(`${this.baseUrl}/${this.routeUrl}`, { params: data });
  }

  add(data: RegistrationFluxStep): Observable<Response<number>> {
    return this.http.post<Response<number>>(`${this.baseUrl}/${this.routeUrl}`, data);
  }

  update(data): Observable<Response<any>> {
    return this.http.patch<Response<any>>(`${this.baseUrl}/${this.routeUrl}`, data);
  }
}
