import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { SetPassword } from '../../../utils/models/set-password.model';
import { Location } from '@angular/common';

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

  constructor(
    private activatedRoute: ActivatedRoute,
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

  initForm(user): void {
    this.passwordForm = this.fb.group({
      id: this.fb.control(user.id, [Validators.required]),
      password: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&].{6,}')]),
      repeatNewPassword: this.fb.control('', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&].{6,}')]),
      emailNotification: this.fb.control(false, [Validators.required])
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
          this.notificationService.success('Success',
            `Password for ${this.userData.name} ${this.userData.lastName} has been set successfully!`,
            NotificationUtil.getDefaultMidConfig()
          );
          this.back();
        },
        (err) => {
          this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
        }
      );
    }
    else {
      this.notificationService.warn('Warning', 'New password and Repeat password are not the same!', NotificationUtil.getDefaultMidConfig());
      this.isLoading = false;
    }
  }

  getUserById(id: string) {
    this.userService.getUser(id).subscribe(response => {
      this.initForm(response);
      this.userData = response.data;
    });
  }

  back(): void {
    this.location.back();
  }
}
