import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { createCustomElement } from '@angular/elements';
import { ConfirmModalComponent } from '@erp/shared';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestService } from '../../../utils/services/test/test.service';
import { Test } from '../../../utils/models/tests/test.model';
import { TestOptions } from '../../../utils/models/test-questions/test-options.model';
import { TestAnswer } from '../../../utils/models/test-questions/test-answer.model';
import { AddTestQuestion } from '../../../utils/models/test-questions/add-test-question.model';
import { TestTypeSettings } from '../../../utils/models/test-types/test-type-settings.model';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { AnswerStatusEnum } from '../../../utils/enums/answer-status.enum';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';

@Component({
  selector: 'app-multiple-per-page-performing-test',
  templateUrl: './multiple-per-page-performing-test.component.html',
  styleUrls: ['./multiple-per-page-performing-test.component.scss']
})
export class MultiplePerPagePerformingTestComponent implements OnInit {

  testId;
  timeLeft;
  interval;
  pageNumber: number = 1;
  count: number;
  pageSize: number = 10;

  testDto = new Test();
  testOptionsList: TestOptions[] = [];
  testQuestions: TestOptions[] = [];
  testAnswersInput: TestAnswer[] = [];
  settings = new TestTypeSettings();

  questionTypeEnum = QuestionUnitTypeEnum;
  answerStatusEnum = AnswerStatusEnum;

  constructor(
    private activatedRoute: ActivatedRoute,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private modalService: NgbModal,
    private router: Router,
    private testTypeService: TestTypeService,
    private injector: Injector
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.getTestById();
    this.startTimer();
    this.subscribeForHashedAnswers()
  }

  getTestById() {
    this.testService.getTest({ id: this.testId }).subscribe(
      res => {
        this.testDto = res.data;
        this.getTestTypeSettings(res.data.testTypeId);
      }
    )
  }

  getTestTypeSettings(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTypeId: testTypeId }).subscribe(res => {
      this.settings = res.data;
      this.getTestQuestions();
    });
  }

  ngDoBoostrap() {
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
  }

  subscribeForHashedAnswers(el?) {
    this.testQuestionService.answerSubject.subscribe(res => {
      if (res && el) {
        const index = el.hashedOptions.findIndex(x => x.id == res.optionId);
        el.hashedOptions[index].answer = res.answer;
        el.hashedOptions.forEach(x => {
          this.testAnswersInput.push({ optionId: x.id, answerValue: x.answer })
        });
      }
    })
  }

  getTestQuestions(questionIndex?: number) {
    this.pageNumber = questionIndex == null ? this.pageNumber : questionIndex;

    this.testQuestionService.getTestQuestions({ testId: this.testId, page: this.pageNumber, itemsPerPage: this.settings.questionsCountPerPage }).subscribe(
      res => {
        this.testQuestions = res.data.items;
        this.count = res.data.pagedSummary.totalCount;

        this.ngDoBoostrap();
      }
    )
  }
  
  postAnswer() {
    this.testQuestions.forEach(el => {
      this.testQuestionService.postTestQuestions(this.parse(el)).subscribe(() => {
        this.testAnswersInput = [];
      })
    })
    if (this.pageNumber < this.count/this.settings.questionsCountPerPage)
      this.getTestQuestions(this.pageNumber + 1);
    else {
      this.getTestQuestions(1);
    }
  }

  parseAnswer(item) {
    return new TestAnswer({
      optionId: item.id,
      answerValue: item.answer
    });
  }

  parseTextAnswer(el) {
    return new TestAnswer({
      optionId: 0,
      answerValue: el.answerText
    });
  }

  onItemChange(event, element) {
    element.options.forEach(el => {
      if (el.id == event.target.value) {
        el.isSelected = event.target.checked;
      } else {
        if (element.questionType == QuestionUnitTypeEnum.OneAnswer) {
          el.isSelected = false;
        }
      }
    });
  }

  saveAnswers(el) {
    this.testAnswersInput = [];

    if (el.questionType == QuestionUnitTypeEnum.FreeText)
      this.testAnswersInput.push(this.parseTextAnswer(el));
    if (el.questionType == QuestionUnitTypeEnum.HashedAnswer)
      this.subscribeForHashedAnswers(el);
    else {
      var selectedOptions = el.options.filter(item => item.isSelected == true);

      selectedOptions.forEach(el => {
        this.testAnswersInput.push(this.parseAnswer(el));
      });
    }

    return this.testAnswersInput;
  }

  parse(el) {
    return new AddTestQuestion({
      testId: this.testId,
      questionIndex: el.questionUnitId,
      status: AnswerStatusEnum.Answered,
      answers: this.saveAnswers(el)
    });
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

  milisecondsToHms(miliseconds) {
    var seconds = Number(miliseconds / 1000);
    var h = Math.floor(seconds % (3600 * 24) / 3600);
    var m = Math.floor(seconds % 3600 / 60);
    var s = Math.floor(seconds % 60);

    return ` ${h < 10 ? '0' + h : h} : ${m < 10 ? '0' + m : m} : ${s < 10 ? '0' + s : s}`;
  }

  submitTest() {
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = "Finish Test";
    modalRef.componentInstance.description = "Do you want to finish test ?";
    modalRef.result.then(
      () => {
        clearInterval(this.interval);
        this.router.navigate(['tests/finish-page', this.testId]);
      }
    );
  }

  finalizeTest() {
    this.testService.finalizeTest(this.testId).subscribe(() => this.router.navigate(['tests/finish-page', this.testId]));
  }
}
