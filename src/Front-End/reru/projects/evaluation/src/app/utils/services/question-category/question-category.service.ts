import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { BehaviorSubject, Observable } from 'rxjs';
import { QuestionCategory } from '../../models/question-category/question-category.model';


export class PaginatedResponse {
	page?: number;
	itemsPerPage?: number;
	orderField?: string;
	fields?: string;
	searchBy?: string;
}

@Injectable({
  providedIn: 'root'
})
export class QuestionCategoryService extends AbstractService{

	private readonly urlRoute = 'QuestionCategory';
  private categoryId: BehaviorSubject<any> = new BehaviorSubject(null);
	category = this.categoryId.asObservable();
  
  constructor(protected appSettingsService: AppSettingsService, private client: HttpClient) {
		super(appSettingsService);
	}

	setData = (value: any) => {this.categoryId.next(value)}

  get(id: number): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/${id}`);
	}

  getList(name): Observable<any> {
    return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/list-non-paginated`, name);
  }

  getCategories(params): Observable<any> {
  return this.client.get(`${this.baseUrl}/${this.urlRoute}`, { params });
  }

  delete(id: number): Observable<QuestionCategory> {
  return this.client.delete<QuestionCategory>(`${this.baseUrl}/${this.urlRoute}/${id}`);
  }

  create(data: QuestionCategory): Observable<any> {
  return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}/create`, data);
  }

  edit(data: QuestionCategory): Observable<QuestionCategory> {
  return this.client.post<QuestionCategory>(`${this.baseUrl}/${this.urlRoute}/edit`, data);
  }
}
