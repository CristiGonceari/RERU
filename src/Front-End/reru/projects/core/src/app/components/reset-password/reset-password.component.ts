import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { User } from '../../utils/models/user.model';
import { I18nService } from '../../utils/services/i18n.service';
import { UserService } from '../../utils/services/user.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../utils/util/notification.util';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  currentLanguage: string;
  userForm: FormGroup;

  isLoading: boolean = false;
  isMain: boolean = false;
  isProfileOverview: boolean = false;
  isEmailVerification: boolean = false;
  isResetedPassword: boolean = false;

  code: number;
  languageList = [
    { code: 'en', label: 'English' },
    { code: 'ro', label: 'Română' },
    { code: 'ru', label: 'Русский' },
  ];

  email: string;
  userDetails: User;

  title: string;
  description: string;

  constructor(
    public translate: I18nService,
    public userService: UserService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private notificationService: NotificationsService,
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.isMain = true;
  }

  initForm(): void {
    this.userForm = this.fb.group({
      email: this.fb.control(null, [Validators.required, Validators.pattern("([!#-'*+/-9=?A-Z^-~-]+(\.[!#-'*+/-9=?A-Z^-~-]+)*|\"\(\[\]!#-[^-~ \t]|(\\[\t -~]))+\")@((?!mail.ru|yandex.ru).)([!#-'*+/-9=?A-Z^-~-]+(\.[!#-'*+/-9=?A-Z^-~-]+)*|\[[\t -Z^-~]*])")]),
    });
  }

  getUser() {
    this.isLoading = true;
    this.userService.getUserPersonalDetails(this.userForm.value.email).subscribe(res => {
      if (res && res.data) {
        this.userDetails = res.data;
        this.isLoading = false;
        this.isMain = false;
        this.isProfileOverview = true;
        
        forkJoin([
          this.translate.get('notification.title.success'),
          this.translate.get('notification.body.get-user-success'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      }
    }, error => {

      forkJoin([
        this.translate.get('notification.title.error'),
        this.translate.get('notification.body.get-user-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());

      this.isLoading = false;
      this.isMain = true;
      this.isProfileOverview = false;
    })
  }

  emailVerification() {
    this.isLoading = true;
    this.code = null;

    let data = {
      email: this.userDetails.email,
      forReset: true
    };

    this.userService.verifyEmail(data).subscribe(res => {
      this.isLoading = false;
      this.isProfileOverview = false;
      this.isEmailVerification = true;

      forkJoin([
        this.translate.get('notification.title.success'),
        this.translate.get('notification.body.email-verification-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.isLoading = false;
      this.isProfileOverview = true;
      this.isEmailVerification = false;

      forkJoin([
        this.translate.get('notification.title.error'),
        this.translate.get('notification.body.email-verification-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  resetPassword() {
    this.isLoading = true;

    let data = {
      code: this.code,
      email: this.userDetails.email
    }

    this.userService.resetPasswordByEmailCode(data).subscribe(res => {
      this.isLoading = false;
      this.isEmailVerification = false;
      this.isResetedPassword = true;

      forkJoin([
        this.translate.get('notification.title.success'),
        this.translate.get('notification.body.reset-password-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      if (res && res.data) {
        setTimeout(() => {
          this.router.navigate(['../'], { relativeTo: this.route });
          res();
        }, 5000);
      }
    }, error => {
      this.isLoading = false;
      this.isEmailVerification = true;
      this.isResetedPassword = false;

      forkJoin([
        this.translate.get('notification.title.error'),
        this.translate.get('notification.body.reset-password-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  ckeckCode(code) {
    if (code == null) {
      return true;
    } else {
      return code.split('').length == 4 ? false : true;
    }
  }

  getLang(): string {
    this.currentLanguage = this.translate.currentLanguage;

    const value = this.languageList.find(l => l.code == this.currentLanguage);

    return (value.label) || "Language";
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }

  reloadPage() {
    window.location.reload();
  }
}
