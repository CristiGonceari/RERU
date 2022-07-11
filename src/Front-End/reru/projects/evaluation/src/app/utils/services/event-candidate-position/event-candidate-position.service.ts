import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventCandidatePositionService extends AbstractService {
  private readonly urlRoute = 'EventVacantPosition';
 
  constructor(protected appConfigService: AppSettingsService, private http: HttpClient) { 	
    super(appConfigService);
  }

  getEventVacandPostition(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }
  
}
