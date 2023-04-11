import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignatureService extends AbstractService {

  private readonly urlRoute = 'MSignService';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

  signDocumentAttmpet(id): Observable<any>{
    return this.http.get(`${this.baseUrl}/${this.urlRoute}/SignDocument/${id}`);
  }

  redirectMSign(requestId, url): string{
    return `${this.baseUrl}/MSignRedirect/${requestId}?returnUrl=${url}`;
  }
}
