import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ContentOrganigramModel, HeadOrganigramModel, OrganigramUserModel } from '../models/organigram.model';
import { Response } from '../models/response.model';
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

  getHead(): Observable<Response<HeadOrganigramModel>> {
    return this.http.get<Response<HeadOrganigramModel>>(`${this.coreUrl}/${this.routeUrl}/organigram-head`);
  }

  getContent(params): Observable<Response<ContentOrganigramModel[]>> {
    return this.http.get<Response<ContentOrganigramModel[]>>(`${this.coreUrl}/${this.routeUrl}/organigram-content`, { params });
  }

  getUsers(params): Observable<Response<OrganigramUserModel[]>> {
    return this.http.get<Response<OrganigramUserModel[]>>(`${this.coreUrl}/${this.routeUrl}/organigram-users`, { params });
  }
}
