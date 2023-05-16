import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { QuestionCategory } from '../../../utils/models/question-category/question-category.model';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { QuestionCategoryService } from '../../../utils/services/question-category/question-category.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';

@Component({
	selector: 'app-add-edit-category',
	templateUrl: './add-edit-category.component.html',
	styleUrls: ['./add-edit-category.component.scss']
})
export class AddEditCategoryComponent implements OnInit {

	categoryForm: FormGroup;
	categoryId: number;
	isLoading: boolean = true;  
	title: string;
	description: string;
	mobileButtonLength: string = "100%";

	constructor(
		private formBuilder: FormBuilder,
		private categoryService: QuestionCategoryService,
		public translate: I18nService,
		private location: Location,
		private activatedRoute: ActivatedRoute,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.categoryForm = new FormGroup({ name: new FormControl() });
		this.initData();
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.categoryId = response.id;
				this.categoryService.get(this.categoryId).subscribe(res => {
					this.initForm(res.data);
				})
			}
			else
				this.initForm();
		})
	}

	initForm(category?: any): void {
		if (category) {
			this.categoryForm = this.formBuilder.group({
				id: this.formBuilder.control(category.id, [Validators.required]),
				name: this.formBuilder.control((category && category.name) || null, Validators.required)
			});
			this.isLoading = false;
		}
		else {
			this.categoryForm = this.formBuilder.group({
				name: this.formBuilder.control(null, [Validators.required]),
			});
			this.isLoading = false;
		}
	}

	onSave(): void {
		if (this.categoryId) {
			this.editQuestionCategory();
		} else {
			this.addQuestionCategory();
		}
	}

	backClicked() {
		this.location.back();
	}

	addQuestionCategory(): void {
		const createCategoryDto = {
			name: this.categoryForm.value.name,
		} as QuestionCategory;

		this.categoryService.create(createCategoryDto).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('categories.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	editQuestionCategory(): void {
		const editCategoryDto = {
			id: this.categoryId,
			name: this.categoryForm.value.name,
		} as QuestionCategory;

		this.categoryService.edit(editCategoryDto).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('categories.succes-update-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}
}
