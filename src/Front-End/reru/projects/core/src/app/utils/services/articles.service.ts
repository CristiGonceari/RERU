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
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	getList(params): Observable<any> {
		return this.client.get<any>(`${this.coreUrl}/${this.urlRoute}`, { params });
	}

	delete(id: number): Observable<ArticleModel> {
		return this.client.delete<ArticleModel>(`${this.coreUrl}/${this.urlRoute}/${id}`);
	}

	create(data: ArticleModel): Observable<ArticleModel> {
		return this.client.post<ArticleModel>(`${this.coreUrl}/${this.urlRoute}`, data);
	}

	print(data): Observable<any> {
		return this.client.put(`${this.coreUrl}/${this.urlRoute}/print`, data, {
			responseType: 'blob',
			observe: 'response',
		});
	}
}
