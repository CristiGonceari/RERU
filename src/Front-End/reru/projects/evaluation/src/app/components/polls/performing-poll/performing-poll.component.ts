import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@erp/shared';
import { AnswerStatusEnum } from '../../../utils/enums/answer-status.enum';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { QuestionUnit } from '../../../utils/models/question-units/question-unit.model';
import { AddTestQuestion } from '../../../utils/models/test-questions/add-test-question.model';
import { TestAnswer } from '../../../utils/models/test-questions/test-answer.model';
import { TestOptions } from '../../../utils/models/test-questions/test-options.model';
import { TestQuestionSummary } from '../../../utils/models/test-questions/test-question-summary.model';
import { TestQuestion } from '../../../utils/models/test-questions/test-question.model';
import { TestTypeSettings } from '../../../utils/models/test-types/test-type-settings.model';
import { TestType } from '../../../utils/models/test-types/test-type.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestService } from '../../../utils/services/test/test.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-performing-poll',
  templateUrl: './performing-poll.component.html',
  styleUrls: ['./performing-poll.component.scss']
})
export class PerformingPollComponent implements OnInit {

  testId: number;
  questionIndex: number = 1;
  testDto = new Test();
  pager: number[] = [];
  viewed: number[] = [];
  skipped: number[] = [];
  answered: number[] = [];

  timeLeft;
  interval;
  count: number;

  testOptionsList: TestOptions[] = [];
  testQuestionSummary: TestQuestionSummary[] = [];
  questionUnit = new TestQuestion();
  questionsList: QuestionUnit[] = [];
  testAnswersInput: TestAnswer[] = [];
  testQuestion: TestQuestion[] = [];
  testTypeModel = new TestType();
  testTypeSettings = new TestTypeSettings();
  hashedOptions;

  textAnswer: string;
  answerStatus: number;

  questionTypeEnum = QuestionUnitTypeEnum;
  answerStatusEnum = AnswerStatusEnum;
  disable;
  disableNext = false;
  
  constructor(
    private activatedRoute: ActivatedRoute,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private modalService: NgbModal,
    private router: Router,
    private testTypeService: TestTypeService,
		private notificationService: NotificationsService
  ) { 
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.summary();
    this.getTestById();
    this.testQuestionService.setData(false);
  }

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      res => {
        this.testDto = res.data;
        this.getTestType(res.data.testTypeId);
      }
    )
  }

  getTestType(testTypeId) {
    this.testTypeService.getTestType(testTypeId ).subscribe(res => this.getTestTypeSettings(res.data.id))
  }

  summary() {
    this.testQuestionService.summary(this.testId).subscribe(
      (res) => {
        for (var i = 1; i <= res.data.length; i++) {
          this.pager.push(i);
        };
        this.testQuestionSummary = res.data;
        this.count = this.testQuestionSummary.length;
        this.viewed = res.data.filter(st => st.answerStatus === 1).map(id => id.index);
        this.skipped = res.data.filter(st => st.answerStatus === 2).map(id => id.index);
        this.answered = res.data.filter(st => st.answerStatus === 3).map(id => id.index);
        if (!this.testTypeSettings.possibleChangeAnswer || !this.testTypeSettings.possibleGetToSkipped)
          this.disable = res.data.filter(st => (st.answerStatus === 3 || st.answerStatus === 2)).map(id => id.index);
        this.getTestQuestions();
      });
  }

  getViewed(page) {
    return !!(this.viewed.includes(page));
  }

  getSkipped(page) {
    return !!(this.skipped.includes(page));
  }

  getAnswered(page) {
    return !!(this.answered.includes(page));
  }

  disabled(page) {
    return !!(this.disable.includes(page));
  }

  parseAnswer(item) {
    return new TestAnswer({
      optionId: item.id,
      answerValue: item.answer
    });
  }

  onItemChange(event) {
    this.testOptionsList.forEach(el => {
      if (el.id == event.target.value) {
        el.isSelected = event.target.checked;
      } else {
        if (this.questionUnit.questionType == QuestionUnitTypeEnum.OneAnswer) {
          el.isSelected = false;
        }
      }
    });
  }

  parse(status) {
    return new AddTestQuestion({
      testId: +this.testId,
      questionIndex: this.questionIndex,
      questionUnitId: null,
      status: status,
      answers: this.testAnswersInput
    });
  }

  saveAnswers() {
    this.testAnswersInput = [];

    var selectedOptions = this.testOptionsList.filter(Item => Item.isSelected == true);

    selectedOptions.forEach(el => {
      this.testAnswersInput.push(this.parseAnswer(el));
    });

    this.postAnswer(+this.answerStatusEnum.Answered);
  }

  getTestTypeSettings(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTypeId: testTypeId }).subscribe(
      res => {
        this.testTypeSettings = res.data;
        if (res.data == null) this.testTypeSettings = new TestTypeSettings();
      }
    );
  }

  postAnswer(status) {
    this.testQuestionService.postTestQuestions(this.parse(status)).subscribe(
      res => {
        this.testAnswersInput = [];

        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            this.testQuestionSummary = res.data;
            this.viewed = res.data.filter(st => st.answerStatus === 1).map(id => id.index);
            this.skipped = res.data.filter(st => st.answerStatus === 2).map(id => id.index);
            this.answered = res.data.filter(st => st.answerStatus === 3).map(id => id.index);
            if (!this.testTypeSettings.possibleChangeAnswer)
              this.disable = res.data.filter(st => (st.answerStatus === 3 || st.answerStatus === 2)).map(id => id.index);

            if (this.testQuestionSummary.every(x => x.isClosed === true)) {
              this.submitTest();
            } else if (!this.testTypeSettings.possibleChangeAnswer || !this.testTypeSettings.possibleGetToSkipped) {
              var isNotClosedAnswers = this.testQuestionSummary.filter(x => x.isClosed === false);

              this.questionIndex = isNotClosedAnswers.some(x => x.index > this.questionIndex) ?
                isNotClosedAnswers.filter(x => x.index > this.questionIndex)[0].index :
                isNotClosedAnswers.filter(x => x.index < this.questionIndex)[0].index;
              this.getTestQuestions(this.questionIndex);
            } else {
              if (this.questionIndex < this.count)
                this.getTestQuestions(this.questionIndex + 1);
              else {
                this.getTestQuestions(1);
              }
            }
          });
      }
    )
  }

  skipQuestion() {
    this.postAnswer(+this.answerStatusEnum.Skipped);
  }

  getTestQuestions(questionIndex?: number) {
    this.questionIndex = questionIndex == null ? this.questionIndex : questionIndex;

    if (this.testQuestionSummary.find(x => x.index === this.questionIndex).isClosed && this.questionIndex == 1) {
      this.questionIndex = this.testQuestionSummary.find(x => x.isClosed === false).index;
    }

    if (this.testQuestionSummary.find(x => x.index === this.questionIndex).isClosed && !this.testTypeSettings.possibleChangeAnswer) {
      this.questionIndex = this.testQuestionSummary.find(x => x.isClosed === false).index
      questionIndex = this.testQuestionSummary.find(x => x.isClosed === false).index
    }

    this.testQuestionService.getTestQuestion({ testId: this.testId, questionIndex: this.questionIndex }).subscribe(
      res => {
        this.questionUnit = res.data;
        this.testOptionsList = res.data.options;
        this.hashedOptions = res.data.hashedOptions;

        if (this.questionUnit.answerStatus == AnswerStatusEnum.None)
          this.testQuestionService.postTestQuestions(this.parse(AnswerStatusEnum.Viewed)).subscribe(
            res => {
              this.testQuestionService.summary(this.testId).subscribe(
                res => {
                  this.viewed = res.data.filter(st => st.answerStatus === 1).map(id => id.index);
                }
              );
            }
          );
      }
    )
  }

  submitTest() {
    if (this.testQuestionSummary.every(x => x.isClosed === true)) {
      this.testQuestionService.summary(this.testId).subscribe(() => this.disableNext = true);
    }

      const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
      modalRef.componentInstance.title = "Finish Poll";
      modalRef.componentInstance.description = "Do you want to finish this poll?";
      modalRef.result.then(
        () => {
          this.testService.finalizeTest(this.testId).subscribe(() => 
      {
        this.notificationService.success('Success', 'Poll was successfully terminated', NotificationUtil.getDefaultMidConfig());
        this.router.navigate(['../../../my-activities/my-polls'], { relativeTo: this.activatedRoute })
      });
        }
      );
  }
}
