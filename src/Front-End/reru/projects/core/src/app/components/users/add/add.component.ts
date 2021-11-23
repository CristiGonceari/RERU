import { UserService } from './../../../utils/services/user.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  userForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router,
    private ngZone: NgZone,
    private route: ActivatedRoute,
		private location: Location
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

  initForm(): void {
    this.userForm = this.fb.group({
      name: this.fb.control(null, [Validators.required,Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      lastName: this.fb.control(null, [Validators.required,Validators.pattern('^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'),]),
      email: this.fb.control(null, [Validators.required , Validators.email]),
      emailNotification: this.fb.control(false, [Validators.required])
    });
  }

  addUser(): void {
    this.userService.createUser(this.userForm.value).subscribe(res => {
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      this.notificationService.success('Success', 'User has been created successfully!', NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.notificationService.error('Error', 'A server error occured', NotificationUtil.getDefaultMidConfig());
    });
  }

  back(): void {
		this.location.back();
	}
}
