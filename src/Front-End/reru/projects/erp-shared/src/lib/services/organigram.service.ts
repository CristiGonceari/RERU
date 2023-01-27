import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ContentOrganigramModel, HeadOrganigramModel, OrganigramUserModel } from '../models/organigram.model';
import { AbstractService } from './abstract.service';
import { AppSettingsService } from './app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class OrganigramService extends AbstractService {
  private readonly routeUrl = 'Organigram';
	constructor(protected readonly configService: AppSettingsService, 
				      private readonly http: HttpClient) {
		super(configService);
	}

  getHead(): Observable<any> {
    return this.http.get<any>(`http://localhost:8182/Api/${this.routeUrl}/organigram-head`);
  }

  getContent(params): Observable<any> {
    return this.http.get<any>(`http://localhost:8182/Api/${this.routeUrl}/organigram-content`, { params });
  }

  getUsers(params): Observable<any> {
    return this.http.get<any>(`http://localhost:8182/Api/${this.routeUrl}/organigram-users`, { params });
  }

  // getHead(): Observable<HeadOrganigramModel> {
  //   return this.http.get<HeadOrganigramModel>(`${this.coreUrl}/${this.routeUrl}/organigram-head`);
  // }

  // getContent(params): Observable<ContentOrganigramModel> {
  //   return this.http.get<ContentOrganigramModel>(`${this.coreUrl}/${this.routeUrl}/organigram-content`, { params });
  // }

  // getUsers(params): Observable<OrganigramUserModel> {
  //   return this.http.get<OrganigramUserModel>(`${this.coreUrl}/${this.routeUrl}/organigram-users`, { params });
  // }
}
