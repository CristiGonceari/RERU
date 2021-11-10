import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@erp/shared';
import { QuestionByCategoryService } from '../../../utils/services/question-by-category/question-by-category.service';
import { QuestionCategoryService } from '../../../utils/services/question-category/question-category.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { BulkImportQuestionsComponent } from '../../questions/bulk-import-questions/bulk-import-questions.component';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.scss']
})
export class CategoryDetailsComponent implements OnInit {

  categoryId: number;
  categoryName: string;
  isLoading: boolean = true;

  constructor(
    private questionCategoryService: QuestionCategoryService,
		private activatedRoute: ActivatedRoute,
    public router: Router,
    private questionByCategory: QuestionByCategoryService,
    private modalService: NgbModal,
		private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
   this.subsribeForParams();
  }

  getCategory(){
    this.questionCategoryService.get(this.categoryId).subscribe(res => {
      if (res && res.data) {
        this.categoryName = res.data.name;
        this.isLoading = false;
      }
    })
  }

  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.categoryId = params.id;
			if (this.categoryId) {
        this.getCategory();
        this.questionCategoryService.setData(this.categoryId);
    }});
	}

  addQuestion(): void {
		this.questionByCategory.setData(this.categoryId);
		this.questionByCategory.setValue(true);
		this.router.navigate(['../questions/add-question']);
	}

  deleteCategory(categoryId): void{
		this.questionCategoryService.delete(categoryId).subscribe(() => {
			this.router.navigate(['/categories']);
			this.notificationService.success('Success', 'Category was successfully deleted', NotificationUtil.getDefaultMidConfig());
		});
	}

	openConfirmationDeleteModal(): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this category? All questions from this category will be deleted';
		modalRef.result.then(() => this.deleteCategory(this.categoryId), () => { });
	}
  
  bulkImport(): void { 
		const modalRef = this.modalService.open(BulkImportQuestionsComponent, { centered: true, size: 'lg' });
		modalRef.result.then(
			() => { }
		);
	}
}
