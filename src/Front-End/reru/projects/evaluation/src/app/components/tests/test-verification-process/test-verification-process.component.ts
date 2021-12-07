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

@Component({
	selector: 'app-test-verification-process',
	templateUrl: './test-verification-process.component.html',
	styleUrls: ['./test-verification-process.component.scss']
})
export class TestVerificationProcessComponent implements OnInit {
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
	autoverified: any;
	verified: any;
	pager: number[] = [];
	correctQuestion;
	isLoading: boolean = true;
	maxPoints: number;
	points: number;
	questionUnitId: number;

	constructor(
		private verifyService: TestVerificationProcessService,
		private modalService: NgbModal,
		private activatedRoute: ActivatedRoute,
		private testService: TestService,
		private notificationService: NotificationsService,
		private injector: Injector,
		private testQuestionService: TestQuestionService,
		private router: Router,
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
					this.correct = res.data.isCorrect;
					this.index = index;
					this.questionType = res.data.questionType;
					this.isLoading = false;
					this.maxPoints = res.data.questionMaxPoints;
					this.questionUnitId = res.data.questionUnitId;
					this.points = (res.data.evaluatorPoints === 0) ? '' : res.data.evaluatorPoints;
				}
			},
			(err) => {
				err.error.messages.some(x => {
					if (x.code === '03001609')
						this.router.navigate(['../../../tests'], { relativeTo: this.activatedRoute })
				})
			});
	}

	verifyTest(): void {
		if (this.correct === null) {
			this.notificationService.error('"Correct" or "Not Correct" value is not selected', null, NotificationUtil.getDefaultMidConfig());
		} else if (this.points < 1 && this.correct === true) {
			this.notificationService.error('Error', "If answer is correct, points must be greater than 0", NotificationUtil.getDefaultMidConfig());
		} else if (this.points > 0 && this.correct === false) {
			this.notificationService.error('Error', "If answer is false, set points to 0", NotificationUtil.getDefaultMidConfig());
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
		this.verifyService.setTestAsVerified(testId).subscribe(() => {
			this.notificationService.success('Success', 'Test was successfully verified', NotificationUtil.getDefaultMidConfig());
			this.router.navigate(['../../../tests'], { relativeTo: this.activatedRoute })
		})
	}

	finalizeVerificationModal(): void {
		const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = "Finish test verification"
		modalRef.componentInstance.description = "Are you sure you want to complete test verification?";
		modalRef.result.then(() => this.setTestVerified(this.testId), () => { });
	}
}
