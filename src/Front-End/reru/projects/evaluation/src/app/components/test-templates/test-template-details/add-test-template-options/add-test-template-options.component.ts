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
	setMaxErrors: boolean;

	title: string;
  	description: string;

	constructor(
		private activatedRoute: ActivatedRoute,
	  	public translate: I18nService,
	  	private location: Location,
		private testTemplateService: TestTemplateService,
		private notificationService: NotificationsService,
		private formBuilder: FormBuilder
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
			maxErrors: new FormControl()
		});

		this.initData();
	}

	initData(): void {
		this.activatedRoute.parent.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.testId = response.id;
				this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testId }).subscribe(res => {
					this.get();
					this.initForm(res.data);
				})
			}
			else
			this.initForm();
		})
	}

	get(){
		this.testTemplateService.getTestTemplate(this.testId).subscribe(res => {
			if(res && res.data) {
				this.isLoading = false;
				this.status = res.data.status;
				this.mode = res.data.mode;
				if(this.status == TestTemplateStatusEnum.Active || this.status == TestTemplateStatusEnum.Canceled){
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
				maxErrors: this.formBuilder.control((test && test.maxErrors) || null)
			});
			this.possibleChangeAnswer = this.settingsForm.value.possibleChangeAnswer;
			this.possibleGetToSkipped = this.settingsForm.value.possibleGetToSkipped;
			this.hidePagination = this.settingsForm.value.hidePagination;
			this.showManyQuestionPerPage = this.settingsForm.value.showManyQuestionPerPage;
			if(this.settingsForm.value.maxErrors) this.setMaxErrors = true;
		}
		else {
			this.settingsForm = this.formBuilder.group({
				testTemplateId: this.testId,
				startWithoutConfirmation: this.formBuilder.control(false),
				startBeforeProgrammation: this.formBuilder.control(false),
				startAfterProgrammation: this.formBuilder.control(false),
				possibleGetToSkipped: this.formBuilder.control(false),
				possibleChangeAnswer: this.formBuilder.control(false),
				canViewResultWithoutVerification: this.formBuilder.control(false),
				canViewPollProgress: this.formBuilder.control(false),
				hidePagination: this.formBuilder.control(false),
				showManyQuestionPerPage: this.formBuilder.control(false),
				questionsCountPerPage: this.formBuilder.control(null),
				maxErrors: this.formBuilder.control(null)
			});
		}
	}

	showManyQuestionPerPageChange(value) {
		this.showManyQuestionPerPage = value;
	}

	possibleChangeAnswerChange(value){
		this.possibleChangeAnswer = value;
	}

	possibleGetToSkippedChange(value){
		this.possibleGetToSkipped = value;
	}

	hidePaginationChange(value){
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