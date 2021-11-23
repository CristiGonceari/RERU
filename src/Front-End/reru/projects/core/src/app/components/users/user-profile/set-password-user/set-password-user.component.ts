import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SetPassword } from 'projects/core/src/app/utils/models/set-password.model';
import { UserService } from 'projects/core/src/app/utils/services/user.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';

@Component({
  selector: 'app-set-password-user',
  templateUrl: './set-password-user.component.html',
  styleUrls: ['./set-password-user.component.scss']
})
export class SetPasswordUserComponent implements OnInit {
  passwordForm: FormGroup;
  isLoading: boolean = true;
  setPasswordData: SetPassword;

  constructor(
    private activatedRoute: ActivatedRoute,
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
      if (params.id) {
        this.getUserById(params.id);
      }
    });
  }
  initForm(user): void {
    this.passwordForm = this.fb.group({
      id: this.fb.control(user.id, [Validators.required]),
      password: this.fb.control('', [Validators.required]),
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
    // console.log(setPassword)
    this.userService.setPassword(setPassword).subscribe(
      (res) => {
        this.notificationService.success('Success', 'Password has been set successfully!', NotificationUtil.getDefaultMidConfig());
        this.router.navigate(['../../'], { relativeTo: this.activatedRoute });
      },
      (err) => {
        this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
      }
    );
  }

  getUserById(id: string) {
    this.userService.getUser(id).subscribe(response => this.initForm(response));
  }
}
