import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService } from './abstract.service';
import { AppSettingsService } from './app-settings.service';
import saveAs from 'file-saver';

@Injectable({
  providedIn: 'root'
})
export class CloudFileService extends AbstractService {

  private readonly urlRoute = 'CloudFile';
	constructor(protected appConfigService: AppSettingsService, public http: HttpClient) {
		super(appConfigService);
	}

  get(id): Observable<HttpEvent<Blob>> {
	  if(this.baseUrl.includes("reru-evaluation"))
	  {
			console.warn('evaluation url', this.baseUrl)
		  return this.http.get(`${this.baseUrl}/${this.urlRoute}/${id}`, { 
			  reportProgress: true,
			  observe: 'events',
			  responseType: 'blob'
		  });
	  } 
	  else {
		  console.warn('core url', this.coreUrl)
		return this.http.get(`${this.coreUrl}/${this.urlRoute}/${id}`, { 
			reportProgress: true,
			observe: 'events',
			responseType: 'blob'
		});
	  }
	}

  download(id: string): void {
		this.http
			.get(`${this.baseUrl}/${this.urlRoute}/${id}`, { responseType: 'blob', observe: 'response' })
			.subscribe(response => {
				let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
				if (response.body.type === 'application/pdf') {
					fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
				}
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			});
	}

  create(data): Observable<any> {
		return this.http.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

  list(): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.urlRoute}/allFiles`);
	  }


  delete(id): Observable<any> {
		return this.http.delete<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}
}
