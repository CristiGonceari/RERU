import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ArticleModel } from '../models/article.model';

@Injectable({
	providedIn: 'root',
})
export class ArticlesService extends AbstractService {
	private readonly urlRoute = 'Article';

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	getList(params): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}`, { params });
	}

	delete(id: number): Observable<ArticleModel> {
		return this.client.delete<ArticleModel>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	create(data: any): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	edit(data: any): Observable<any> {
		return this.client.patch<any>(`${this.baseUrl}/${this.urlRoute}`, data);
	}

	print(data): Observable<any> {
		return this.client.put(`${this.baseUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
