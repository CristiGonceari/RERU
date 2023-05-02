import { Component, Injector, OnDestroy, OnInit } from '@angular/core';
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
import { FileTestAnswerService } from '../../../utils/services/FileTestAnswer/file-test-answer.service';
import { saveAs } from 'file-saver';
import { EnumStringTranslatorService } from '../../../utils/services/enum-string-translator.service';
import { StyleNodesService } from '../../../utils/services/style-nodes/style-nodes.service';

@Component({
  selector: 'app-view-evaluation-result',
  templateUrl: './view-evaluation-result.component.html',
  styleUrls: ['./view-evaluation-result.component.scss']
})
export class ViewEvaluationResultComponent implements OnInit, OnDestroy {
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

  files: any[] = [];
  fileName: string;
  answerFileid: string;
  hadFile: boolean = false;

  optionFileId = [];
  isLoadingOptionMedia:  boolean = true;

  stylesToApply: string = '.breadcrumb{ display: none !important; }';
  
  constructor(
    private verifyService: TestVerificationProcessService,
    private activatedRoute: ActivatedRoute,
    private testService: TestService,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private router: Router,
    private location: Location,
    private fileTestAnswerService: FileTestAnswerService,
    private enumStringTranslatorService: EnumStringTranslatorService,
    private styleNodesService: StyleNodesService
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
    this.styleNodesService.addStyle('breadcrumb', this.stylesToApply);
  }
  
  ngOnDestroy() {
    this.styleNodesService.removeStyle('breadcrumb');
  }

  ngDoBoostrap() {
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
  }

  translateResultValue(item){
		return this.enumStringTranslatorService.translateTestResultValue(item);
	}

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      (res) => {
        this.testData = res.data;
      }
    );
  }

  checkIfHadFile(){
    let params = {
      questionIndex: this.index,
      testId: this.testId
    };

    this.fileTestAnswerService.getList(params).subscribe(res => {
      if(res.data.fileId !== null){
        this.answerFileid = res.data.fileId;
        this.fileName = res.data.fileName;
        this.hadFile = true;
      } else {
        this.hadFile = false;
      }
    })
  }

  getFile() {
    this.fileTestAnswerService.getFile(this.answerFileid).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

  checkFileNameLength() {
    return this.fileName.length <= 20 ? this.fileName : this.fileName.slice(0, 20) + "...";
  }

  getSummary(): void {
    this.verifyService.getSummary(this.testId).subscribe(
      (res) => {
        for (let i = 1; i <= res.data.testQuestions.length; i++) {
          this.pager.push(i);
        };
        this.summaryList = res.data.testQuestions;
        this.result = res.data.resultValue;
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

    this.checkIfHadFile();

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
    this.router.navigate(['/my-activities/my-evaluations']);
  }

  next(): void {
    if (this.index < this.summaryList.length)
      this.processTestQuestion(this.index + 1)
    else this.processTestQuestion(1)
  }
}
