import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PersonalData } from '../../../utils/models/personal-data.model';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
	selector: 'app-change-personal-data',
	templateUrl: './change-personal-data.component.html',
	styleUrls: ['./change-personal-data.component.scss']
})
export class ChangePersonalDataComponent implements OnInit {
	personalDataForm: FormGroup;
	isLoading: boolean = true;
	title: string;
	description: string;

	constructor(
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private fb: FormBuilder,
		private userService: UserService,
		private notificationService: NotificationsService,
		private router: Router
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.activatedRoute.params.subscribe(params => {
			if (params) {
				this.getPersonalData();
			}
		});
	}

	hasErrors(field): boolean {
		return this.personalDataForm.touched && this.personalDataForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.personalDataForm.get(field).invalid &&
			this.personalDataForm.get(field).touched &&
			this.personalDataForm.get(field).hasError(error)
		);
	}

	initForm(oldPersonalData: PersonalData): void {
		this.personalDataForm = this.fb.group({
			name: this.fb.control(oldPersonalData.name, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
			lastName: this.fb.control(oldPersonalData.lastName, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
		});
		this.isLoading = false;
	}

	parseRequest(data: PersonalData): PersonalData {
		return {
			...data,
		};
	}

	changePersonalData(): void {
		const personalDataForUpdate = this.parseRequest(this.personalDataForm.value);

		this.userService.changePersonalData(personalDataForUpdate).subscribe(
			res => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('user.succes-edit'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
					});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				window.location.reload();
			},
			err => {
				this.isLoading = false;
			}
		);
	}

	getPersonalData() {
		this.userService.getPersonalData().subscribe(response => this.initForm(response.data));
	}
}
