import { Component, Input, OnInit } from '@angular/core';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionCategoryService } from 'projects/evaluation/src/app/utils/services/question-category/question-category.service';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { QuestionUnitStatusEnum } from '../../../../utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { QuestionUnit } from '../../../../utils/models/question-units/question-unit.model';

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
    private categoryService: QuestionCategoryService
  ) { }

  ngOnInit(): void {
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
			params = { questionId: id, status: QuestionUnitStatusEnum.Inactive }
		else
			params = { questionId: id, status: QuestionUnitStatusEnum.Active }

		this.questionService.editStatus(params).subscribe(
			res => {
				this.getAll();
			}
		)
	}
}
