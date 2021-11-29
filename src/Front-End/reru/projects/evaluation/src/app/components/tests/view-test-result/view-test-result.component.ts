import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { Location } from '@angular/common';
import { createCustomElement } from '@angular/elements';
import { OptionModel } from '../../../utils/models/options/option.model';
import { Test } from '../../../utils/models/tests/test.model';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-view-test-result',
  templateUrl: './view-test-result.component.html',
  styleUrls: ['./view-test-result.component.scss']
})
export class ViewTestResultComponent implements OnInit {

  isDisabled = true;
  testId: number;
  index = 1;
  count: number;
  question: string;
  answer: string;
  comment: string;
  correct: boolean;
  testData = new Test();
  options = new OptionModel();
  verifiedStatus = [];
  questionType;
  enum = QuestionUnitTypeEnum;
  hashedOptions;
  correctAnswer: any;
  notCorrectAnswer: any;
  pager: number[] = [];
  correctQuestion;
  summaryList;
  closed;
  result;
  correctAnswers;
  isLoading: boolean = true;
  maxPoints: number;
  accumulatedPoints: number;

  constructor(
    private verifyService: TestVerificationProcessService,
    private activatedRoute: ActivatedRoute,
    private testService: TestService,
    private notificationService: NotificationsService,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private translate: I18nService,
    private router: Router,
    private location: Location
  ) { 
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.getSummary();
    this.getTestById();
    this.ngDoBoostrap();
    this.testQuestionService.setData(true);
  }

  ngDoBoostrap() {
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
  }

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      res => {
        this.testData = res.data;
      },
      error => {
        this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
      }
    );
  }

  getSummary(): void {
    this.verifyService.getSummary(this.testId).subscribe(
      (res) => {
        for (var i = 1; i <= res.data.testQuestions.length; i++) {
          this.pager.push(i);
        };
        this.summaryList = res.data.testQuestions;
        this.result = res.data.result;
        this.correctAnswers = res.data.correctAnswers;
        this.count = res.data.testQuestions.length;
        this.verifiedStatus = res.data.testQuestions.map(el => el.isCorrect);
        this.notCorrectAnswer = res.data.testQuestions.filter(st => st.isCorrect === false).map(id => id.index);
        this.correctAnswer = res.data.testQuestions.filter(st => st.isCorrect === true).map(id => id.index);
        this.processTestQuestion(1);
      }
    );
  }

  getCorrect(page) {
    return !!(this.correctAnswer.includes(page));
  }

  getNotCorrect(page) {
    return !!(this.notCorrectAnswer.includes(page));
  }

  processTestQuestion(index?: number): void {
    this.index = index == null ? this.index : index;

    const testData = {
      testId: +this.testId,
      questionIndex: this.index
    };

    this.verifyService.getTest(testData).subscribe( res => {
      if (res && res.data) {
        this.question = res.data.question;
        this.correctQuestion = res.data.correctHashedQuestion;
        this.answer = res.data.answerText;
        this.comment = res.data.comment;
        this.options = res.data.options;
        this.correct = res.data.isCorrect;
        if (this.correct == null) this.correct = false;
        this.index = index;
        this.questionType = res.data.questionType;
        this.isLoading = false;
        this.maxPoints = res.data.questionMaxPoints;
        this.accumulatedPoints = res.data.evaluatorPoints;
      }
    });
  }

  backClicked() {
		this.location.back();
	}

  close() {
		this.router.navigate(['/my-activities']);
	}

  next(): void {
    if (this.index < this.summaryList.length)
      this.processTestQuestion(this.index + 1)
    else this.processTestQuestion(1)
  }

  logout(): void {
    localStorage.removeItem('idnp');
    this.router.navigate(['public']);  
  }
}
