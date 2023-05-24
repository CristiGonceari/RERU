import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { SetPassword } from '../../../utils/models/set-password.model';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ConfirmedValidator } from '../../../utils/form-validator/password.validator';

@Component({
  selector: 'app-set-password',
  templateUrl: './set-password.component.html',
  styleUrls: ['./set-password.component.scss']
})
export class SetPasswordComponent implements OnInit {
  passwordForm: FormGroup;
  isLoading: boolean = true;
  setPasswordData: SetPassword;
  userId: any;
  userData: any;
  title: string;
	description: string;
	no: string;
	yes: string;
  mobileButtonLength: string = "100%";

  constructor(
    private activatedRoute: ActivatedRoute,
		public translate: I18nService,
    private fb: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.activatedRoute.params.subscribe(params => {
      if (params.id) {
        this.getUserById(params.id);
        this.userId = params.id;
        this.initForm(this.userId);
      }
    });
  }

  hasErrors(field): boolean {
    return this.passwordForm.touched && this.passwordForm.get(field).invalid;
  }

  hasError(field: string, error = 'required'): boolean {
    return this.passwordForm.get(field).invalid &&
      this.passwordForm.get(field).touched &&
      this.passwordForm.get(field).hasError(error);
  }

  initForm(userId): void {
    this.passwordForm = this.fb.group({
      id: this.fb.control(userId, [Validators.required]),
      password: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&)(])[A-Za-z\d@$!%*#?&].{6,}')]),
      repeatNewPassword: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&)(])[A-Za-z\d@$!%*#?&].{6,}')]),
      emailNotification: this.fb.control(false, [Validators.required])
    }, { 
      validator: ConfirmedValidator('password', 'repeatNewPassword')
    });
    this.isLoading = false;
  }

  parseRequest(data: SetPassword): SetPassword {
    return {
      ...data,
    };
  }

  setPassword(): void {
    const setPassword = this.parseRequest(this.passwordForm.value);
    setPassword.id = this.userId;
    if (setPassword.password == setPassword.repeatNewPassword) {
      this.userService.setPassword(setPassword).subscribe(
        (res) => {
            forkJoin([
              this.translate.get('modal.success'),
              this.translate.get('set-password.succes-set-password'),
            ]).subscribe(([title, description]) => {
              this.title = title;
              this.description = description;
            });
          this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
          this.back();
        },
        (err) => {
            forkJoin([
              this.translate.get('notification.title.error'),
              this.translate.get('notification.body.error'),
            ]).subscribe(([title, description]) => {
              this.title = title;
              this.description = description;
            });
          this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        }
      );
    }
    else {
      forkJoin([
        this.translate.get('notification.title.warning'),
        this.translate.get('set-password.pass-is-not-the-same'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    }
  }

  getUserById(id: string) {
    this.userService.getUser(id).subscribe(response => {
      this.userData = response.data;
    });
  }

  back(): void {
    this.location.back();
  }
}
