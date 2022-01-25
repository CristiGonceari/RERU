import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { SetPassword } from 'projects/core/src/app/utils/models/set-password.model';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';
import { UserService } from 'projects/core/src/app/utils/services/user.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-set-password-user',
  templateUrl: './set-password-user.component.html',
  styleUrls: ['./set-password-user.component.scss']
})
export class SetPasswordUserComponent implements OnInit {
  passwordForm: FormGroup;
  isLoading: boolean = true;
  setPasswordData: SetPassword;
  title: string;
	description: string;

  constructor(
    private activatedRoute: ActivatedRoute,
		public translate: I18nService,
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
        this.router.navigate(['../../'], { relativeTo: this.activatedRoute });
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

  getUserById(id: string) {
    this.userService.getUser(id).subscribe(response => this.initForm(response));
  }
}
