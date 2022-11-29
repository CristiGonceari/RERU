import { Component, Input, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { I18nService } from '@erp/shared';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { AutoEvaluateDto } from '../../models/auto-evaluate-dto';
import { CounterSignDto } from '../../models/counter-sign-dto';
import { EvaluateDto } from '../../models/evaluate-dto';
import { Evaluation } from '../../models/evaluation';
import { NotificationUtil } from '../../util/notification.util';
import { EvaluationService } from '../../services/survey.service';
import { hasRequiredField } from '../../util/has-required-field.util';

@Component({
	selector: 'app-evaluation',
	templateUrl: './evaluation.component.html',
	styleUrls: ['./evaluation.component.scss'],
})
export class EvaluationComponent implements OnInit {
	@Input() action: number;
	@Input() evaluation: Evaluation;
	surveyForm: FormGroup;

	questions: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
	radios: number[] = [1, 2, 3, 4];
	criterias = [];

	qualificatives = {
		unsatisfactory: 'Unsatisfactory',
		satisfactory: 'Satisfactory',
		good: 'Good',
		veryGood: 'Very good'
	}

	isFinalManual: boolean;

	constructor(private evaluationService: EvaluationService,
				private fb: FormBuilder,
				private notificationService: NotificationsService,
				private ngZone: NgZone,
				private route: ActivatedRoute,
				private router: Router,
				private translate: I18nService) {}

	ngOnInit(): void {
		this.translateData();
		this.translate.change.subscribe(() => this.translateData());
		this.initForm(this.evaluation);
		this.buildCriterias(this.evaluation && this.evaluation.type);
		this.initMarkEvaluation();
	}

	initMarkEvaluation(): void {
		if ([1,2].includes(this.action)) {
			this.subscribeForMarkChanges();
			this.subscribeForFinalMarkChange();
		} else {
			this.updateFinalMarkView();
		}
	}

	isRequired(field: string): boolean {
		return hasRequiredField(this.surveyForm.get(field));
	}

	isEditable(field: string): boolean {
		return !this.surveyForm.get(field).disabled;
	}

	initForm(data: any = {}): void {
		this.surveyForm = this.fb.group({
			evaluationFromDate: this.fb.control({ value: data.evaluationFromDate, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'evaluationFromDate')),
			evaluationToDate: this.fb.control({ value: data.evaluationToDate, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'evaluationToDate')),
			specializationCoursesInternal: this.fb.control(
				{ value: data.specializationCoursesInternal, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'specializationCoursesInternal')
			),
			specializationCoursesInternational: this.fb.control(
				{ value: data.specializationCoursesInternational, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'specializationCoursesInternational')
			),
			specializationCoursesOthers: this.fb.control(
				{ value: data.specializationCoursesOthers, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'specializationCoursesOthers')
			),
			degree: this.fb.control({ value: data.degree, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'degree')),
			lastQualifyCategory: this.fb.control({ value: data.lastQualifyCategory, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'lastQualifyCategory')),
			lastQualifyCategoryOrderSeries: this.fb.control(
				{ value: data.lastQualifyCategoryOrderSeries, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'lastQualifyCategoryOrderSeries')
			),
			lastQualifyCategoryOrderNumber: this.fb.control(
				{ value: data.lastQualifyCategoryOrderNumber, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'lastQualifyCategoryOrderNumber')
			),
			lastQualifyCategoryOrderDate: this.fb.control(
				{ value: data.lastQualifyCategoryOrderDate, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'lastQualifyCategoryOrderDate')
			),
			treeYearsAgoMark: this.fb.control({ value: data.treeYearsAgoMark, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'treeYearsAgoMark')),
			twoYearsAgoMark: this.fb.control({ value: data.twoYearsAgoMark, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'twoYearsAgoMark')),
			lastYearMark: this.fb.control({ value: data.lastYearMark, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'lastYearMark')),
			partialEvaluationFromDate: this.fb.control(
				{ value: data.partialEvaluationFromDate, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'partialEvaluationFromDate')
			),
			partialEvaluationToDate: this.fb.control(
				{ value: data.partialEvaluationToDate, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'partialEvaluationToDate')
			),
			partialEvaluationRate: this.fb.control({ value: data.partialEvaluationRate, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'partialEvaluationRate')),
			partialEvaluationMark: this.fb.control({ value: data.partialEvaluationMark, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'partialEvaluationMark')),
			sanctionStartDate: this.fb.control({ value: data.sanctionStartDate, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'sanctionStartDate')),
			sanctionEndDate: this.fb.control({ value: data.sanctionEndDate, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'sanctionEndDate')),
			question1SelfEvaluationMark: this.fb.control(
				{ value: data.question1SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question1SelfEvaluationMark')
			),
			question1Mark: this.fb.control({ value: data.question1Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question1Mark')),
			question2SelfEvaluationMark: this.fb.control(
				{ value: data.question2SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question2SelfEvaluationMark')
			),
			question2Mark: this.fb.control({ value: data.question2Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question2Mark')),
			question3SelfEvaluationMark: this.fb.control(
				{ value: data.question3SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question3SelfEvaluationMark')
			),
			question3Mark: this.fb.control({ value: data.question3Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question3Mark')),
			question4SelfEvaluationMark: this.fb.control(
				{ value: data.question4SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question4SelfEvaluationMark')
			),
			question4Mark: this.fb.control({ value: data.question4Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question4Mark')),
			question5SelfEvaluationMark: this.fb.control(
				{ value: data.question5SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question5SelfEvaluationMark')
			),
			question5Mark: this.fb.control({ value: data.question5Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question5Mark')),
			question6SelfEvaluationMark: this.fb.control(
				{ value: data.question6SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question6SelfEvaluationMark')
			),
			question6Mark: this.fb.control({ value: data.question6Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question6Mark')),
			question7SelfEvaluationMark: this.fb.control(
				{ value: data.question7SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question7SelfEvaluationMark')
			),
			question7Mark: this.fb.control({ value: data.question7Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question7Mark')),
			question8SelfEvaluationMark: this.fb.control(
				{ value: data.question8SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question8SelfEvaluationMark')
			),
			question8Mark: this.fb.control({ value: data.question8Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question8Mark')),
			question9SelfEvaluationMark: this.fb.control(
				{ value: data.question9SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question9SelfEvaluationMark')
			),
			question9Mark: this.fb.control({ value: data.question9Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question9Mark')),
			question10SelfEvaluationMark: this.fb.control(
				{ value: data.question10SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question10SelfEvaluationMark')
			),
			question10Mark: this.fb.control({ value: data.question10Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question10Mark')),
			question11SelfEvaluationMark: this.fb.control(
				{ value: data.question11SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question11SelfEvaluationMark')
			),
			question11Mark: this.fb.control({ value: data.question11Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question11Mark')),
			question12SelfEvaluationMark: this.fb.control(
				{ value: data.question12SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question12SelfEvaluationMark')
			),
			question12Mark: this.fb.control({ value: data.question12Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question12Mark')),
			question13SelfEvaluationMark: this.fb.control(
				{ value: data.question13SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question13SelfEvaluationMark')
			),
			question13Mark: this.fb.control({ value: data.question13Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question13Mark')),
			question14SelfEvaluationMark: this.fb.control(
				{ value: data.question14SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question14SelfEvaluationMark')
			),
			question14Mark: this.fb.control({ value: data.question14Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question14Mark')),
			question15SelfEvaluationMark: this.fb.control(
				{ value: data.question15SelfEvaluationMark, disabled: this.getDisabled([2]) },
				this.setValidation(this.action, 'question15SelfEvaluationMark')
			),
			question15Mark: this.fb.control({ value: data.question15Mark, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'question15Mark')),
			interviewDate: this.fb.control({ value: data.interviewDate, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'interviewDate')),
			comments: this.fb.control({ value: data.comments, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'comments')),
			individualObjective1: this.fb.control({ value: data.individualObjective1, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'individualObjective1')),
			individualObjective2: this.fb.control({ value: data.individualObjective2, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'individualObjective2')),
			individualObjective3: this.fb.control({ value: data.individualObjective3, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'individualObjective3')),
			needImprovement1: this.fb.control({ value: data.needImprovement1, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'needImprovement1')),
			needImprovement2: this.fb.control({ value: data.needImprovement2, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'needImprovement2')),
			needImprovement3: this.fb.control({ value: data.needImprovement3, disabled: this.getDisabled([1]) }, this.setValidation(this.action, 'needImprovement3')),
			finalMark: this.fb.control({ value: data.finalMark, disabled: this.getDisabled([3]) }, this.setValidation(this.action, 'finalMark')),
			evaluatedComments: this.fb.control({ value: data.evaluatedComments, disabled: this.getDisabled([2]) }, this.setValidation(this.action, 'evaluatedComments')),
			counterSingerComments: this.fb.control({ value: data.counterSingerComments, disabled: this.getDisabled([3]) }, this.setValidation(this.action, 'counterSingerComments')),
		});
	}

	setValidation(action: number, field: string) {
		switch(+action) {
			case 1:
				return [
					'evaluationFromDate',
					'evaluationToDate',
				].includes(field) ? [Validators.required] : [
					'question1Mark',
					'question2Mark',
					'question3Mark',
					'question4Mark',
					'question5Mark',
					'question6Mark',
					'question7Mark',
					'question8Mark',
					'question9Mark',
					'question10Mark',
					'question11Mark',
					'question12Mark',
					'question13Mark',
					'question14Mark',
					'question15Mark',
				].includes(field) ? [Validators.required, Validators.min(0.1)] : [];
			case 2: return [
					'specializationCoursesInternal',
					'specializationCoursesInternational',
					'specializationCoursesOthers',
					'degree',
					'lastQualifyCategory',
					'lastQualifyCategoryOrderSeries',
					'lastQualifyCategoryOrderNumber',
					'lastQualifyCategoryOrderDate',
					'treeYearsAgoMark',
					'twoYearsAgoMark',
					'lastYearMark'
					].includes(field) ? [Validators.required] : [
					'question1SelfEvaluationMark',
					'question2SelfEvaluationMark',
					'question3SelfEvaluationMark',
					'question4SelfEvaluationMark',
					'question5SelfEvaluationMark',
					'question6SelfEvaluationMark',
					'question7SelfEvaluationMark',
					'question8SelfEvaluationMark',
					'question9SelfEvaluationMark',
					'question10SelfEvaluationMark',
					'question11SelfEvaluationMark',
					'question12SelfEvaluationMark',
					'question13SelfEvaluationMark',
					'question14SelfEvaluationMark',
					'question15SelfEvaluationMark'
					].includes(field) ? [Validators.required, Validators.min(0.1)] : [];
			case 3: return [
					'finalMark'
					].includes(field) ? [Validators.required, Validators.min(0.1)] : [];
			default: return [];
		}
	}

	subscribeForFinalMarkChange(): void {
		this.surveyForm.get('finalMark').valueChanges.subscribe(response => {
			this.isFinalManual = true;
		})
	}

	subscribeForMarkChanges(): void {
		this.surveyForm.valueChanges.subscribe(() => {
			if (this.isFinalManual) {
				this.isFinalManual = false;
				return;
			}

			this.updateFinalMarkView();
		});
	}

	updateFinalMarkView(): void {
		const mark = +this.calculateFinalMark();
		if (this.areAllMarksCompleted()) {
			if (mark >= 1 && mark <= 1.50) {
				this.surveyForm.get('finalMark').patchValue('4', { emitEvent: false });
			}
			if (mark >= 1.51 && mark <= 2.50) {
				this.surveyForm.get('finalMark').patchValue('3', { emitEvent: false });
			}
			if (mark >= 2.51 && mark <= 3.50) {
				this.surveyForm.get('finalMark').patchValue('2', { emitEvent: false });
			}
			if (mark >= 3.51 && mark <= 4.00) {
				this.surveyForm.get('finalMark').patchValue('1', { emitEvent: false });
			}
		}
	}

	areAllMarksCompleted(): boolean {
		const marks = [];
		for (let i = 1; i <= 15; i++) {
			if (!isNaN(this.surveyForm.get(`question${i}Mark`).value)) {
				marks.push(true);
			} else {
				marks.push(false)
			}
		}

		return marks.every(el => !!el);
	}

	getDisabled(enableOnActions: number[]) {
    return !enableOnActions.includes(this.action);
	}

	submit(accept: boolean) {
		this.evaluate(accept);
	}

	evaluate(accept: boolean) {
		this.evaluationService.evaluate(this.evaluation.id, this.getEvaluate(this.surveyForm.value, accept)).subscribe(
			response => {
				this.notificationService.success(
					'Succes',
					'Fisa a fost transmisa cu succes!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.navigateToList();
			},
			error => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	acceptEvaluation(evaluatedAcceptance: number) {
		var dto = this.getAutoEvaluate(this.surveyForm.value);
    	dto.evaluatedAcceptance = evaluatedAcceptance;
		if (evaluatedAcceptance === 1 || evaluatedAcceptance === 2 ) {
			dto.accept = true;
		}
		this.autoEvaluate(dto);
	}

	autoEvaluate(dto: AutoEvaluateDto) {
		this.evaluationService.autoevaluate(this.evaluation.id, dto).subscribe(
			response => {
				this.notificationService.success(
					'Succes',
					'Fisa a fost transmisa cu succes!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.navigateToList();
			},
			error => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	counterSignEvaluation(evaluatedAcceptance: number) {
		var dto = this.getCounterSign(this.surveyForm.value);
		dto.counterSignerAcceptance = evaluatedAcceptance;
		this.counterSign(dto);
	}

	counterSign(dto: CounterSignDto) {
		this.evaluationService.counterSign(this.evaluation.id, dto).subscribe(
			response => {
				this.notificationService.success(
					'Succes',
					'Fisa a fost transmisa cu succes!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.navigateToList();
			},
			error => {
				if (error.status === 400) {
					this.notificationService.warn('Warning', 'Validation error occured!', NotificationUtil.getDefaultMidConfig());
					return;
				}

				this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	getEvaluate(data, accept: boolean) {
    	const dto = new EvaluateDto();
    	dto.accept = accept;
		dto.comments = data['comments'];

		dto.evaluationFromDate = data['evaluationFromDate'];
		dto.evaluationToDate = data['evaluationToDate'];

		dto.interviewDate = data['interviewDate'];

		dto.needImprovement1 = data['needImprovement1'];
		dto.needImprovement2 = data['needImprovement2'];
		dto.needImprovement3 = data['needImprovement3'];

		dto.question1Mark = data['question1Mark'];
		dto.question2Mark = data['question2Mark'];
		dto.question3Mark = data['question3Mark'];
		dto.question4Mark = data['question4Mark'];
		dto.question5Mark = data['question5Mark'];
		dto.question6Mark = data['question6Mark'];
		dto.question7Mark = data['question7Mark'];
		dto.question8Mark = data['question8Mark'];
		dto.question9Mark = data['question9Mark'];
		dto.question10Mark = data['question10Mark'];
		dto.question11Mark = data['question11Mark'];
		dto.question12Mark = data['question12Mark'];
		dto.question13Mark = data['question13Mark'];
		dto.question14Mark = data['question14Mark'];
		dto.question15Mark = data['question15Mark'];

		return dto;
	}

	getAutoEvaluate(data) {
    	const dto = new AutoEvaluateDto();
		dto.individualObjective1 = data['individualObjective1'];
		dto.individualObjective2 = data['individualObjective2'];
    	dto.individualObjective3 = data['individualObjective3'];

    	dto.lastQualifyCategory = data['lastQualifyCategory'];
		dto.lastQualifyCategoryOrderDate = data['lastQualifyCategoryOrderDate'];
		dto.lastQualifyCategoryOrderNumber = data['lastQualifyCategoryOrderNumber'];
		dto.lastQualifyCategoryOrderSeries = data['lastQualifyCategoryOrderSeries'];
    	dto.lastYearMark = !isNaN(data['lastYearMark']) && data['lastYearMark'] != null ? +data['lastYearMark'] : null;
    	dto.degree = !isNaN(data['degree']) && data['degree'] != null ? +data['degree'] : null;

    	dto.partialEvaluationFromDate = data['partialEvaluationFromDate'];
		dto.partialEvaluationMark = !isNaN(data['partialEvaluationMark']) && data['partialEvaluationMark'] != null ? +data['partialEvaluationMark'] : null;
		dto.partialEvaluationRate = data['partialEvaluationRate'];
    	dto.partialEvaluationToDate = data['partialEvaluationToDate'];

    	dto.sanctionEndDate = data['sanctionEndDate'];
		dto.sanctionStartDate = data['sanctionStartDate'];
		dto.specializationCoursesInternal = data['specializationCoursesInternal'];
		dto.specializationCoursesInternational = data['specializationCoursesInternational'];
		dto.specializationCoursesOthers = data['specializationCoursesOthers'];
		dto.treeYearsAgoMark =
			!isNaN(data['treeYearsAgoMark']) && data['treeYearsAgoMark'] != null ? +data['treeYearsAgoMark'] : null;
		dto.twoYearsAgoMark =
			!isNaN(data['twoYearsAgoMark']) && data['twoYearsAgoMark'] != null ? +data['twoYearsAgoMark'] : null;

		dto.question1SelfEvaluationMark = data['question1SelfEvaluationMark'];
		dto.question2SelfEvaluationMark = data['question2SelfEvaluationMark'];
		dto.question3SelfEvaluationMark = data['question3SelfEvaluationMark'];
		dto.question4SelfEvaluationMark = data['question4SelfEvaluationMark'];
		dto.question5SelfEvaluationMark = data['question5SelfEvaluationMark'];
		dto.question6SelfEvaluationMark = data['question6SelfEvaluationMark'];
		dto.question7SelfEvaluationMark = data['question7SelfEvaluationMark'];
		dto.question8SelfEvaluationMark = data['question8SelfEvaluationMark'];
		dto.question9SelfEvaluationMark = data['question9SelfEvaluationMark'];
		dto.question10SelfEvaluationMark = data['question10SelfEvaluationMark'];
		dto.question11SelfEvaluationMark = data['question11SelfEvaluationMark'];
		dto.question12SelfEvaluationMark = data['question12SelfEvaluationMark'];
		dto.question13SelfEvaluationMark = data['question13SelfEvaluationMark'];
		dto.question14SelfEvaluationMark = data['question14SelfEvaluationMark'];
		dto.question15SelfEvaluationMark = data['question15SelfEvaluationMark'];
		dto.evaluatedComments = data['evaluatedComments'];
		// return parsed;
		return dto;
	}

	getCounterSign(data) {
    	const dto = new CounterSignDto();
    	dto.finalMark = !isNaN(data['finalMark']) && data['finalMark'] != null ? +data['finalMark'] : null;
		dto.counterSingerComments = data['counterSingerComments'];
		return dto;
	}

	navigateToList() {
		this.ngZone.run(() => this.router.navigate(['../../'], { relativeTo: this.route }));
	}

	buildSelfEvaluationFormControl(i: number): string {
		return i === 16 ?  'question15SelfEvaluationMark' :
			   i > 10 ?    'question' + (i) + 'SelfEvaluationMark':
			   			   'question' + (i + 1) + 'SelfEvaluationMark';
	}

	buildEvaluationFormControl(i: number): string {
		return i === 16 ?  'question15Mark' :
		       i > 10 ?    'question' + (i) + 'Mark':
				  		   'question' + (i + 1) + 'Mark';
	}

	calculateAutoEvaluate(): string {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/question\d\d?SelfEvaluationMark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.slice(0, 10).reduce((a,b) => a + b, 0) / 10).toString() : '0';
	}

	calculateEvaluator(): string {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/question\d\d?Mark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.slice(0, 10).reduce((a,b) => a + b, 0) / 10).toString() : '0';
	}

	calculateMediaAutoEvaluate(): string {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/question\d\d?SelfEvaluationMark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.slice(10, values.length - 1).reduce((a,b) => a + b, 0) / 4).toString() : '0';
	}

	calculateMediaEvaluator(): string {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/question\d\d?Mark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.slice(10, values.length - 1).reduce((a,b) => a + b, 0) / 4).toString() : '0';
	}

	calculateSelfFinalMark(): string {
		return ((+this.calculateAutoEvaluate() + +this.calculateMediaAutoEvaluate() + +this.surveyForm.get('question15SelfEvaluationMark').value) / 3).toFixed(2);
	}

	calculateFinalMark(): string {
		if (!this.areAllMarksCompleted()) {
			return '0'
		}

		return ((+this.calculateEvaluator() + +this.calculateMediaEvaluator() + +this.surveyForm.get('question15Mark').value) / 3).toFixed(2);
	}

	getSelfFinalSuggestionMark(): string {
		const  mark = +this.calculateSelfFinalMark();
		return mark >= 1 && mark <= 1.50 ? this.qualificatives.unsatisfactory :
			   mark >= 1.51 && mark <= 2.50 ? this.qualificatives.satisfactory :
			   mark >= 2.51 && mark <= 3.50 ? this.qualificatives.good :
			   mark >= 3.51 && mark <= 4.00 ? this.qualificatives.veryGood : '-';
	}

	getFinalSuggestionMark(): string {
		const  mark = +this.calculateFinalMark();
		return mark >= 1 && mark <= 1.50 ? this.qualificatives.unsatisfactory :
			   mark >= 1.51 && mark <= 2.50 ? this.qualificatives.satisfactory :
			   mark >= 2.51 && mark <= 3.50 ? this.qualificatives.good :
			   mark >= 3.51 && mark <= 4.00 ? this.qualificatives.veryGood : '-';
	}

	translateData(): void {
		forkJoin([
			this.translate.get('survey.qualificatives.qualificative-1'),
			this.translate.get('survey.qualificatives.qualificative-2'),
			this.translate.get('survey.qualificatives.qualificative-3'),
			this.translate.get('survey.qualificatives.qualificative-4'),
		]).subscribe(([veryGood, good, satisfactory, unsatisfactory]) => {
			this.qualificatives = { veryGood, good, satisfactory, unsatisfactory };
		})
	}

	buildCriterias(type: number): void {
		if (type === 0) {
			this.criterias = [
				'Capacitatea de a planifica, organiza, coordona, monitoriza și evalua activitatea subdiviziunii conduse',
				'Capacitatea de a gestiona eficient activitatea personalului prin repartizarea în mod echilibrat a sarcinilor de serviciu',
				'Capacitatea de a lua decizii în mod operativ, de a-și asuma riscurile și responsabilitatea pentru deciziile',
				'Menținerea unui climat optim de muncă a subordonaților',
				'Capacitatea de a-și îndeplini atribuțiile cu exactitate complet și calitativ',
				'Capacitatea de a gestiona, utiliza, întocmi și aplica documentele de serviciu',
				'Nivel de realizare a sarcinilor de serviciu și obiectivelor individuale',
				'Executarea ordinelor și dispozițiilor conducătorilor, operativitatea în realizarea misiunilor',
				'Respectarea eticii profesionale (comportamentul cu șefii, subordonații, cetățenii)',
				'Abateri disciplinare (se va puncta: avertisment/observație: -:; alte sancțiuni - 1; lipsă - 3 sau 4, după caz)',
				'Punctajul stabilit a compartimentului I "Activitatea profesională"',
				'Cunoștințe la pregătirea generala',
				'Cunoștinte la pregătirea de specialitate',
				'Instrucția tragerii (TS - dupa caz)',
				'Intervenția profesională (normative de luptă, SPIGF)',
				'Media aritmetică de la criteriile 11-14',
				'Pregatirea fizică: (PF)',
				'Punctajul final stabilit: Mf = M1 + M2 + Pf'
			];
		}

		if (type === 1) {
			this.criterias = [
				'Capacitatea de a-și organiza și planifica activitatea',
				'Capacitatea de a-și exercita atribuțiile cu exactitate complet și calitativ',
				'Capacitatea de a gestiona, utiliza, întocmi și aplica documentele de serviciu',
				'Capacitatea de comunicare și lucru în colectiv',
				'Atitudinea față de muncă, angajarea la eforturi și sarcini suplimentare',
				'Utilizarea tehnicii și tehnologiilor computerizate pentru eficientizarea activității',
				'Nivelul de realizare a sarcinilor de serviciu și obiectivelor individuale',
				'Executarea ordinelor și dispozițiilor conducătorilor, operativitatea în realizarea misiunilor',
				'Respectarea eticii profesionale (comportamentul cu șefii, colegii, cetățenii)',
				'Abateri disciplinare',
				'Punctajul stabilit a compartimentului I "Activitatea profesională"',
				'Cunoștințe la pregătirea generală',
				'Cunostințe la pregătirea de spcialitate',
				'Instrucția tragerii (TS - dupa caz)',
				'Intervenția profesională (normative de lupta, SPIGF)',
				'Media aritmetică de la criteriile 11-14',
				'Pregatirea fizică: (PF)',
				'Punctajul final stabilit: Mf = M1 + M2 + Pf'
			]
		}
	}
}
