import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';

@Injectable({
  providedIn: 'root'
})
export class ContactService extends AbstractService {
  private readonly routeUrl: string = 'Contact';
  constructor(protected appSettings: AppSettingsService,
    private http: HttpClient) {
    super(appSettings);
  }

  retrieveContact(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }

  createContact(contact): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.routeUrl}`, { data: contact });
  }

  updateContact(contact): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${this.routeUrl}`, { data: contact });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.routeUrl}/${id}`);
  }
}
