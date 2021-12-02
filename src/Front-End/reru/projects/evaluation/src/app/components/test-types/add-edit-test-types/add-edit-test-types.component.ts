import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { TestTypeModeEnum } from '../../../utils/enums/test-type-mode.enum';
import { TestTypeStatusEnum } from '../../../utils/enums/test-type-status.enum';
import { SelectItem } from '../../../utils/models/select-item.model';
import { TestType } from '../../../utils/models/test-types/test-type.model';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { Location } from '@angular/common';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-add-edit-test-types',
  templateUrl: './add-edit-test-types.component.html',
  styleUrls: ['./add-edit-test-types.component.scss']
})
export class AddEditTestTypesComponent implements OnInit {
	testForm: FormGroup;
	testId: number;
	statusEnum = TestTypeStatusEnum;
	isLoading: boolean = true;
	modeId;
	modes: SelectItem[] = [{label: '', value: ''}];
	testType: TestType = new TestType();

	constructor(private location: Location,
		private testTypeService: TestTypeService,
		private activatedRoute: ActivatedRoute,
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
				this.testTypeService.getTestType( this.testId ).subscribe(res => {
					if (res && res.data) {
						this.testType = res.data;
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
				minPercent: this.formBuilder.control((test && test.minPercent) || null, [Validators.required]),
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
				minPercent: this.formBuilder.control(null, [Validators.required]),
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

	edit() {
		this.testTypeService.editTestType({ data: this.testForm.value }).subscribe(() => {
			this.backClicked();
			this.notificationService.success('Success', 'Test type was successfully updated', NotificationUtil.getDefaultMidConfig());
		});
	}

	add() {
		this.testTypeService.addTestType({data: this.testForm.value}).subscribe(res => { 
			this.testId = res.data; 
			this.settings();
			// this.backClicked()
			this.notificationService.success('Success', 'Test type was successfully added', NotificationUtil.getDefaultMidConfig());
		});
	}

	settings(){
		let params;
		if(this.modeId == TestTypeModeEnum.Poll){
			params = {
				testTypeId: this.testId,
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
				testTypeId: this.testId,
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
		
		this.testTypeService.addEditTestTypeSettings({data: params}).subscribe(() => this.backClicked());
	}

	backClicked() {
		this.location.back();
	}
}
