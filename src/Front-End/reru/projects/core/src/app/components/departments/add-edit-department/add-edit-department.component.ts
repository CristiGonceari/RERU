import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { DepartmentService } from '../../../utils/services/department.service';
import { DepartmentModel } from '../../../utils/models/department.model';

@Component({
  selector: 'app-add-edit-department',
  templateUrl: './add-edit-department.component.html',
  styleUrls: ['./add-edit-department.component.scss']
})
export class AddEditDepartmentComponent implements OnInit {
	departmentForm: FormGroup;
	departmentId: number;
	isLoading: boolean = true;  
	title: string;
	description: string;

	constructor(
		private formBuilder: FormBuilder,
		private departmentService: DepartmentService,
		public translate: I18nService,
		private location: Location,
		private activatedRoute: ActivatedRoute,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.departmentForm = new FormGroup({ name: new FormControl() });
		this.initData();
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.departmentId = response.id;
				this.departmentService.get(this.departmentId).subscribe(res => {
					this.initForm(res.data);
				})
			}
			else
				this.initForm();
		})
	}

	initForm(department?: any): void {
		if (department) {
			this.departmentForm = this.formBuilder.group({
				id: this.formBuilder.control(department.id, [Validators.required]),
				name: this.formBuilder.control((department && department.name) || null, Validators.required)
			});
			this.isLoading = false;
		}
		else {
			this.departmentForm = this.formBuilder.group({
				name: this.formBuilder.control(null, [Validators.required]),
			});
			this.isLoading = false;
		}
	}

	onSave(): void {
		if (this.departmentId) {
			this.edit();
		} else {
			this.add();
		}
	}

	backClicked() {
		this.location.back();
	}

	add(): void {
		const data = {
			name: this.departmentForm.value.name,
		} as DepartmentModel;

		this.departmentService.create(data).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('departments.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	edit(): void {
		const data = {
			id: this.departmentId,
			name: this.departmentForm.value.name,
		} as DepartmentModel;

		this.departmentService.edit(data).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('departments.succes-edit-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}
}
