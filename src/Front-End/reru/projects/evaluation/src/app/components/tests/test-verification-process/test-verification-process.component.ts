import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { createCustomElement } from '@angular/elements';
import { NotificationsService } from 'angular2-notifications';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { OptionModel } from '../../../utils/models/options/option.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';
import { TestService } from '../../../utils/services/test/test.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { CloudFileService } from '../../../utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
	selector: 'app-test-verification-process',
	templateUrl: './test-verification-process.component.html',
	styleUrls: ['./test-verification-process.component.scss']
})
export class TestVerificationProcessComponent implements OnInit {
	testId: number;
	index = 1;
	question: string;
	answer: string;
	comment: string;
	correct: boolean;
	testData = new Test();
  	options = [];
//   options = new OptionModel();
	verifiedStatus = [];
	questionType;
	enum = QuestionUnitTypeEnum;
	autoverified: any;
	verified: any;
	pager: number[] = [];
	correctQuestion;
	isLoading: boolean = true;
	maxPoints: number;
	points: number;
	questionUnitId: number;
	isLoadingMedia: boolean;
	imageUrl: any;
	audioUrl: any;
	videoUrl: any;
	filenames: any;
	fileName: string;
	fileId: string;

	title: string;
	description: string;
	description1: string;
	description2: string;
	description3: string;
	no: string;
	yes: string;

	optionFileId = [];
  	isLoadingOptionMedia:  boolean = true;
  	optionFilenames: any;
  	optionFileName: string;

	constructor(
		private verifyService: TestVerificationProcessService,
		private modalService: NgbModal,
		private activatedRoute: ActivatedRoute,
		private testService: TestService,
		private notificationService: NotificationsService,
		private injector: Injector,
		private testQuestionService: TestQuestionService,
	  	public translate: I18nService,
	  	private router: Router,
		private fileService : CloudFileService,
		private sanitizer: DomSanitizer
	) { }

	ngOnInit(): void {
		this.getTestId();
		this.getSummary(this.testId, this.index - 1);
		this.ngDoBoostrap();
		this.pagination();
		this.testQuestionService.setData(true);
	}

	ngDoBoostrap() {
		const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

		customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
	}

	getTestId(): void {
		this.activatedRoute.params.subscribe(params => {
			if (params.id) {
				this.testId = params.id;
				this.getTestById();
			}
		});
	}

	getTestById() {
		this.testService.getTest(this.testId).subscribe(
			(res) => {
				this.testData = res.data;
			}
		);
	}

	pagination() {
		this.verifyService.getSummary(this.testId).subscribe((res) => {
			for (var i = 1; i <= res.data.testQuestions.length; i++) {
				this.pager.push(i);
			};
		});
	}

	getSummary(testId, index): void {
		this.verifyService.getSummary(testId).subscribe(
			(res) => {
				this.verifiedStatus = res.data.testQuestions.map(el => el.verificationStatus);
				this.autoverified = res.data.testQuestions.filter(st => st.verificationStatus === 0).map(id => id.index);
				this.verified = res.data.testQuestions.filter(st => st.verificationStatus === 1).map(id => id.index);
				this.processTestQuestion(this.getNextIndex());
			},
		);
	}

	getAutoverified(page) {
		if (page)
			return !!(this.autoverified.includes(page));
	}

	getVerified(page) {
		if (page)
			return !!(this.verified.includes(page));
	}

	processTestQuestion(index: number): void {
		const testData = {
			testId: +this.testId,
			questionIndex: index
		};
		this.verifyService.getTest(testData).subscribe(
			(res) => {
				if (res && res.data) {
					this.question = res.data.question;
					this.correctQuestion = res.data.correctHashedQuestion;
					this.answer = res.data.answerText;
					this.comment = res.data.comment;
					this.options = res.data.options;
					this.options.map ( (option) => {
						// TODO add type Option -> options = array<Option>
						  option.videoUrl = null
						  option.imageUrl = null
						  option.audioUrl = null
						  return option;
					  })
					this.optionFileId = res.data.options.map(el => el.optionMediaFileId);
          			for (let i = 0; i < this.optionFileId.length; i++) {
            		if (this.optionFileId[i] !== null) this.getOptionsMediaFile(this.optionFileId[i], i);
          			}
					this.correct = res.data.isCorrect;
					this.index = index;
					this.questionType = res.data.questionType;
					this.isLoading = false;
					this.maxPoints = res.data.questionMaxPoints;
					this.questionUnitId = res.data.questionUnitId;
					this.points = (res.data.evaluatorPoints === 0) ? '' : res.data.evaluatorPoints;
					this.fileId = res.data.questionUnitMediaFileId;
				}
			},
			(err) => {
				err.error.messages.some(x => {
					if (x.code === '03001609')
						this.router.navigate(['../../../tests'], { relativeTo: this.activatedRoute })
				})
			});
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

	getOptionsMediaFile(optionFileId, index) {
		this.fileService.get(optionFileId).subscribe(res => {
		  this.resportOptionsProggress(res, index);
		})
	  }
	
	  private resportOptionsProggress(httpEvent: HttpEvent<string[] | Blob>, index): void
	  { 
		switch(httpEvent.type)
		{
		  case HttpEventType.Response:
			if (httpEvent.body instanceof Array) {
			  for (const filename of httpEvent.body) {
				this.optionFilenames.unshift(filename);
			  }
			} else {
			  this.optionFileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
			  const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
			  const file = new File([blob], this.optionFileName, { type: httpEvent.body.type });
			  this.readFile(file).then(fileContents => {
				if (blob.type.includes('image')) {
				  this.options[index].imageUrl = fileContents;
				}
				else if (blob.type.includes('video')) {
				  this.options[index].videoUrl = fileContents;
				}
				else if (blob.type.includes('audio')) {
				  this.options[index].audioUrl = fileContents;
				  this.options[index].audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.options[index].audioUrl);
				}
				this.isLoadingOptionMedia = false;
			  });
			}
		  break;
		}
	}
 
	verifyTest(): void {
		forkJoin([
			this.translate.get('modal.error'),
			this.translate.get('verify-test.point-value-not-selected'),
			this.translate.get('verify-test.points-must-be-greater'),
			this.translate.get('verify-test.set-points-to-zero'),
		]).subscribe(([title, description1, description2, description3]) => {
			this.title = title;
			this.description1 = description1;
			this.description2 = description2;
			this.description3 = description3;

			});
		if (this.correct === null) {
			this.notificationService.error(this.title, this.description1, null, NotificationUtil.getDefaultMidConfig());
		} else if (this.points < 1 && this.correct === true) {
			this.notificationService.error(this.title, this.description2, NotificationUtil.getDefaultMidConfig());
		} else if (this.points > 0 && this.correct === false) {
			this.notificationService.error(this.title, this.description3, NotificationUtil.getDefaultMidConfig());
		} else {
			const verifyData = {
				testId: +this.testId,
				questionIndex: this.index,
				comment: this.comment,
				isCorrect: this.correct,
				evaluatorPoints: +this.points,
				questionUnitId: this.questionUnitId
			};
			this.verifyService.verify(verifyData).subscribe(() => this.next());
		}
	}

	next() {
		if (this.isNotVerified()) {
			this.index = Math.min.apply({}, this.getNotVerified());
			this.getSummary(this.testId, this.index);
			if (this.index === this.verifiedStatus.length) {
				this.finalizeVerificationModal();
			}
			return;
		}
		this.finalizeVerificationModal();
	}

	isNotVerified(): boolean {
		if (!this.verifiedStatus.length) {
			return false;
		}

		return !!this.verifiedStatus.map((el, i) => { if (el === 2) return i }).filter(el => !isNaN(el)).length;
	}

	getNotVerified(): any[] {
		return this.verifiedStatus.map((el, i) => { if (el === 2) return i + 1 }).filter(el => !isNaN(el));
	}

	getNextIndex(): number {
		const notVerifiedIndexes = this.getNotVerified();
		const unansweredIndex = notVerifiedIndexes.length ? Math.min.apply({}, this.getNotVerified()) : 1;

		if (unansweredIndex === 1 && this.index + 1 !== this.verifiedStatus.length && !notVerifiedIndexes.length) {
			return 1;
		}

		if (this.index + 1 === unansweredIndex) {
			return this.index + 1;
		}

		if (this.index + 1 !== unansweredIndex && notVerifiedIndexes.length) {
			return unansweredIndex;
		}

		return this.verifiedStatus.length;
	}

	check(event) {
		this.correct = (!!+event.target.value);
	}

	hasValue(): boolean {
		return !(typeof this.correct === 'boolean');
	}

	setTestVerified(testId): void {
		forkJoin([
			this.translate.get('modal.success'),
			this.translate.get('verify-test.succes-verified-msg'),
		]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
			});
		this.verifyService.setTestAsVerified(testId).subscribe(() => {
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.router.navigate(['../../../tests'], { relativeTo: this.activatedRoute })
		})
	}

	finalizeVerificationModal(): void {
		forkJoin([
			this.translate.get('modal.finish-verification'),
			this.translate.get('verify-test.finish-verification-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.setTestVerified(this.testId), () => { });
	}
}
