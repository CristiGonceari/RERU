import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { User } from 'projects/core/src/app/utils/models/user.model';
import { UserService } from 'projects/core/src/app/utils/services/user.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {
  userForm: FormGroup;
  isLoading: boolean = true;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router,
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
    this.userForm = this.fb.group({
      id: this.fb.control(user.data.id, [Validators.required]),
      avatar: this.fb.control(user.data.avatar, [Validators.required]),
      name: this.fb.control(user.data.name, [Validators.required]),
      lastName: this.fb.control(user.data.lastName, [Validators.required]),
      email: this.fb.control(user.data.email, [Validators.required]),
      username: this.fb.control(user.data.username, [Validators.required])
    });
    this.isLoading = false;
  }

  parseRequest(data: User): User {
    return {
      ...data,
    };
  }

  editUser(): void {
    const userForUpdate = this.parseRequest(this.userForm.value);

    this.userService.editUser(userForUpdate).subscribe(
      (res) => {
        this.notificationService.success('Success', 'User has been updated successfully!', NotificationUtil.getDefaultMidConfig());
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
