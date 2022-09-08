import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { createCustomElement } from '@angular/elements';
import { Test } from '../../../utils/models/tests/test.model';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { TestResultStatusEnum } from '../../../utils/enums/test-result-status.enum';

@Component({
  selector: 'app-view-evaluation-result',
  templateUrl: './view-evaluation-result.component.html',
  styleUrls: ['./view-evaluation-result.component.scss']
})
export class ViewEvaluationResultComponent implements OnInit {
  testId: number;
  index = 1;
  count: number;
  question: string;
  answer: string;
  comment: string;
  correct: boolean;
  testData = new Test();
  options = [];

  verifiedStatus = [];
  questionType;
  enum = QuestionUnitTypeEnum;
  resultEnum = TestResultStatusEnum;
  correctAnswer: any;
  notCorrectAnswer: any;
  pager: number[] = [];
  correctQuestion;
  summaryList;
  result;
  correctAnswers;
  isLoading: boolean = true;
  maxPoints: number;
  accumulatedPoints: number;
	isLoadingMedia: boolean;
	fileId: string;
  showNegativeMessage: boolean;

  optionFileId = [];
  isLoadingOptionMedia:  boolean = true;
  
  constructor(
    private verifyService: TestVerificationProcessService,
    private activatedRoute: ActivatedRoute,
    private testService: TestService,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private router: Router,
    private location: Location,
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
      (res) => {
        this.testData = res.data;
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

    this.verifyService.getEvaluationQuestion(testData).subscribe(
      (res) => {
        if (res && res.data) {
          this.question = res.data.question;
          this.correctQuestion = res.data.correctHashedQuestion;
          this.answer = res.data.answerText;
          this.comment = res.data.comment;
          this.options = res.data.options;
          this.correct = res.data.isCorrect;
          this.showNegativeMessage = res.data.showNegativeMessage;
          if (this.correct == null) this.correct = false;
          this.index = index;
          this.questionType = res.data.questionType;
          this.isLoading = false;
          this.maxPoints = res.data.questionMaxPoints;
          this.accumulatedPoints = res.data.evaluatorPoints;
          this.fileId = res.data.questionUnitMediaFileId;
        }
      },
      (err) => {
        err.error.messages.some(x => {
          if (x.code === '03001610')
            this.router.navigate(['../../../tests'], { relativeTo: this.activatedRoute })
        })
      }
    );
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
}
