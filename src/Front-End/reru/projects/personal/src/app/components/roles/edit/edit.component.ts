import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { RoleModel } from '../../../utils/models/role.model';
import { RoleService } from '../../../utils/services/role.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit {
  isLoading: boolean = true;
  roleForm: FormGroup;
  role: RoleModel;
  constructor(private fb: FormBuilder,
    private roleService: RoleService,
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private notificationService: NotificationsService) {
      super();
      this.callback = this.submit;
     }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id && !isNaN(response.id)) {
        this.retrieveRole(response.id);
      }
    })
  }

  submit() {
    this.roleService.update(this.roleForm.value).subscribe((response: ApiResponse<any>) => {
      this.ngZone.run(() => this.router.navigate(['../../', this.role.id], { relativeTo: this.route }));
      this.notificationService.success('Success', 'Role updated!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Validation error', 'Please fill in role name!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    });
  }

  initForm(data): void {
    this.roleForm = this.fb.group({
      id: this.fb.control(data && data.id || null, [Validators.required]),
      name: this.fb.control(data && data.name || null, [Validators.required]),
      description: this.fb.control('', []),
      code: this.fb.control(data && data.code || null, [Validators.required]),
      shortCode: this.fb.control(data && data.shortCode || null, [Validators.required])
    });
  }

  retrieveRole(id: number): void {
    this.roleService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.initForm(response.data);
      this.role = response.data;
      this.isLoading = false;
    });
  }
}
