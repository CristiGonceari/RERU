import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends EnterSubmitListener implements OnInit {
  departmentForm: FormGroup;
  isLoading: boolean;
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
    this.initForm();
  }

  //commented for future use
  submit() {
    // this.departmentService.add(this.departmentForm.value).subscribe((response: ApiResponse<any>) => {
    //   this.ngZone.run(() => this.router.navigate(['../', response.data], { relativeTo: this.route }));
    //   this.notificationService.success('Success', 'Department created!', NotificationUtil.getDefaultMidConfig());
    // }, (error) => {
    //   if (error.status === 400) {
    //     this.notificationService.warn('Validation error', 'Please fill in department name!', NotificationUtil.getDefaultMidConfig());
    //     return;
    //   }
    //   this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    // });
  }

  initForm(): void {
    this.departmentForm = this.fb.group({
      name: this.fb.control('', [Validators.required]),
      description: this.fb.control('', [])
    });
  }

}
