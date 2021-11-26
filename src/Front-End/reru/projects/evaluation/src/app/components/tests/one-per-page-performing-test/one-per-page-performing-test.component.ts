import { Component, Injector, OnInit } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { AnswerStatusEnum } from '../../../utils/enums/answer-status.enum';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestOptions } from '../../../utils/models/test-questions/test-options.model';
import { TestQuestionSummary } from '../../../utils/models/test-questions/test-question-summary.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestQuestion } from '../../../utils/models/test-questions/test-question.model';
import { TestAnswer } from '../../../utils/models/test-questions/test-answer.model';
import { TestType } from '../../../utils/models/test-types/test-type.model';
import { TestTypeSettings } from '../../../utils/models/test-types/test-type-settings.model';
import { AddTestQuestion } from '../../../utils/models/test-questions/add-test-question.model';
import { SafeHtmlPipe } from '../../../utils/pipes/safe-html.pipe';
import { ConfirmModalComponent } from '@erp/shared';

@Component({
  selector: 'app-one-per-page-performing-test',
  templateUrl: './one-per-page-performing-test.component.html',
  styleUrls: ['./one-per-page-performing-test.component.scss'],
  providers: [SafeHtmlPipe]

})
export class OnePerPagePerformingTestComponent implements OnInit {

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

  timeQuestionLeft;
  timerInterval;
  percent;

  testOptionsList: TestOptions[] = [];
  testQuestionSummary: TestQuestionSummary[] = [];
  questionUnit = new TestQuestion();
  testAnswersInput: TestAnswer[] = [];
  testQuestion: AddTestQuestion[] = [];
  testTypeModel = new TestType();
  testTypeSettings = new TestTypeSettings();
  hashedOptions;

  textAnswer: string;
  answerStatus: number;

  questionTypeEnum = QuestionUnitTypeEnum;
  answerStatusEnum = AnswerStatusEnum;
  disable: number[] = [];
  disableNext = false;
  isLoading = true;
  testtypeId;

  constructor(
    private activatedRoute: ActivatedRoute,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private modalService: NgbModal,
    private router: Router,
    private testTypeService: TestTypeService
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.summary();
    this.startTimer();
    this.getTestById();
    this.subscribeForHashedAnswers();
    this.testQuestionService.setData(false);
  }

  subscribeForHashedAnswers() {
    this.testQuestionService.answerSubject.subscribe(res => {
      console.log(res);
      if (res && res != undefined) {
        const index = this.hashedOptions.findIndex(x => x.id == res.optionId);
        this.hashedOptions[index].answer = res.answer;
        this.hashedOptions.forEach(element => {
          this.testAnswersInput.push({ optionId: element.id, answerValue: element.answer })
        });
      }
    })
  }

  startTimer() {
    this.interval = setInterval(() => {
      var endTime = new Date(this.testDto.endTime).getTime();
      var date = new Date().getTime();

      if (endTime > date) {
        this.timeLeft = this.milisecondsToHms(Math.abs(endTime - date));
      } else {
        this.timeLeft = this.milisecondsToHms(endTime);
        clearInterval(this.interval);
        this.finalizeTest();
      }
    }, 1000)
  }

  startQuestionTimer() {
    this.timeQuestionLeft = this.questionUnit.timeLimit;
    this.percent = this.timeQuestionLeft * 100 / this.questionUnit.timeLimit;

    this.timerInterval = setInterval(() => {
      if (this.timeQuestionLeft > 0) {
        this.timeQuestionLeft--;
        this.percent = this.timeQuestionLeft * 100 / this.questionUnit.timeLimit;
      } else {
        clearInterval(this.timerInterval);
        if (this.questionIndex < this.count)
          this.getTestQuestions(this.questionIndex + 1);
        else {
          this.getTestQuestions(1);
        }
      }
    }, 1000);
  }

  milisecondsToHms(miliseconds) {
    var seconds = Number(miliseconds / 1000);
    var h = Math.floor(seconds % (3600 * 24) / 3600);
    var m = Math.floor(seconds % 3600 / 60);
    var s = Math.floor(seconds % 60);

    return ` ${h < 10 ? '0' + h : h} : ${m < 10 ? '0' + m : m} : ${s < 10 ? '0' + s : s}`;
  }

  ngDoBoostrap() {
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
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
    this.testTypeService.getTestType(testTypeId).subscribe(res => { this.testtypeId = res.data.id; this.getTestTypeSettings(res.data.id) });
  }

  summary() {
    this.testQuestionService.summary(this.testId).subscribe((res) => {
      for (var i = 1; i <= res.data.length; i++) {
        this.pager.push(i);
      };
      this.testQuestionSummary = res.data;
      this.count = this.testQuestionSummary.length;
      this.pageColor(res.data);
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
        if (this.questionUnit.questionType == this.questionTypeEnum.OneAnswer) {
          el.isSelected = false;
        }
      }
    });
  }

  parseTextAnswer() {
    return new TestAnswer({
      optionId: 0,
      answerValue: this.questionUnit.answerText
    });
  }

  parse(status) {
    return{
      data: new AddTestQuestion({
        testId: this.testId,
        questionIndex: this.questionIndex,
        questionUnitId: null,
        status: status,
        answers: this.testAnswersInput
      })
    }
  }

   saveAnswers() {
    this.testAnswersInput = [];

    if (this.questionUnit.questionType == this.questionTypeEnum.FreeText)
      this.testAnswersInput.push(this.parseTextAnswer());
    if (this.questionUnit.questionType == this.questionTypeEnum.HashedAnswer)
      this.subscribeForHashedAnswers();
    else {
      var selectedOptions = this.testOptionsList.filter(Item => Item.isSelected == true);

      selectedOptions.forEach(el => {
        this.testAnswersInput.push(this.parseAnswer(el));
      });
    }

    this.postAnswer(+this.answerStatusEnum.Answered);
  }

  checkIfDisabled(index) {
    return this.testQuestionSummary.find(x => x.index == index).isClosed;
  }

  getTestTypeSettings(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTypeId: testTypeId }).subscribe(
      res => {
        this.testTypeSettings = res.data;
        this.isLoading = false;
        if (res.data == null) this.testTypeSettings = new TestTypeSettings();
      }
    );
  }

  pageColor(data) {
    this.viewed = data.filter(st => st.answerStatus === 1).map(id => id.index);
    this.skipped = data.filter(st => st.answerStatus === 2).map(id => id.index);
    this.answered = data.filter(st => st.answerStatus === 3).map(id => id.index);
  }

  postAnswer(status) {
    this.testQuestionService.postTestQuestions(this.parse(status)).subscribe(
      res => {
        this.testAnswersInput = [];

        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            this.testQuestionSummary = res.data;
            this.pageColor(res.data);

            if (this.testQuestionSummary.every(x => x.isClosed === true))
              this.submitTest();
            else if (!this.testTypeSettings.possibleChangeAnswer || !this.testTypeSettings.possibleGetToSkipped) {
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

    if (this.questionUnit.timeLimit)
      clearInterval(this.timerInterval);

    if (this.testQuestionSummary.find(x => x.index === this.questionIndex).isClosed) {
      this.questionIndex = this.testQuestionSummary.find(x => x.isClosed === false).index;
    }

    this.testQuestionService.getTestQuestion({ testId: this.testId, questionIndex: this.questionIndex })
      .subscribe(
        (res) => {
          this.questionUnit = res.data;
          this.testOptionsList = res.data.options;
          this.hashedOptions = res.data.hashedOptions;

          if (this.questionUnit.timeLimit)
            this.startQuestionTimer();

          if (this.questionUnit.answerStatus == AnswerStatusEnum.None)
            this.testQuestionService.postTestQuestions(this.parse(AnswerStatusEnum.Viewed)).subscribe(
              (res) => {
                this.testQuestionService.summary(this.testId).subscribe(
                  res => {
                    this.viewed = res.data.filter(st => st.answerStatus === 1).map(id => id.index);
                  });
              });

          this.ngDoBoostrap();
        },
        (error) => {
          error.error.messages.some(x => {
            if(x.code === '03020604')
              this.finalizeTest();
          })
        }
      )
  }

  submitTest() {
    if (this.testQuestionSummary.every(x => x.isClosed === true)) {
      this.testQuestionService.summary(this.testId).subscribe(() => this.disableNext = true);
    }

    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = "Finish Test";
    modalRef.componentInstance.description = "Do you want to finish test ?";
    modalRef.result.then(
      () => {
        this.finalizeTest();
        clearInterval(this.interval);
        clearInterval(this.timerInterval);
      }
    );
  }

  finalizeTest() {
    this.testService.finalizeTest(this.testId).subscribe(() => this.router.navigate(['tests/finish-page', this.testId]));
  }
}
