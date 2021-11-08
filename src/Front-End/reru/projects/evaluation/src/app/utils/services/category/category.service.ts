import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { QuestionCategoryModel } from '../../models/question-category.model';
import { PaginatedListModel } from '../../models/paginated-list.model';

export class PaginatedResponse {
	page?: number;
	itemsPerPage?: number;
	orderField?: string;
	fields?: string;
	searchBy?: string;
}

@Injectable({
	providedIn: 'root',
})
export class CategoryService extends AbstractService {
	private readonly urlRoute = 'QuestionCategory';
	private categoryId: BehaviorSubject<any> = new BehaviorSubject(null);
	category = this.categoryId.asObservable();

	constructor(protected appConfigService: AppSettingsService, private client: HttpClient) {
		super(appConfigService);
	}

	setData = (value: any) => {this.categoryId.next(value)}

	get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

	getList(name): Observable<any> {
    	return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/list-non-paginated`, name);
	}

	getCategories(params): Observable<any> {
		return this.client.get(`${this.baseUrl}/${this.urlRoute}/list`, { params });
	}

	delete(id: number): Observable<QuestionCategoryModel> {
		return this.client.delete<QuestionCategoryModel>(`${this.baseUrl}/${this.urlRoute}/delete/${id}`);
	}

	create(data: QuestionCategoryModel): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}/create`, data);
	}

	edit(data: QuestionCategoryModel): Observable<QuestionCategoryModel> {
		return this.client.post<QuestionCategoryModel>(`${this.baseUrl}/${this.urlRoute}/edit`, data);
	}
}
