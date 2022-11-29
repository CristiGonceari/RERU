import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { EvaluationIntroModel } from '../models/evaluation-setup.model';

@Injectable({
  providedIn: 'root'
})
export class EvaluationService extends AbstractService {
  private readonly routeUrl: string = 'Evaluation';
  constructor(protected readonly configService: AppSettingsService, 
              private readonly http: HttpClient) {
    super(configService);
   }

   get(id: number): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}`); 
   }

   create(data: EvaluationIntroModel): Observable<any> {
     return this.http.post(`${this.baseUrl}/${this.routeUrl}`, data);
   }

   delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }

   list(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}`, { params: data });
   }

   listMine(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/mine`, { params: data });
   }

   listEvaluation(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/evaluate`, { params: data });
   }

   listCountersign(data): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/counter-sign`, { params: data });
   }

   retrieveEvaluate(id: number): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}/evaluate`);
   }

   evaluate(id: number, data): Observable<any> {
     return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}/evaluate`, data);
   }

   accept(id: number, data): Observable<any> {
     return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}/accept`, data);
   }

   retrieveCounterSign(id: number): Observable<any> {
     return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}/countersign`);
   }

   counterSign(id: number, data): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.routeUrl}/${id}/counter-sign`, data);
  }

  download(id: number) {
    return this.http.get(`${this.baseUrl}/export/${id}`, { responseType: 'blob' as 'json', observe: 'response'}).subscribe((response: HttpResponse<Blob>) => {
      if (navigator.msSaveOrOpenBlob) {
        return navigator.msSaveOrOpenBlob(response.body);
      } else {
        const url = window.URL.createObjectURL(response.body);
        let a = document.createElement('a');
        document.body.appendChild(a);
        a.setAttribute('style', 'display: none');
        a.href = url;
        a.click();
        window.URL.revokeObjectURL(url);
        a.remove();
      }
    });
  }
}
