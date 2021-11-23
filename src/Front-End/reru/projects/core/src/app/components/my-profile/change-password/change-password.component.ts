import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { ChangePassword } from '../../../utils/models/change-password.model';
import { Router } from '@angular/router';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})

export class ChangePasswordComponent implements OnInit {
  
  passwordForm: FormGroup;
  isLoading = true;
  changePasswordData: ChangePassword;

  constructor(
    private fb: FormBuilder,
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
          this.notificationService.success('Success', 'Password has been updated!', NotificationUtil.getDefaultMidConfig());
          this.isLoading = false;
        })
      } else if (passwordForUpdate.newPassword !== passwordForUpdate.repeatPassword){
        this.notificationService.warn('Warning', 'New password and Repeat password are not the same!', NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
      } else if (passwordForUpdate.oldPassword === passwordForUpdate.newPassword){
        this.notificationService.warn('Warning', 'New password and Old password are the same!', NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
      }
  }

  changePassword(): void {
    this.isLoading = true;
    if (this.passwordForm.touched && this.passwordForm.valid) {
      this.checkPassword();
    } else {
      this.notificationService.warn('Warning', 'Fill in all the fields correctly!', NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    }
    this.isLoading = false;
  }
}
