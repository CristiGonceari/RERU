import { UserService } from './../../../utils/services/user.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ValidatorUtil } from '../../../utils/util/validator.util';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  userForm: FormGroup;
  title: string;
	description: string;

  userId: any;
  fileId: string;
  fileType: string = null;
  attachedFile: File;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router,
		public translate: I18nService,
    private ngZone: NgZone,
    private route: ActivatedRoute,
		private location: Location
  ) { } 

  ngOnInit(): void {
    this.initForm();
    if(this.userId)this.get();
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
  get() {
    this.userService.getUser(this.userId).subscribe(res => {
      if (res && res.data) {
        this.fileId = res.data.mediaFileId;
      }
    });
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

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.userForm, field);
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
      request.append('EmailNotification', this.userForm.value.emailNotification);

    this.userService.createUser(request).subscribe(res => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user.succes-create'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
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

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  back(): void {
		this.location.back();
	}
}
