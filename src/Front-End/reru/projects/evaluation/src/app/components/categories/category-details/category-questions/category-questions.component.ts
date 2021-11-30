import { Component, Input, OnInit } from '@angular/core';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionCategoryService } from 'projects/evaluation/src/app/utils/services/question-category/question-category.service';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { QuestionUnitStatusEnum } from '../../../../utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { QuestionUnit } from '../../../../utils/models/question-units/question-unit.model';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BulkImportQuestionsComponent } from '../../../questions/bulk-import-questions/bulk-import-questions.component';
import { Router } from '@angular/router';
import { QuestionByCategoryService } from 'projects/evaluation/src/app/utils/services/question-by-category/question-by-category.service';

@Component({
  selector: 'app-category-questions',
  templateUrl: './category-questions.component.html',
  styleUrls: ['./category-questions.component.scss']
})
export class CategoryQuestionsComponent implements OnInit {

  @Input() categoryId;
  categoryList = [];
	questionList: QuestionUnit[] = [];
	pagedSummary: PaginationModel = new PaginationModel();
	questionEnum = QuestionUnitStatusEnum;
  type = QuestionUnitTypeEnum;
  isLoading: boolean = true;
  
  constructor(
    private questionService: QuestionService,
    private categoryService: QuestionCategoryService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
    public router: Router,
    private questionByCategory: QuestionByCategoryService,
  ) { }

  ngOnInit(): void {
    this.categoryService.category.subscribe(x => this.categoryId = x);
    this.getAll();
  }

  getAll(data: any = {}){
    let params = {
      questionCategoryId: this.categoryId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.questionService.getAll(params).subscribe((res) => {
      if (res && res.data) {
        this.questionList = res.data.items;
        this.pagedSummary = res.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  changeStatus(id, status) {
		let params;
		if (status == QuestionUnitStatusEnum.Active) 
			params = { data: { questionId: id, status: QuestionUnitStatusEnum.Inactive } }
		else
			params = { data: { questionId: id, status: QuestionUnitStatusEnum.Active } }

      this.questionService.editStatus(params).subscribe(
			res => {
				this.getAll();
				this.notificationService.success('Success', 'Question status was updated', NotificationUtil.getDefaultMidConfig());
			}
		)
	}

  bulkImport(): void { 
		const modalRef = this.modalService.open(BulkImportQuestionsComponent, { centered: true, size: 'lg' });
		modalRef.result.then(
			() => { }
		);
	}

	addQuestion(): void {
		this.questionByCategory.setData(this.categoryId);
		this.questionByCategory.setValue(true);
		this.router.navigate(['../questions/add-question']);
	}

  deleteQuestion(id): void {
		this.questionService.delete(id).subscribe(() => {
			this.notificationService.success('Success', 'Question was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.getAll();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this question?';
		modalRef.result.then(() => this.deleteQuestion(id), () => { });
	}
}
