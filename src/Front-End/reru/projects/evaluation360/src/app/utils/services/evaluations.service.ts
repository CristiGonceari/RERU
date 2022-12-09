import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { EvaluationIntroModel } from '../models/evaluation-setup.model';
import { EvaluationModel } from '../models/evaluation.model';
import { Response, ResponseArray } from '../models/response.model';
import { EvaluationListModel } from '../models/evaluation-list.model';

@Injectable({
  providedIn: 'root'
})
export class EvaluationService extends AbstractService {
  private readonly routeUrl: string = 'Evaluation';
  constructor(protected readonly configService: AppSettingsService, 
              private readonly http: HttpClient) {
    super(configService);
   }

   get(id: number): Observable<Response<EvaluationModel>> {
     return this.http.get<Response<EvaluationModel>>(`${this.baseUrl}/${this.routeUrl}/${id}`); 
   }

   create(data: EvaluationIntroModel): Observable<Response<string>> {
     return this.http.post<Response<string>>(`${this.baseUrl}/${this.routeUrl}`, data);
   }

   update(data: EvaluationModel): Observable<Response<string>> {
    return this.http.put<Response<string>>(`${this.baseUrl}/${this.routeUrl}/update`, data)
   }

   confirm(data: EvaluationModel): Observable<Response<string>> {
    return this.http.put<Response<string>>(`${this.baseUrl}/${this.routeUrl}/confirm`, data)
   }

   accept(data: EvaluationModel): Observable<Response<string>> {
    return this.http.put<Response<string>>(`${this.baseUrl}/${this.routeUrl}/accept`, data)
   }
  
   reject(data: EvaluationModel): Observable<Response<string>> {
    return this.http.put<Response<string>>(`${this.baseUrl}/${this.routeUrl}/reject`, data)
   }

   delete(id: number): Observable<Response<string>> {
    return this.http.delete<Response<string>>(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }

   listMine(data): Observable<ResponseArray<EvaluationListModel>> {
     return this.http.get<ResponseArray<EvaluationListModel>>(`${this.baseUrl}/${this.routeUrl}/mine`, { params: data });
   }

  download(id: number) {
    return this.http.get(`${this.baseUrl}/export/${id}`, { responseType: 'blob' as 'json', observe: 'response'}).subscribe((response: HttpResponse<Blob>) => {
      if (window.navigator && (window.navigator as any).msSaveOrOpenBlob ) {
        return (window.navigator as any).msSaveOrOpenBlob(response.body);
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
