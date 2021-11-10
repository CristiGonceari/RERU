import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { QuestionCategory } from '../../../../utils/models/question-category/question-category.model';
import { QuestionCategoryService } from '../../../../utils/services/question-category/question-category.service';
import { QuestionByCategoryService } from '../../../../utils/services/question-by-category/question-by-category.service';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { BulkImportQuestionsComponent } from '../../../questions/bulk-import-questions/bulk-import-questions.component';

@Component({
  selector: 'app-category-list-table',
  templateUrl: './category-list-table.component.html',
  styleUrls: ['./category-list-table.component.scss']
})
export class CategoryListTableComponent implements OnInit {

  	questionCategories: QuestionCategory[] = [];
  	pagination: PaginationModel = new PaginationModel();
	name = '';
	pager: number[] = [];
	keyword: string;
	isLoading: boolean = true;

  constructor(
   		private questionCategoryService: QuestionCategoryService,
		private modalService: NgbModal,
		private router: Router,
		private questionByCategory: QuestionByCategoryService,
		private route: ActivatedRoute,
		private notificationService: NotificationsService
  ) { }

  	ngOnInit(): void {
		this.list();
 	}

  	list(data: any = {}) {
		console.warn('list')
		this.isLoading = true;
		this.keyword = data.keyword;
		let params = {
			name: this.keyword || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}

		this.questionCategoryService.getCategories(params).subscribe(
			res => {
				if (res && res.data) {
					this.questionCategories = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
				}
			}
		)
	}

	addQuestion(categoryId): void {
		this.questionByCategory.setData(categoryId);
		this.questionByCategory.setValue(true);
		this.router.navigate(['../questions/add-question']);
	}

	bulkImport(): void {
		const modalRef = this.modalService.open(BulkImportQuestionsComponent, { centered: true, size: 'lg' });
		modalRef.result.then(
			() => this.list(),
			() => { }
		);
	}

	navigate(id){
		this.router.navigate(['question-category/', id, 'overview'], {relativeTo: this.route});
	}

	deleteCategory(id): void{
		this.questionCategoryService.delete(id).subscribe(() => 
		{
			this.notificationService.success('Success', 'Category was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this category? All questions from this category will be deleted';
		modalRef.result.then(() => this.deleteCategory(id), () => { });
	}
}
