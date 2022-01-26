import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { DepartmentModel } from '../../../utils/models/department.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit {
  departmentForm: FormGroup;
  isLoading: boolean = true;
  department: DepartmentModel;
  constructor(private fb: FormBuilder,
    private departmentService: DepartmentService,
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
        this.retrieveDepartment(response.id);
      }
    })
  }

  submit() {
    this.departmentService.update(this.departmentForm.value).subscribe((response: ApiResponse<any>) => {
      this.navigateBack();
      this.notificationService.success('Success', 'Department updated!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Validation error', 'Please fill in department name!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    });
  }

  navigateBack(): void {
    if (history.length > 2) {
      history.back();
    } else {
      this.router.navigate(['../../'], { relativeTo: this.route });
    }
  }

  initForm(data): void {
    this.departmentForm = this.fb.group({
      id: this.fb.control(data && data.id || null, [Validators.required]),
      name: this.fb.control(data && data.name || null, [Validators.required]),
      description: this.fb.control(data && data.description, [])
    });
  }

  retrieveDepartment(id: number): void {
    this.departmentService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.initForm(response.data);
      this.department = response.data;
      this.isLoading = false;
    });
  }

}
