import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { createCustomElement } from '@angular/elements';
import { OptionModel } from '../../../utils/models/options/option.model';
import { Test } from '../../../utils/models/tests/test.model';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { CloudFileService } from '../../../utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpEvent, HttpEventType } from '@angular/common/http';

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
	isLoadingMedia: boolean;
	imageUrl: any;
	audioUrl: any;
	videoUrl: any;
	filenames: any;
	fileName: string;
	fileId: string;

  constructor(
    private verifyService: TestVerificationProcessService,
    private activatedRoute: ActivatedRoute,
    private testService: TestService,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private router: Router,
    private location: Location,
		private fileService : CloudFileService,
		private sanitizer: DomSanitizer
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

    this.verifyService.getTest(testData).subscribe(
      (res) => {
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
          this.fileId = res.data.questionUnitMediaFileId;
            if (res.data.questionUnitMediaFileId) this.getMediaFile(this.fileId);
        }
      },
      (err) => {
        err.error.messages.some(x => {
          if (x.code === '03001609')
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

  logout(): void {
    localStorage.removeItem('idnp');
    this.router.navigate(['public']);
  }

  getMediaFile(fileId) {
		this.isLoadingMedia = true;
		this.fileService.get(fileId).subscribe( res => {
		  this.resportProggress(res);
		})
	  }
	
	  private resportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
		switch(httpEvent.type)
		{
		  case HttpEventType.Response:
			if (httpEvent.body instanceof Array) {
			  for (const filename of httpEvent.body) {
				this.filenames.unshift(filename);
			  }
			} else {
			  this.fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
			  const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
			  const file = new File([blob], this.fileName, { type: httpEvent.body.type });
			  this.readFile(file).then(fileContents => {
				if (blob.type.includes('image')) this.imageUrl = fileContents;
				else if (blob.type.includes('video')) this.videoUrl = fileContents;
				else if (blob.type.includes('audio')) {
				  this.audioUrl = fileContents;
				  this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
				}
			  }).then(this.videoUrl = this.audioUrl = this.imageUrl = this.fileName = null);
			this.isLoadingMedia = false;
		  }
		  break;
		}
	  }
	
	  public async readFile(file: File): Promise<string | ArrayBuffer> {
		return new Promise<string | ArrayBuffer>((resolve, reject) => {
		  const reader = new FileReader();
	  
		  reader.onload = e => {
			return resolve((e.target as FileReader).result);
		  };
	
		  reader.onerror = e => {
			console.error(`FileReader failed on file ${file.name}.`);
			return reject(null);
		  };
	
		  if (!file) {
			console.error('No file to read.');
			return reject(null);
		  }
	
		  reader.readAsDataURL(file);
		});
	  }
}
