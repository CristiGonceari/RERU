import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../utils/services/user.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { FileTypeEnum } from '@erp/shared';
import { DepartmentService } from '../../../utils/services/department.service';
import { UserRoleService } from '../../../utils/services/user-role.service';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ReferenceService } from '../../../utils/services/reference.service';

@Component({
	selector: 'app-edit',
	templateUrl: './edit.component.html',
	styleUrls: ['./edit.component.scss'],
})
export class EditComponent implements OnInit {
	userForm: FormGroup;
	isLoading: boolean = true;
	userId: string;
	title: string;
	description: string;

	fileId: string;
	fileType: FileTypeEnum = FileTypeEnum.Photos;
	attachedFile: File;
	departments: SelectItem[] = [{ label: '', value: '' }];
	roles: SelectItem[] = [{ label: '', value: '' }];
	accessModes: SelectItem[] = [{ label: '', value: '' }];
	employeeFunctions: SelectItem[] = [{ label: '', value: '' }];

	birthDate: Date;
	date: string;
	startDate;

	constructor(
		private activatedRoute: ActivatedRoute,
		private fb: FormBuilder,
		public translate: I18nService,
		private userService: UserService,
		private notificationService: NotificationsService,
		private userProfileService: UserProfileService,
		private router: Router,
		private location: Location,
		private departmentService: DepartmentService,
		private roleService: UserRoleService,
		private referenceService: ReferenceService,
	) { }

	ngOnInit(): void {
		this.initData();
		this.getAccessMode();
		this.getDepartments();
		this.getRoles();
		this.getEmployeeFunctions();
	}

	getDepartments() {
		this.departmentService.getValues().subscribe(res => this.departments = res.data);
	}

	getRoles() {
		this.roleService.getValues().subscribe(res => this.roles = res.data);
	}

	getEmployeeFunctions(){
		this.referenceService.getEmployeeFunctionsSelectValues().subscribe(res => this.employeeFunctions = res.data);
	}

	getAccessMode() {
		this.userProfileService.getAccessMode().subscribe(res => this.accessModes = res.data);
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (response.id && response.id !== 'undefined') {
				this.userId = response.id;
				this.userService.getEditUserPersonalDetails(response.id).subscribe(res => {
					this.initForm(res.data);
					this.isLoading = false;
					this.fileId = res.data.mediaFileId;
				});
			} else {
				this.initForm();
				this.isLoading = false;
			}
		});
	}

	hasErrors(field): boolean {
		return this.userForm.touched && this.userForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.userForm.get(field).invalid && this.userForm.get(field).touched && this.userForm.get(field).hasError(error)
		);
	}

	initForm(user?: any): void {
		var matchesPattern = '^[a-zA-ZĂăÎîȘșȚțÂâ]+([- ]?[a-zA-ZĂăÎîȘșȚțÂâ]+)*$';

		this.userForm = this.fb.group({
			id: this.fb.control(user.id, [Validators.required]),
			firstName: this.fb.control((user && user.firstName) || null, [
				Validators.required,
				Validators.pattern(matchesPattern),
			]),
			lastName: this.fb.control((user && user.lastName) || null, [
				Validators.required,
				Validators.pattern(matchesPattern),
			]),
			fatherName: this.fb.control((user && user.fatherName) || null, [Validators.pattern(matchesPattern)]),
			phoneNumber: this.fb.control((user && user.phoneNumber) || "", [
				Validators.required,
				Validators.pattern(
					"^((\\+373-?)|0)?[0-9]{8}$"
				),
				Validators.maxLength(12), 
				Validators.minLength(12)
			]),
			email: this.fb.control((user && user.email) || "", [
				Validators.required,
				Validators.email
			]),
			emailNotification: this.fb.control(false,  [
				Validators.required
			]),
			departmentColaboratorId: this.fb.control((user && user.departmentColaboratorId) || null, Validators.required),
			roleColaboratorId: this.fb.control((user && user.roleColaboratorId) || null, [Validators.required]),
			functionColaboratorId: this.fb.control((user && user.functionColaboratorId) || null, [Validators.required]),
			accessModeEnum: this.fb.control((user && user.accessModeEnum) || 0, [Validators.required]),
		});

		this.birthDate = user.birthDate;
		this.isLoading = false;
	}

	setBirthDate(): void {
		if (this.birthDate) {
			const date = new Date(this.birthDate);
			this.date = new Date(date.getTime() - (new Date(this.birthDate).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	editUser(): void {
		this.isLoading = true;
		this.setBirthDate();
		let data = {
			id: this.userForm.value.id,
			firstName: this.userForm.value.firstName,
			lastName: this.userForm.value.lastName,
			fatherName: this.userForm.value.fatherName,
			email: this.userForm.value.email,
			emailNotification: this.userForm.value.emailNotification,
			birthDate: this.date || null,
			phoneNumber: this.userForm.value.phoneNumber,
			departmentColaboratorId: +this.userForm.value.departmentColaboratorId || null,
			roleColaboratorId: +this.userForm.value.roleColaboratorId || null,
			functionColaboratorId: +this.userForm.value.functionColaboratorId || null,
			accessModeEnum: this.userForm.value.accessModeEnum
		}

		this.userService.editUserPersonalDetails(data).subscribe(
			(res) => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('user.succes-edit'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});

				const request = new FormData();

				if (this.attachedFile) {
					request.append('Data.File.File', this.attachedFile);
					request.append('Data.File.Type', this.fileType.toString());
					request.append('Data.UserId', res.data);
					this.userService.addUserAvatar(request).subscribe((response) => {
						if (response.ok) {
							this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
							this.isLoading = false;
						}
					})
				} else {
					request.append('Data.UserId', res.data);
					this.userService.addUserAvatar(request).subscribe((response) => {
						if (response.ok) {
							this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
							this.isLoading = false;
						}
					})
				}
				this.backClicked();
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

	checkFile(event) {
		if (event != null) this.attachedFile = event;
		else this.fileId = null;
	}

	backClicked() {
		this.location.back();
	}
}
