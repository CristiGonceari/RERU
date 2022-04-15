import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../utils/services/user.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { FileTypeEnum } from 'projects/erp-shared/src/lib/models/FileTypeEnum';

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

	constructor(
		private activatedRoute: ActivatedRoute,
		private fb: FormBuilder,
		public translate: I18nService,
		private userService: UserService,
		private notificationService: NotificationsService,
		private router: Router,
		private location: Location
	) { }

	ngOnInit(): void {
		this.initData();
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
		this.userForm = this.fb.group({
			id: this.fb.control(user.id, [Validators.required]),
			name: this.fb.control((user && user.name) || null, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
			lastName: this.fb.control((user && user.lastName) || null, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
			fatherName: this.fb.control((user && user.fatherName) || null, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
		});
		this.isLoading = false;
	}

	editUser(): void {
		this.isLoading = true;
		let data = {
			id: this.userForm.value.id,
			name: this.userForm.value.name,
			lastName: this.userForm.value.lastName,
			fatherName: this.userForm.value.fatherName
		}

		this.userService.editUserPersonalDetails(data).subscribe(
			res => {
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
					this.userService.addUserAvatar(request).subscribe(() => {
						this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
						this.isLoading = false;
						this.back();
					})
				} else {
					request.append('Data.UserId', res.data);
					this.userService.addUserAvatar(request).subscribe(() => {
						this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
						this.isLoading = false;
						this.back();
					})
				}
			}
		);
	}

	checkFile(event) {
		if (event != null) this.attachedFile = event;
		else this.fileId = null;
	}

	back(): void {
		this.location.back();
	}
}
