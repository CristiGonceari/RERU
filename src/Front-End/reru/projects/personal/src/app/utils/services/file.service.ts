import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractService, AppSettingsService } from '@erp/shared';
import saveAs from 'file-saver';

@Injectable({
	providedIn: 'root',
})
export class FileService extends AbstractService {
	private readonly routeUrl: string = 'File';
	constructor(protected appSettingsService: AppSettingsService, private http: HttpClient) {
		super(appSettingsService);
	}

	upload(data): Observable<any> {
		return this.http.put<any>(`${this.baseUrl}/${this.routeUrl}/html-to-pdf`, data, {
			responseType: 'blob' as 'json',
			observe: 'response',
		});
	}

	getPdfFromString(htmlContent): Observable<any> {
		return this.http.put<any>(`${this.baseUrl}/${this.routeUrl}/html-string-to-pdf`, {fileContent : htmlContent},{
			responseType: 'blob' as 'json',
			observe: 'response',
		});
	}

	get(id: number): Observable<any> {
		return this.http.get(`${this.baseUrl}/${this.routeUrl}/${id}`, { responseType: 'blob', observe: 'response' });
	}

	create(data): Observable<any> {
		console.log("data:", data)
		return this.http.post<any>(`${this.baseUrl}/${this.routeUrl}`, data);
	}

	delete(id: number): Observable<any> {
		return this.http.delete<any>(`${this.baseUrl}/${this.routeUrl}/${id}`);
	}

	download(id: number): void {
		this.http
			.get(`${this.baseUrl}/${this.routeUrl}/${id}`, { responseType: 'blob', observe: 'response' })
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

	sign(data: FormData): Observable<any> {
		return this.http.post(`${this.baseUrl}/${this.routeUrl}/sign`, data);
	}
}
