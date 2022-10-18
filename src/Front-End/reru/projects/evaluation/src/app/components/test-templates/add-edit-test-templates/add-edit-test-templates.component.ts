import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateModeEnum } from '../../../utils/enums/test-template-mode.enum';
import { TestTemplateStatusEnum } from '../../../utils/enums/test-template-status.enum';
import { SelectItem } from '../../../utils/models/select-item.model';
import { TestTemplate } from '../../../utils/models/test-templates/test-template.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'add-edit-test-templates',
  templateUrl: './add-edit-test-templates.component.html',
  styleUrls: ['./add-edit-test-templates.component.scss']
})
export class AddEditTestTemplateComponent implements OnInit {
	testForm: FormGroup;
	testId: number;
	statusEnum = TestTemplateStatusEnum;
	isLoading: boolean = true;
	modeId;
	modes: SelectItem[] = [{label: '', value: ''}];
	qualifyingTypes: SelectItem[] = [{label: '', value: ''}];
	qualifyingTypesWithout15: SelectItem[] = [{label: '', value: ''}];
	testTemplate: TestTemplate = new TestTemplate();
	title: string;
	description: string;

	constructor(private location: Location,
		private testTemplateService: TestTemplateService,
		private activatedRoute: ActivatedRoute,
	  	public translate: I18nService,
	  	private formBuilder: FormBuilder,
		private referenceService: ReferenceService,
		private notificationService: NotificationsService) { }

	ngOnInit(): void {
		this.testForm = new FormGroup({
			name: new FormControl(),
			questionCount: new FormControl(),
			duration: new FormControl(),
			mode: new FormControl(),
			qualifyingType: new FormControl(),
			minPercent: new FormControl(),
			status: new FormControl()
		});
		this.initData();
		this.getMode();
		this.getQualifyingType();
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.testId = response.id;
				this.testTemplateService.getTestTemplate( this.testId ).subscribe(res => {
					if (res && res.data) {
						this.testTemplate = res.data;
						this.isLoading = false;
						this.initForm(res.data);
					}
				})
			}
			else {
				this.isLoading = false;
				this.initForm();
			}
		})
	}

	getTestTemplate(){
		this.testTemplateService.getTestTemplate( this.testId ).subscribe(res => {
			if (res && res.data) {
				if(res.data.mode == 1) 
					this.addPollSetting(this.testId);
				else if(res.data.mode == 2)
					this.addEvaluationSetting(this.testId);
			}
		})
	}

	getMode() {
		this.referenceService.getMode().subscribe((res) => this.modes = res.data);
	}

	getQualifyingType() {
		this.referenceService.getQualifyingType().subscribe((res) => {
			this.qualifyingTypes = res.data;
			this.qualifyingTypesWithout15 = this.qualifyingTypes.filter(x => x.value == '2' || x.value == '3' || x.value == '4');
		});
	}

	initForm(test?: any): void {
		if (test) {
			this.testForm = this.formBuilder.group({
				id: this.formBuilder.control(test.id, [Validators.required]),
				name: this.formBuilder.control((test && test.name) || null, [Validators.required]),
				questionCount: this.formBuilder.control((test && test.questionCount) || null, [Validators.required]),
				duration: this.formBuilder.control((test && test.duration) || null, [Validators.required]),
				minPercent: this.formBuilder.control(test && test.minPercent, [Validators.required, Validators.pattern(/^-?(0|[1-9]\d*\.)?$/)]),
				mode: this.formBuilder.control((test && !isNaN(test.mode) ? test.mode : null), [Validators.required]),
				qualifyingType: this.formBuilder.control((test && !isNaN(test.qualifyingType) ? test.qualifyingType : null), [Validators.required]),
				status: this.statusEnum.Draft,
			});
			this.modeId = this.testForm.value.mode;
		}
		else {
			this.testForm = this.formBuilder.group({
				name: this.formBuilder.control(null, [Validators.required]),
				questionCount: this.formBuilder.control(null, [Validators.required]),
				duration: this.formBuilder.control(null, [Validators.required]),
				minPercent: this.formBuilder.control([Validators.required] , [Validators.pattern(/^-?(0|[1-9]\d*\.)?$/)]),
				mode: this.formBuilder.control(0, [Validators.required]),
				qualifyingType: this.formBuilder.control(2, [Validators.required]),
				status: this.statusEnum.Draft,
			});
			this.modeId = this.testForm.value.mode;
		}
	}

	hasErrors(field): boolean {
		return this.testForm.touched && this.testForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.testForm.get(field).touched && this.testForm.get(field).hasError(error)
		);
	}

	onChange(value) {
		this.modeId = value;
	}

	onSave(): void {
		if (this.testId) {
			this.edit();
		} else {
			this.add();
		}
	}

	parseNumber(){
		let parsedNumber = this.testForm.get("minPercent").value;
		this.testForm.get("minPercent").setValue(Math.round(parsedNumber))
	}

	edit() {
		this.parseNumber();
		if(this.modeId == 0) this.testForm.value.qualifyingType = 1;
		if(this.modeId == 1) this.testForm.value.qualifyingType = 5;
		
		this.testTemplateService.editTestTemplate({ data: this.testForm.value }).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-update-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	add() {
		this.parseNumber();
		if(this.modeId == 0) this.testForm.value.qualifyingType = 1;
		if(this.modeId == 1) this.testForm.value.qualifyingType = 5;

		this.testTemplateService.addTestTemplate({data: this.testForm.value}).subscribe(res => { 
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.testId = res.data; 
			this.getTestTemplate();
			this.backClicked()
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	backClicked() {
		this.location.back();
	}

	addPollSetting(testTemplateId){
		let data = {
			testTemplateId: testTemplateId,
			startWithoutConfirmation: true,
			startBeforeProgrammation: true,
			startAfterProgrammation: true,
			possibleGetToSkipped: false,
			possibleChangeAnswer: false,
			canViewResultWithoutVerification: false,
			canViewPollProgress: false,
			hidePagination: false,
			showManyQuestionPerPage: false,
			questionsCountPerPage: null,
			maxErrors: null,
			formulaForOneAnswer: null,
			negativeScoreForOneAnswer: null,
			formulaForMultipleAnswers: null,
			negativeScoreForMultipleAnswers: null
		}

		this.testTemplateService.addEditTestTemplateSettings({data: data}).subscribe();
	}

	addEvaluationSetting(testTemplateId){
		let data = {
			testTemplateId: testTemplateId,
			startWithoutConfirmation: true,
			startBeforeProgrammation: true,
			startAfterProgrammation: true,
			possibleGetToSkipped: true,
			possibleChangeAnswer: true,
			canViewResultWithoutVerification: true,
			canViewPollProgress: false,
			hidePagination: false,
			showManyQuestionPerPage: false,
			questionsCountPerPage: null,
			maxErrors: null,
			formulaForOneAnswer: null,
			negativeScoreForOneAnswer: null,
			formulaForMultipleAnswers: null,
			negativeScoreForMultipleAnswers: null
		}

		this.testTemplateService.addEditTestTemplateSettings({data: data}).subscribe();
	}
}
