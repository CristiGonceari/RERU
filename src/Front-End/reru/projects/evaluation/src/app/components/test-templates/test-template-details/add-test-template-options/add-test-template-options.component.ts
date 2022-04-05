import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { Location } from '@angular/common';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { TestTemplateSettings } from 'projects/evaluation/src/app/utils/models/test-templates/test-template-settings.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';

@Component({
	selector: 'app-add-test-template-options',
	templateUrl: './add-test-template-options.component.html',
	styleUrls: ['./add-test-template-options.component.scss']
})
export class AddTestTemplateOptionsComponent implements OnInit {
	settingsForm: FormGroup;
	testId: number;
	testName;
	status;
	settingTestTemplate;
	isLoading: boolean = true;
	disable: boolean = false;
	show: boolean = false;
	mode;
	showManyQuestionPerPage;
	possibleChangeAnswer;
	possibleGetToSkipped;
	hidePagination;
	setMaxErrors: boolean = false;

	title: string;
	description: string;
	scoreFormulas: SelectItem[] = [{ label: "", value: "" }];
	showNegativeInputOneAnswer: boolean;
	showNegativeInputMultipleAnswers: boolean;

	constructor(
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private location: Location,
		private testTemplateService: TestTemplateService,
		private notificationService: NotificationsService,
		private formBuilder: FormBuilder,
		private referenceService: ReferenceService
	) { }

	ngOnInit(): void {
		this.settingsForm = new FormGroup({
			testTemplateId: new FormControl(),
			startWithoutConfirmation: new FormControl(),
			startBeforeProgrammation: new FormControl(),
			startAfterProgrammation: new FormControl(),
			possibleGetToSkipped: new FormControl(),
			possibleChangeAnswer: new FormControl(),
			canViewResultWithoutVerification: new FormControl(),
			canViewPollProgress: new FormControl(),
			hidePagination: new FormControl(),
			showManyQuestionPerPage: new FormControl(),
			questionsCountPerPage: new FormControl(),
			maxErrors: new FormControl(),
			formulaForOneAnswer: new FormControl(),
			negativeScoreForOneAnswer: new FormControl(),
			formulaForMultipleAnswers: new FormControl(),
			negativeScoreForMultipleAnswers: new FormControl()
		});

		this.initData();
		this.getScoreFormulas();
	}

	getScoreFormulas() {
		this.referenceService.getScoreFormula().subscribe(res => this.scoreFormulas = res.data);
	}

	selectScoreFormulaOneAnswer(event) {
		if (event == 1 || event == 2) {
			this.showNegativeInputOneAnswer = true;
			if (this.settingsForm.value.negativeScoreForOneAnswer == null) {
				this.settingsForm.value.negativeScoreForOneAnswer = false;
			}
		} else {
			this.showNegativeInputOneAnswer = false;
			this.settingsForm.value.negativeScoreForOneAnswer = null;
		}
	}

	selectScoreFormulaMultipleAnswers(event) {
		if (event == 1 || event == 2) {
			this.showNegativeInputMultipleAnswers = true;
			if (this.settingsForm.value.negativeScoreForMultipleAnswers == null) {
				this.settingsForm.value.negativeScoreForMultipleAnswers = false;
			}
		} else {
			this.showNegativeInputMultipleAnswers = false;
			this.settingsForm.value.negativeScoreForMultipleAnswers = null;
		}
	}

	initData(): void {
		this.activatedRoute.parent.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.testId = response.id;
				this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testId }).subscribe(res => {
					this.get();
					this.selectScoreFormulaOneAnswer(res.data.formulaForOneAnswer);
					this.selectScoreFormulaMultipleAnswers(res.data.formulaForMultipleAnswers);
					this.initForm(res.data);
				})
			}
		})
	}

	get() {
		this.testTemplateService.getTestTemplate(this.testId).subscribe(res => {
			if (res && res.data) {
				this.isLoading = false;
				this.status = res.data.status;
				this.mode = res.data.mode;
				if (this.status == TestTemplateStatusEnum.Active || this.status == TestTemplateStatusEnum.Canceled) {
					this.disable = true;
				}
			}
		})
	}

	initForm(test?: any): void {
		if (test) {
			this.settingsForm = this.formBuilder.group({
				testTemplateId: this.testId,
				startWithoutConfirmation: this.formBuilder.control((test && test.startWithoutConfirmation) || false),
				startBeforeProgrammation: this.formBuilder.control((test && test.startBeforeProgrammation) || false),
				startAfterProgrammation: this.formBuilder.control((test && test.startAfterProgrammation) || false),
				possibleGetToSkipped: this.formBuilder.control((test && test.possibleGetToSkipped) || false),
				possibleChangeAnswer: this.formBuilder.control((test && test.possibleChangeAnswer) || false),
				canViewResultWithoutVerification: this.formBuilder.control((test && test.canViewResultWithoutVerification) || false),
				canViewPollProgress: this.formBuilder.control((test && test.canViewPollProgress) || false),
				hidePagination: this.formBuilder.control((test && test.hidePagination) || false),
				showManyQuestionPerPage: this.formBuilder.control((test && test.showManyQuestionPerPage) || false),
				questionsCountPerPage: this.formBuilder.control((test && test.questionsCountPerPage) || null),
				maxErrors: this.formBuilder.control((test && test.maxErrors) || null),
				formulaForOneAnswer: this.formBuilder.control(test && test.formulaForOneAnswer),
				negativeScoreForOneAnswer: this.formBuilder.control((test && test.negativeScoreForOneAnswer) || null),
				formulaForMultipleAnswers: this.formBuilder.control(test && test.formulaForMultipleAnswers),
				negativeScoreForMultipleAnswers: this.formBuilder.control((test && test.negativeScoreForMultipleAnswers) || null)
			});

			this.possibleChangeAnswer = this.settingsForm.value.possibleChangeAnswer;
			this.possibleGetToSkipped = this.settingsForm.value.possibleGetToSkipped;
			this.hidePagination = this.settingsForm.value.hidePagination;
			this.showManyQuestionPerPage = this.settingsForm.value.showManyQuestionPerPage;
			if (this.settingsForm.value.maxErrors) this.setMaxErrors = true;
		}
	}

	showManyQuestionPerPageChange(value) {
		this.showManyQuestionPerPage = value;
	}

	possibleChangeAnswerChange(value) {
		this.possibleChangeAnswer = value;
	}

	possibleGetToSkippedChange(value) {
		this.possibleGetToSkipped = value;
	}

	hidePaginationChange(value) {
		this.hidePagination = value;
	}

	getTestTemplateSettings() {
		this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testId }).subscribe(
			res => {
				if (res && res.data) {
					this.settingTestTemplate = res.data;
					this.isLoading = false;
				}
				if (res.data == null) this.settingTestTemplate = new TestTemplateSettings();
			}
		);
	}

	backClicked() {
		this.location.back();
	}

	updateOptions() {
		this.selectScoreFormulaOneAnswer(this.settingsForm.value.formulaForOneAnswer);
		this.selectScoreFormulaMultipleAnswers(this.settingsForm.value.formulaForMultipleAnswers);

		this.testTemplateService.addEditTestTemplateSettings({ data: this.settingsForm.value }).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-update-settings-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.getTestTemplateSettings();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}
}
