import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { QuestionService } from  '../../../utils/services/question/question.service'
import { SelectItem } from '../../../utils/models/select-item.model';
import { QuestionByCategoryService } from '../../../utils/services/question-by-category/question-by-category.service';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { QuestionUnit } from '../../../utils/models/question-units/question-unit.model';


@Component({
  selector: 'app-add-edit-question',
  templateUrl: './add-edit-question.component.html',
  styleUrls: ['./add-edit-question.component.scss']
})
export class AddEditQuestionComponent implements OnInit {
  questionForm: FormGroup;
  questionUnitId: number;

  types: SelectItem[] = [{ label: '', value: '' }];
  categories: SelectItem[] = [{ label: '', value: '' }];
  category = 0;
  value = false;
  isLoading: boolean = true;
  items = [];
  placeHolderString = '+ Tag'
  tags = [];

  constructor(
    private questionService: QuestionService,
    private activatedRoute: ActivatedRoute,
    private referenceService: ReferenceService,
    private location: Location,
    private questionByCategory: QuestionByCategoryService,
    private formBuilder: FormBuilder,
		private notificationService: NotificationsService
  ) {  }

  ngOnInit(): void {
    this.questionForm = new FormGroup({
			questionCategoryId: new FormControl(),
			question: new FormControl(),
			questionType: new FormControl(),
      questionPoints: new FormControl()
		});

    this.questionByCategory.value.subscribe(x => this.value = x);
    this.initData();
    this.getQuestionTypeValueForDropdown();
    this.getQuestionCategories();
  }

  initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.questionUnitId = response.id;
				this.questionService.get(this.questionUnitId).subscribe(res => {
					this.initForm(res.data);
          this.tags = res.data.tags;
				})
			}
			else
				this.initForm();
		})
	}

	hasErrors(field): boolean {
		return this.questionForm.touched && this.questionForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.questionForm.get(field).touched && this.questionForm.get(field).hasError(error)
		);
	}

	initForm(data?: any): void {
		if (data){
			this.questionForm = this.formBuilder.group({
				id: this.questionUnitId,
				questionCategoryId: this.formBuilder.control((data && !isNaN(data.categoryId) ? data.categoryId : null), [Validators.required]),
				question: this.formBuilder.control((data && data.question) || null, [Validators.required]),
        questionPoints: this.formBuilder.control((data && data.questionPoints) || null, [Validators.required]),
				questionType: this.formBuilder.control((data && !isNaN(data.questionType) ? data.questionType : null), [Validators.required]),
				status: QuestionUnitStatusEnum.Draft
			});
		}
		else {
      if(this.value) this.questionByCategory.selected.subscribe(x => this.category = x);
      
			this.questionForm = this.formBuilder.group({
        questionCategoryId: this.formBuilder.control((this.category), [Validators.required]),
				question: this.formBuilder.control(null, [Validators.required]),
        questionPoints: this.formBuilder.control(1, [Validators.required]),
				questionType: this.formBuilder.control(0, [Validators.required]),
				status: QuestionUnitStatusEnum.Draft
			});
		}
    
    this.isLoading = false;
	}

  getQuestionTypeValueForDropdown() {
    this.referenceService.getQuestionTypeStatuses().subscribe((res) => this.types = res.data);
  }

  getQuestionCategories() {
    this.referenceService.getQuestionCategory().subscribe((res) => this.categories = res.data);
  }

  addQuestion() {
    let params = {
        ...this.questionForm.value,
        tags: this.tags
    };
    this.questionService.create({data: params}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Question was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  onTextChange(text: string) {
    if(text.length) {
      this.questionService.getTags({Keyword: text}).subscribe(res => {
        this.items = res.data.map(el => el.name);
      })
    }
  };

  editQuestion() {
    let params = {
      ...this.questionForm.value,
      tags: this.tags
    }
    this.questionService.edit({data: params}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Question was successfully updated', NotificationUtil.getDefaultMidConfig());
    });
  }

  onSave(): void {
    if (this.questionUnitId) {
      this.editQuestion();
    } else {
      this.addQuestion();
    }
  }

  backClicked() {
    this.location.back();
  }
}
