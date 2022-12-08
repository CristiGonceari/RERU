import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { SelectionTypeEnum } from 'projects/evaluation/src/app/utils/enums/selection-type.enum';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionUnit } from 'projects/evaluation/src/app/utils/models/question-units/question-unit.model';
import { QuestionUnitTypeEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-type.enum';
import { DragulaService } from 'ng2-dragula';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplateQuestionCategoryService } from 'projects/evaluation/src/app/utils/services/test-template-question-category/test-template-question-category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-category-questions-table',
  templateUrl: './category-questions-table.component.html',
  styleUrls: ['./category-questions-table.component.scss']
})
export class CategoryQuestionsTableComponent implements OnInit, OnDestroy {
  @Input() timeLimit;
  @Input() setLimit;
  isLoading: boolean = false;
  questionStatus = QuestionUnitStatusEnum;
  qType = QuestionUnitTypeEnum;
  questions: QuestionUnit[] = [];
  selectedQuestions = [];
  questionCategoryName: string;
  questionCategoryId: number;
  questionType: number;
  questionCount: number;
  pagination: PaginationModel = new PaginationModel();
  testTemplateId: number;
  sequenceType: SequenceTypeEnum;
  checked;
  itemsToAdd = [];
  selectQuestions = SelectionTypeEnum.All;
  questiosToAdd = []
  orderQuestions = SequenceTypeEnum.Random;
  tableData: any;
  maxQuestionCount: number;

  title: string;
  description: string;

  preview: boolean = false;

  constructor(private dragulaService: DragulaService,
    private questionService: QuestionService,
    private referenceService: ReferenceService,
    private questionCategoryService: TestTemplateQuestionCategoryService,
	  public translate: I18nService,
    private route: ActivatedRoute,
    public router: Router,
    private notificationService: NotificationsService,
    private testTemplateService: TestTemplateService) { }

  ngOnInit(): void {
    this.dragulaService.createGroup('Questions', {});
    this.getTestTemplateId();
  }

  getTestTemplateId() {
    this.route.params.subscribe(params => {
      this.testTemplateId = params.id;
      if (this.testTemplateId) {
        this.testTemplateService.getTestTemplate(this.testTemplateId).subscribe( res => {
          this.maxQuestionCount = res.data.questionCount;
        })
      }
    });
  }

  getCount(): void {
    this.questionCount = this.itemsToAdd.length ;
  }

  selectQuestion(event) {
    if (event.target.checked === true) {
      let idToInclude = event.target.value;
      this.selectedQuestions.push(+idToInclude);
    } else if (event.target.checked === false) {
      let idToExclude = event.target.value;
      this.selectedQuestions.splice(this.selectedQuestions.indexOf(+idToExclude), 1);
    }

    this.checked = Object.keys(this.selectedQuestions).map((id, index) => {
      return {
        index: +id + 1,
        id: Object.values(this.selectedQuestions)[index]
      }
    });

    this.itemsToAdd = Object.keys(this.selectedQuestions).map((id, index) => {
      return {
        index: +id + 1,
        testTemplateQuestionCategoryId: this.questionCategoryId,
        questionUnitId: Object.values(this.selectedQuestions)[index]
      }
    });
    this.getCount();
  }

  onChangeSelection(event){
    this.selectQuestions = event;
    this.reset();
  }

  onChangeSequence(event){
    this.sequenceType = event;
  }

  previewQuestions() {
    this.selectedQuestions = [];

    let params = {
      testTemplateId: +this.testTemplateId,
      categoryId: this.questionCategoryId,
      questionCount: this.questionCount,
      questionType: this.questionType,
      selectionType: this.selectQuestions,
      sequenceType: this.sequenceType,
      selectedQuestions: this.checked
    }

      this.questionCategoryService.preview(params).subscribe(res => {
        if (res && res.data) {
          this.questions = res.data;
          this.preview = true;
        }
      }, err => {
        this.reset();
      })
  }

  addQuestions(questions) {
    if(this.selectQuestions === 1) {
      let idList = questions.map(x => x.questionUnitId);
      this.questiosToAdd = Object.keys(idList).map((id, index) => {
        return {
          index: +id + 1,
          testTemplateQuestionCategoryId: this.questionCategoryId,
          questionUnitId: Object.values(idList)[index]
        }
      });
    }

    let params = {
      testTemplateId: +this.testTemplateId,
      questionCategoryId: this.questionCategoryId,
      categoryIndex: 0,
      questionCount: this.questionCount,
      questionType: this.questionType,
      selectionType: this.selectQuestions,
      sequenceType: this.sequenceType,
      testCategoryQuestions: this.questions,
      timeLimit: this.setLimit ? this.timeLimit : null
    }
    this.questionCategoryService.add(params).subscribe(res => {
      if (res && res.data) {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('questions.succes-add-multiple-msg'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
          });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.router.navigate(['test-type/type-details', this.testTemplateId, 'categories'])
      }
    })
  }

  initData(data): void {
    this.questionCategoryId = +data.questionCategoryId;
    this.questionType = +data.type;
    this.getQuestionCategory(data.questionCategoryId);
    this.list();
  }

  list(data: any = {}): void {
    let params = {
      categoryId: this.questionCategoryId,
      type: this.questionType,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }
    this.questionService.getActiveQuestions(params).subscribe(res => {
      this.questions = res.data.items;
      this.pagination = res.data.pagedSummary;
    })
  }

  reset(): void {
    this.list();
    this.preview = false;
    this.questionCount = null;
    this.selectedQuestions = [];
  }

  refresh(): void {
    this.previewQuestions();
  }

  getQuestionCategory(id): void {
    this.isLoading = true;
    this.referenceService.getQuestionCategory().subscribe(res => {
      this.questionCategoryName = res.data.filter(el => el.value === id).map(name => name.label);
    });
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.dragulaService.destroy('Questions');
  }
}
