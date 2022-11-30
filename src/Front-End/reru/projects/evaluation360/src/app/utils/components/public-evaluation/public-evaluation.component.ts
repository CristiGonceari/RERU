import { Component, Input, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { I18nService } from '@erp/shared';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { Evaluation } from '../../models/evaluation.model';
import { hasRequiredField, NotificationUtil } from '@utils';
import { EvaluationCounterSignModel } from '@utils';
import { EvaluationService } from '@utils/services';

@Component({
  selector: 'app-public-evaluation',
  templateUrl: './public-evaluation.component.html',
  styleUrls: ['./public-evaluation.component.scss']
})
export class PublicEvaluationComponent implements OnInit {
	@Input() action: number;
	@Input() evaluation: Evaluation;
	@Input() type: number;
	surveyForm: FormGroup;

	objectives: number[] = [1,2,3,4,5];

	criterias: string[] = [
		'Competență profesională',
		'Activism și spirit de inițiativă',
		'Eficiență',
		'Calitate a muncii',
		'Lucru în echipă',
		'Comunicare'
	];

	qualificatives: any = {
		unsatisfactory: 'Unsatisfactory',
		satisfactory: 'Satisfactory',
		good: 'Good',
		veryGood: 'Very good'
	}

	isFinalManual: boolean;

	constructor(
		private evaluationService: EvaluationService,
		private fb: FormBuilder,
		private notificationService: NotificationsService,
		private ngZone: NgZone,
		private route: ActivatedRoute,
		private router: Router,
		private translate: I18nService
	) {}

	ngOnInit(): void {
		this.translateData();
		this.translate.change.subscribe(() => this.translateData());
		this.initForm(this.evaluation);
		this.buildCriterias(this.type);
		// this.buildCriterias(this.evaluation.type);
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

	initForm(data: any = {}): void {
		this.surveyForm = this.fb.group({
			evaluationFromDate: this.fb.control({ value: data.evaluationFromDate, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'evaluationFromDate')),
			evaluationToDate: this.fb.control({ value: data.evaluationToDate, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'evaluationToDate')),
			specializationCoursesInternal: this.fb.control(
				{ value: data.specializationCoursesInternal, disabled: this.getDisabled(2) },
				this.setValidation(this.action, 'specializationCoursesInternal')
			),
			specializationCoursesInternational: this.fb.control(
				{ value: data.specializationCoursesInternational, disabled: this.getDisabled(2) },
				this.setValidation(this.action, 'specializationCoursesInternational')
			),
			specializationCoursesOthers: this.fb.control(
				{ value: data.specializationCoursesOthers, disabled: this.getDisabled(2) },
				this.setValidation(this.action, 'specializationCoursesOthers')
			),
			treeYearsAgoMark: this.fb.control({ value: data.treeYearsAgoMark, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'treeYearsAgoMark')),
			twoYearsAgoMark: this.fb.control({ value: data.twoYearsAgoMark, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'twoYearsAgoMark')),
			lastYearMark: this.fb.control({ value: data.lastYearMark, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'lastYearMark')),
			partialEvaluationFromDate: this.fb.control(
				{ value: data.partialEvaluationFromDate, disabled: this.getDisabled(2) },
				this.setValidation(this.action, 'partialEvaluationFromDate')
			),
			partialEvaluationToDate: this.fb.control(
				{ value: data.partialEvaluationToDate, disabled: this.getDisabled(2) },
				this.setValidation(this.action, 'partialEvaluationToDate')
			),
			partialEvaluationRate: this.fb.control({ value: data.partialEvaluationRate, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'partialEvaluationRate')),
			partialEvaluationMark: this.fb.control({ value: data.partialEvaluationMark, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'partialEvaluationMark')),

			objective1Name: this.fb.control({ value: data.objective1Name, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective1Name')),
			objective2Name: this.fb.control({ value: data.objective2Name, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective2Name')),
			objective3Name: this.fb.control({ value: data.objective3Name, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective3Name')),
			objective4Name: this.fb.control({ value: data.objective4Name, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective4Name')),
			objective5Name: this.fb.control({ value: data.objective5Name, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective5Name')),

			objective1Performance: this.fb.control({ value: data.objective1Performance, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective1Performance')),
			objective2Performance: this.fb.control({ value: data.objective2Performance, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective2Performance')),
			objective3Performance: this.fb.control({ value: data.objective3Performance, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective3Performance')),
			objective4Performance: this.fb.control({ value: data.objective4Performance, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective4Performance')),
			objective5Performance: this.fb.control({ value: data.objective5Performance, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective5Performance')),

			objective1Complete: this.fb.control({ value: data.objective1Complete, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective1Complete')),
			objective2Complete: this.fb.control({ value: data.objective2Complete, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective2Complete')),
			objective3Complete: this.fb.control({ value: data.objective3Complete, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective3Complete')),
			objective4Complete: this.fb.control({ value: data.objective4Complete, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective4Complete')),
			objective5Complete: this.fb.control({ value: data.objective5Complete, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective5Complete')),

			objective1Comments: this.fb.control({ value: data.objective1Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective1Comments')),
			objective2Comments: this.fb.control({ value: data.objective2Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective2Comments')),
			objective3Comments: this.fb.control({ value: data.objective3Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective3Comments')),
			objective4Comments: this.fb.control({ value: data.objective4Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective4Comments')),
			objective5Comments: this.fb.control({ value: data.objective5Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective5Comments')),

			objective1Mark: this.fb.control({ value: data.objective1Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective1Mark')),
			objective2Mark: this.fb.control({ value: data.objective2Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective2Mark')),
			objective3Mark: this.fb.control({ value: data.objective3Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective3Mark')),
			objective4Mark: this.fb.control({ value: data.objective4Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective4Mark')),
			objective5Mark: this.fb.control({ value: data.objective5Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'objective5Mark')),
			
			question1Mark: this.fb.control({ value: data.question1Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question1Mark')),
			question2Mark: this.fb.control({ value: data.question2Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question2Mark')),
			question3Mark: this.fb.control({ value: data.question3Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question3Mark')),
			question4Mark: this.fb.control({ value: data.question4Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question4Mark')),
			question5Mark: this.fb.control({ value: data.question5Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question5Mark')),
			question6Mark: this.fb.control({ value: data.question6Mark, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question6Mark')),
			
			question1Comments: this.fb.control({ value: data.question1Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question1Comments')),
			question2Comments: this.fb.control({ value: data.question2Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question2Comments')),
			question3Comments: this.fb.control({ value: data.question3Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question3Comments')),
			question4Comments: this.fb.control({ value: data.question4Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question4Comments')),
			question5Comments: this.fb.control({ value: data.question5Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question5Comments')),
			question6Comments: this.fb.control({ value: data.question6Comments, disabled: this.getDisabled(1)}, this.setValidation(this.action, 'question6Comments')),

			interviewDate: this.fb.control({ value: data.interviewDate, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'interviewDate')),
			comments: this.fb.control({ value: data.comments, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'comments')),

			needImprovement1: this.fb.control({ value: data.needImprovement1, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'needImprovement1')),
			needImprovement2: this.fb.control({ value: data.needImprovement2, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'needImprovement2')),
			needImprovement3: this.fb.control({ value: data.needImprovement3, disabled: this.getDisabled(1) }, this.setValidation(this.action, 'needImprovement3')),
			finalMark: this.fb.control({ value: data.finalMark, disabled: this.getDisabled(3) }, this.setValidation(this.action, 'finalMark')),
			evaluatedComments: this.fb.control({ value: data.evaluatedComments, disabled: this.getDisabled(2) }, this.setValidation(this.action, 'evaluatedComments')),
			counterSingerComments: this.fb.control({ value: data.counterSingerComments, disabled: this.getDisabled(3) }, this.setValidation(this.action, 'counterSingerComments')),
		});
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
		const mark = this.calculateMedias();
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

	isRequired(field: string): boolean {
		return hasRequiredField(this.surveyForm.get(field));
	}

	isEditable(field: string): boolean {
		return !this.surveyForm.get(field).disabled;
	}

	setValidation(action: number, field: string) {
		switch(+action) {
			case 1:
				return [
					'evaluationFromDate', 
					'evaluationToDate'
				].includes(field) ? [Validators.required] : [
					'question1Mark',
					'question2Mark',
					'question3Mark',
					'question4Mark',
					'question5Mark',
					'question6Mark'
				].includes(field) ? [Validators.required, Validators.min(0.1)] : [];
			case 2: return [
					'specializationCoursesInternal',
					'specializationCoursesInternational',
					'specializationCoursesOthers'
			].includes(field) ? [Validators.required] : [];
			default: return [];
		}
	}

	areAllMarksCompleted(): boolean {
		const marks = [];
		for (let i = 1; i <= this.objectives.length; i++) {
			if (!isNaN(this.surveyForm.get(`objective${i}Mark`).value)) {
				marks.push(true);
			} else {
				marks.push(false)
			}
		}

		for (let i = 1; i <= this.criterias.length; i++) {
			if (!isNaN(this.surveyForm.get(`question${i}Mark`).value)) {
				marks.push(true);
			} else {
				marks.push(false)
			}
		}

		return marks.every(el => !!el);
	}


	getDisabled(enableOnAction: number) {
		return enableOnAction != this.action;
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
	}

	counterSignEvaluation(evaluatedAcceptance: number) {
		var dto = this.getCounterSign(this.surveyForm.value);
		dto.counterSignerAcceptance = evaluatedAcceptance;
		this.counterSign(dto);
	}

	counterSign(dto: EvaluationCounterSignModel) {
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
		const dto: Evaluation = <Evaluation>{};
		return dto;
	}

	getAutoEvaluate(data) {
		const dto:any  = {};
		dto.evaluatedComments = data['evaluatedComments'];

		return dto;
	}

	getCounterSign(data) {
		const dto: any = {};
		dto.finalMark = !isNaN(data['finalMark']) && data['finalMark'] != null ? +data['finalMark'] : null;
		dto.counterSingerComments = data['counterSingerComments'];

		return dto;
	}

	navigateToList() {
		this.ngZone.run(() => this.router.navigate(['../../'], { relativeTo: this.route }));
	}

	translateData(): void {
		forkJoin([
			this.translate.get('evaluations.qualificatives.qualificative-1'),
			this.translate.get('evaluations.qualificatives.qualificative-2'),
			this.translate.get('evaluations.qualificatives.qualificative-3'),
			this.translate.get('evaluations.qualificatives.qualificative-4'),
		]).subscribe(([veryGood, good, satisfactory, unsatisfactory]) => {
			this.qualificatives = { veryGood, good, satisfactory, unsatisfactory };
		})
	}

	calculateObjectiveMedia(): number {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/objective\dMark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.reduce((a,b) => a + b, 0) / values.length) : 0;
	}

	calculateQuestionMedia(): number {
		const survey = {...this.surveyForm.getRawValue()};
		const values = [];
		for (const prop in survey) {
			if (prop.match(/question\dMark/) && !isNaN(survey[prop])) {
				values.push(+survey[prop]);
			}
		}

		return values.length ? (values.reduce((a,b) => a + b, 0) / values.length) : 0;
	}

	calculateMedias(): number {
		if (this.calculateObjectiveMedia() === 0) {
			return this.calculateQuestionMedia();
		}

		return (this.calculateObjectiveMedia() + this.calculateQuestionMedia()) / 2;
	}

	buildCriterias(type: number): void {
		if (type === 0) {
			this.criterias = [
				'Competență profesională',
				'Activism și spirit de inițiativă',
				'Eficiență',
				'Calitate a muncii',
				'Lucru în echipă',
				'Comunicare'
			];
		}

		if (type === 1) {
			this.criterias = [
				'Competență managerială',
				'Competență profesională',
				'Activism și spirit de inițiativă',
				'Eficiență',
				'Creativitate',
				'Comunicare și reprezentare'
			]
		}
	}
}
