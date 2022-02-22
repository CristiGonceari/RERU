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
			minPercent: new FormControl(),
			status: new FormControl()
		});
		this.initData();
		this.getMode();
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

	getMode() {
		this.referenceService.getMode().subscribe((res) => this.modes = res.data);
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
				status: this.statusEnum.Draft,
			});
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
		this.testTemplateService.addTestTemplate({data: this.testForm.value}).subscribe(res => { 
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.testId = res.data; 
			this.settings();
			// this.backClicked()
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	settings(){
		let params;
		if(this.modeId == TestTemplateModeEnum.Poll){
			params = {
				testTemplateId: this.testId,
				startWithoutConfirmation: true,
				startBeforeProgrammation: true,
				startAfterProgrammation: true,
				possibleGetToSkipped: true,
				possibleChangeAnswer: false,
				canViewResultWithoutVerification: false,
				canViewPollProgress: false,
				hidePagination: false,
				showManyQuestionPerPage: false
			}
		} else {
			params = {
				testTemplateId: this.testId,
				startWithoutConfirmation: false,
				startBeforeProgrammation: false,
				startAfterProgrammation: false,
				possibleGetToSkipped: false,
				possibleChangeAnswer: false,
				canViewResultWithoutVerification: false,
				canViewPollProgress: false,
				hidePagination: false,
				showManyQuestionPerPage: false
			}
		}
		this.testTemplateService.addEditTestTemplateSettings({data: params}).subscribe(() => this.backClicked());
	}

	backClicked() {
		this.location.back();
	}
}
