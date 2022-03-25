import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import {  FormGroup  } from '@angular/forms';
import {  Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../utils/services/i18n.service';
import { UserService } from '../../utils/services/user.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { ValidatorUtil } from '../../utils/util/validator.util';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss']
})
export class RegistrationPageComponent implements OnInit {
  
  userForm: FormGroup;
  title: string;
	description: string;
success : boolean = false;

  userId: any;
  fileId: string;
  fileType: string = null;
  attachedFile: File;


  isCollapsed = true;
  
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router,
		public translate: I18nService,
    private ngZone: NgZone,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.initForm();
  }
  
  hasErrors(field): boolean {
		return this.userForm.touched && this.userForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.userForm.get(field).invalid &&
			this.userForm.get(field).touched &&
			this.userForm.get(field).hasError(error)
		);
	}

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.userForm, field);
  }
  
  initForm(): void {
    this.userForm = this.fb.group({
      name: this.fb.control(null, [Validators.required,Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      lastName: this.fb.control(null, [Validators.required,Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      fatherName: this.fb.control(null, [Validators.required,Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      idnp: this.fb.control(null, [Validators.required, Validators.maxLength(13), Validators.minLength(13)]),
      email: this.fb.control(null, [Validators.required , Validators.email]),
      emailNotification: this.fb.control(false, [Validators.required])
    });
  }

  addUser(): void {
    const request = new FormData();
    if (this.attachedFile) {
      this.fileType = '7';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
      request.append('Name', this.userForm.value.name);
      request.append('LastName', this.userForm.value.lastName);
      request.append('FatherName', this.userForm.value.fatherName);
      request.append('Email', this.userForm.value.email);
      request.append('Idnp', this.userForm.value.idnp);
      request.append('EmailNotification', "true");

    this.userService.createUser(request).subscribe(res => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user.succes-create'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
        this.success = true;
				});
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      forkJoin([
				this.translate.get('notification.title.error'),
				this.translate.get('notification.body.error'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }
}
