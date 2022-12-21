import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { ChangePassword } from '../../../utils/models/change-password.model';
import { Router } from '@angular/router';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ConfirmedValidator } from '../../../utils/form-validator/password.validator';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})

export class ChangePasswordComponent implements OnInit {
  passwordForm: FormGroup;
  isLoading = true;
  changePasswordData: ChangePassword;
  title: string;
	description: string;

  constructor(
    private fb: FormBuilder,
		public translate: I18nService,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  hasErrors(field): boolean {
    return this.passwordForm.touched && this.passwordForm.get(field).invalid;
  }

  hasError(field: string, error = 'required'): boolean {
    return this.passwordForm.get(field).invalid &&
      this.passwordForm.get(field).touched &&
      this.passwordForm.get(field).hasError(error);
  }
  
  initForm(): void {
    this.passwordForm = this.fb.group({
      oldPassword: this.fb.control('', [Validators.required]),
      newPassword: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&].{6,}')]),
      repeatPassword: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&].{6,}')])
    }, { 
      validator: ConfirmedValidator('newPassword', 'repeatPassword')
    });
    this.isLoading = false;
  }

  parseRequest(data: ChangePassword): ChangePassword {
    return {
      ...data,
    };
  }

  checkPassword(): void {
    const passwordForUpdate = this.parseRequest(this.passwordForm.value);
      if ((passwordForUpdate.newPassword === passwordForUpdate.repeatPassword) && 
        (passwordForUpdate.newPassword !== passwordForUpdate.oldPassword)) {
        this.userService.changePassword(passwordForUpdate).subscribe(
        (res) => {
          console.warn('response', res);
          this.router.navigateByUrl('personal-profile/overview')
          forkJoin([
            this.translate.get('modal.success'),
            this.translate.get('set-password.upd-password'),
          ]).subscribe(([title, description]) => {
            this.title = title;
            this.description = description;
            });
          this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
          this.isLoading = false;
        })
      } else if (passwordForUpdate.newPassword !== passwordForUpdate.repeatPassword){
        forkJoin([
          this.translate.get('notification.title.warning'),
          this.translate.get('set-password.pass-is-not-the-same'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
          });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
      } else if (passwordForUpdate.oldPassword === passwordForUpdate.newPassword){
        forkJoin([
          this.translate.get('notification.title.warning'),
          this.translate.get('set-password.new-old-pass-the-same'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
          });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
      }
  }

  changePassword(): void {
    this.isLoading = true;
    if (this.passwordForm.touched && this.passwordForm.valid) {
      this.checkPassword();
    } else {
      forkJoin([
        this.translate.get('notification.title.warning'),
        this.translate.get('set-password.fill-corectly'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    }
    this.isLoading = false;
  }
}
