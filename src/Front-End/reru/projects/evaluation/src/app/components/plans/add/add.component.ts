import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';



@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  planForm: FormGroup;
  isLoading: boolean;

  constructor(private fb: FormBuilder,
              private planService: PlanService,
              private location: Location,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.planForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      tillDate: this.fb.control(null, [Validators.required]),
      description: this.fb.control(null, [Validators.required])
    });
  }
  
  submit(): void {
    console.log("this.planFormValue:", this.planForm.value)
    this.planService.add({data: this.planForm.value}).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Plan was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  back(){
    this.location.back();
  }

}
