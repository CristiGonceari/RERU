import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { TestTemplateStatusEnum } from '../../../utils/enums/test-template-status.enum';
import { SelectItem } from '../../../utils/models/select-item.model';
import { TestTemplate } from '../../../utils/models/test-templates/test-template.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { BasicTemplateEnum } from '../../../utils/enums/basic-template.enum';

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
	modeId: number = 0;
	modes: SelectItem[] = [{ label: '', value: '' }];
	qualifyingTypes: SelectItem[] = [{ label: '', value: '' }];
	qualifyingTypesWithout15: SelectItem[] = [{ label: '', value: '' }];

	testTemplate: TestTemplate;
	title: string;
	description: string;
	basicTemplateEnum = BasicTemplateEnum;

	tags: any[] = [];
	placeHolder = '+ Rol';
	items = [];
	mobileButtonLength: string = "100%";

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
			status: new FormControl(),
			basicTestTemplate: new FormControl(null),
			isGridTest: new FormControl()
		});
		this.onTextChange();
		this.getMode();
		this.initData();
		this.getQualifyingType();
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.testId = response.id;
				this.testTemplateService.getTestTemplate(this.testId).subscribe(res => {
					if (res && res.data) {
						this.testTemplate = res.data;
						this.isLoading = false;
						this.initForm(res.data);
						res.data.roles.forEach(element => {
							this.tags.push({ display: element.label, value: +element.value })
						});
					}
				})
			}
			else {
				this.initForm();
				this.isLoading = false;
			}
		})
	}

	getTestTemplate() {
		this.testTemplateService.getTestTemplate(this.testId).subscribe(res => {
			if (res && res.data) {
				if (res.data.mode == 0)
					this.addTestSettings(this.testId);
				else if (res.data.mode == 1)
					this.addPollSetting(this.testId);
				else if (res.data.mode == 2)
					this.addEvaluationSetting(this.testId);
			}

			res.data.roles.forEach(element => {
				this.tags.push({ display: element.label, value: +element.value })
			});
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

	initForm(test?: any, value?: number): void {
		if (test) {
			if (value == null) {
				this.testForm = this.formBuilder.group({
					id: this.formBuilder.control(test.id || null, [Validators.required]),
					name: this.formBuilder.control((test && test.name) || null, [Validators.required]),
					questionCount: this.formBuilder.control((test && test.questionCount) || null, [Validators.required]),
					duration: this.formBuilder.control((test && !isNaN(test.duration) ? test.duration : null), [Validators.required]),
					minPercent: this.formBuilder.control(test && test.minPercent, [Validators.required]),
					mode: this.formBuilder.control((test && test.mode), [Validators.required]),
					qualifyingType: this.formBuilder.control((test && !isNaN(test.qualifyingType) ? test.qualifyingType : null), [Validators.required]),
					status: this.statusEnum.Draft,
					moduleRoles: this.items,
					basicTestTemplate: this.formBuilder.control(test?.basicTestTemplate),
					isGridTest: this.formBuilder.control(test?.isGridTest)
				});

				this.modeId = this.testForm.value.mode;
			} else {
				this.testForm = this.formBuilder.group({
					id: this.formBuilder.control(test.id || null, [Validators.required]),
					name: this.formBuilder.control((test && test.name) || null, [Validators.required]),
					questionCount: this.formBuilder.control((test && test.questionCount) || null, [Validators.required]),
					duration: this.formBuilder.control((test && test.duration) || null, [Validators.required]),
					minPercent: this.formBuilder.control(test && test.minPercent, [Validators.required]),
					mode: this.formBuilder.control(value, [Validators.required]),
					qualifyingType: this.formBuilder.control((test && !isNaN(test.qualifyingType) ? test.qualifyingType : null), [Validators.required]),
					status: this.statusEnum.Draft,
					moduleRoles: this.items,
					basicTestTemplate: this.formBuilder.control(test?.basicTestTemplate),
					isGridTest: this.formBuilder.control(test?.isGridTest)
				});

				this.modeId = this.testForm.value.mode;
			}
		}
		else {
			if (this.modeId == 0) {
				this.testForm = this.formBuilder.group({
					name: this.formBuilder.control(null, [Validators.required]),
					questionCount: this.formBuilder.control(null, [Validators.required]),
					duration: this.formBuilder.control(null, [Validators.required]),
					minPercent: this.formBuilder.control(null, [Validators.required]),
					mode: this.formBuilder.control(this.modeId, [Validators.required]),
					qualifyingType: this.formBuilder.control(2, [Validators.required]),
					status: this.statusEnum.Draft,
					moduleRoles: this.items || [],
					basicTestTemplate: this.formBuilder.control(null),
					isGridTest: this.formBuilder.control(true)
				});
			} else if (this.modeId == 1) {
				this.testForm = this.formBuilder.group({
					name: this.formBuilder.control(null, [Validators.required]),
					questionCount: this.formBuilder.control(null, [Validators.required]),
					mode: this.formBuilder.control(this.modeId, [Validators.required],),
					status: this.statusEnum.Draft,
					moduleRoles: this.items || [],
					basicTestTemplate: this.formBuilder.control(null),
					isGridTest: this.formBuilder.control(true)
				});
			} else {
				this.testForm = this.formBuilder.group({
					name: this.formBuilder.control(null, [Validators.required]),
					questionCount: this.formBuilder.control(null, [Validators.required]),
					mode: this.formBuilder.control(this.modeId, [Validators.required]),
					qualifyingType: this.formBuilder.control(2, [Validators.required]),
					status: this.statusEnum.Draft,
					moduleRoles: this.items || [],
					basicTestTemplate: this.formBuilder.control(null),
					isGridTest: this.formBuilder.control(true)
				});
			}
		}
	}

	onTextChange() {
		this.referenceService.getArticleRoles().subscribe(res => {
			res.data.forEach(element => {
				this.items.push({ display: element.label, value: +element.value })
			});
		})
	};

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
		if (this.testTemplate != null) {
			this.initForm(this.testTemplate, value);
		} else {
			this.initForm();
		}
	}

	onSave(): void {
		this.isLoading = true;
		if (this.testId) {
			this.edit();
		} else {
			this.add();
		}
	}

	parseNumber() {
		if (this.modeId == 0) {
			let parsedNumber = this.testForm.get("minPercent").value;
			this.testForm.get("minPercent").setValue(Math.round(parsedNumber));
		}
		const tagsArr = this.tags.map(obj => typeof obj.value !== 'number' ? { ...obj, value: 0 } : obj);
		this.testForm.get("moduleRoles").setValue(tagsArr);
	}

	edit() {
		this.parseNumber();
		if (this.modeId == 0) this.testForm.value.qualifyingType = 1;
		if (this.modeId == 1) this.testForm.value.qualifyingType = 5;

		this.testTemplateService.editTestTemplate({ data: this.preParseRequest(this.testForm.value) }).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-update-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.backClicked();
			this.isLoading = false;
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		}, err => {
			this.isLoading = false;
		});
	}


	add() {
		this.parseNumber();
		if (this.modeId == 0) this.testForm.value.qualifyingType = 1;
		if (this.modeId == 1) this.testForm.value.qualifyingType = 5;

		this.testTemplateService.addTestTemplate({ data: this.preParseRequest(this.testForm.value) }).subscribe(res => {

			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.testId = res.data;
			this.getTestTemplate();
			this.backClicked();
			this.isLoading = false;
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		}, err => {
			this.isLoading = false;
		});
	}

	private preParseRequest(data: TestTemplate): TestTemplate {
		return {
			...data,
			basicTestTemplate: !isNaN(data?.basicTestTemplate) ? data.basicTestTemplate : null
		}
	}

	backClicked() {
		this.location.back();
	}

	addPollSetting(testTemplateId) {
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

		this.testTemplateService.addEditTestTemplateSettings({ data: data }).subscribe();
	}

	addEvaluationSetting(testTemplateId) {
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

		this.testTemplateService.addEditTestTemplateSettings({ data: data }).subscribe();
	}

	addTestSettings(testTemplateId) {
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
			formulaForOneAnswer: 0,
			negativeScoreForOneAnswer: null,
			formulaForMultipleAnswers: 0,
			negativeScoreForMultipleAnswers: null
		}

		this.testTemplateService.addEditTestTemplateSettings({ data: data }).subscribe();
	}
}
