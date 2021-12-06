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
import { TestTypeQuestionCategoryService } from 'projects/evaluation/src/app/utils/services/test-type-question-category/test-type-question-category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';

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
  testTypeId: number;
  sequenceType: SequenceTypeEnum;
  checked;
  itemsToAdd = [];
  selectQuestions = SelectionTypeEnum.All;
  questiosToAdd = []
  orderQuestions = SequenceTypeEnum.Random;
  tableData: any;
  maxQuestionCount: number;

  preview: boolean = false;

  constructor(private dragulaService: DragulaService,
    private questionService: QuestionService,
    private referenceService: ReferenceService,
    private questionCategoryService: TestTypeQuestionCategoryService,
    private route: ActivatedRoute,
    public router: Router,
    private notificationService: NotificationsService,
    private testTypeService: TestTypeService) { }

  ngOnInit(): void {
    this.dragulaService.createGroup('Questions', {});
    this.getTestTypeId();
  }

  getTestTypeId() {
    this.route.params.subscribe(params => {
      this.testTypeId = params.id;
      if (this.testTypeId) {
        this.testTypeService.getTestType(this.testTypeId).subscribe( res => {
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
        testTypeQuestionCategoryId: this.questionCategoryId,
        questionUnitId: Object.values(this.selectedQuestions)[index]
      }
    });
    this.getCount();
  }

  onChangeSelection(event){
    this.selectQuestions = event;
    this.reset()
  }

  onChangeSequence(event){
    this.sequenceType = event;
  }

  previewQuestions() {
    this.selectedQuestions = [];

    let params = {
      testTypeId: +this.testTypeId,
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
          testTypeQuestionCategoryId: this.questionCategoryId,
          questionUnitId: Object.values(idList)[index]
        }
      });
    }

    let params = {
      testTypeId: +this.testTypeId,
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
        this.notificationService.success('Success', 'Questions were successfully added', NotificationUtil.getDefaultMidConfig());
        this.router.navigate(['test-type/type-details', this.testTypeId, 'categories'])
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
  }

  refresh(): void {
    this.previewQuestions();
  }

  getQuestionCategory(id): void {
    this.referenceService.getQuestionCategory().subscribe(res => {
      this.questionCategoryName = res.data.filter(el => el.value === id).map(name => name.label);
    });
  }

  ngOnDestroy(): void {
    this.dragulaService.destroy('Questions');
  }
}
