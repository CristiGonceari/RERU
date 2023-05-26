import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PersonalData } from '../../../utils/models/personal-data.model';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ProfileService } from '../../../utils/services/profile.service';

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
	startDate;
	birthDate;
	date: string;

	constructor(
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private fb: FormBuilder,
		private userService: UserService,
		private profileService: ProfileService,
		private notificationService: NotificationsService
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
		return this.personalDataForm?.touched && this.personalDataForm?.get(field)?.invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.personalDataForm.get(field).invalid &&
			this.personalDataForm.get(field).touched &&
			this.personalDataForm.get(field).hasError(error)
		);
	}

	initForm(oldPersonalData: PersonalData): void {
		var matchesPattern = '^[a-zA-ZĂăÎîȘșȚțÂâ]+([- ]?[a-zA-ZĂăÎîȘșȚțÂâ]+)*$';
		
		this.personalDataForm = this.fb.group({
			firstName: this.fb.control(oldPersonalData.firstName, [
				Validators.required,
				Validators.pattern(matchesPattern),
			]),
			lastName: this.fb.control(oldPersonalData.lastName, [
				Validators.required,
				Validators.pattern(matchesPattern),
			]),
			fatherName: this.fb.control(oldPersonalData.fatherName, [
				Validators.pattern(matchesPattern),
			]),
			phoneNumber: this.fb.control(oldPersonalData.phoneNumber, [
				Validators.required,
				Validators.pattern(
					"^((\\+373-?)|0)?[0-9]{8}$"
				),
				Validators.maxLength(12), 
				Validators.minLength(12)
			]),
			email: this.fb.control(oldPersonalData.email, [
				Validators.required,
				Validators.email
			]),
			emailNotification: this.fb.control(false,  [
				Validators.required
			])
		});
		this.birthDate = oldPersonalData.birthDate;
		this.isLoading = false;
	}

	parseRequest(data: PersonalData): PersonalData {
		return {
			birthDate: this.date || null,
			...data
		};
	}

	setBirthDate(): void {
		if (this.birthDate) {
			const date = new Date(this.birthDate);
			this.date = new Date(date.getTime() - (new Date(this.birthDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	changePersonalData(): void {
		this.isLoading = true;
		this.setBirthDate();
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
				this.isLoading = false;
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.subsribeForParams();
				this.profileService.isUserUpdated.next(true);
			},
			(err) => {
				let lol: string = err.error.messages[0].messageText;
				if(lol.includes("DuplicateUserName")){
					forkJoin([
						this.translate.get('modal.error'),
					]).subscribe(([title]) => {
						this.title = title;
						this.description = "Email duplicat";
					});
					this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				}

				this.isLoading = false;
			}
		);
	}

	getPersonalData() {
		this.userService.getPersonalData().subscribe(response => this.initForm(response.data));
	}
}
