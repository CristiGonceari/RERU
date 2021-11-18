import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Plan } from '../../../utils/models/plans/plan.model';

@Component({
  selector: 'app-add-edit-plans',
  templateUrl: './add-edit-plans.component.html',
  styleUrls: ['./add-edit-plans.component.scss']
})
export class AddEditPlansComponent implements OnInit {

  planForm: FormGroup;
  isLoading: boolean;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private planService: PlanService,
              private location: Location,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrievePlan(response.id);
      }
    })
  }

  retrievePlan(id: number): void {
    this.planService.get(id).subscribe(response => {
      this.initForm(response.data);
      this.isLoading = false;
    });
  } 

  initForm(plan: Plan = <any>{}): void {
    this.planForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      tillDate: this.fb.control(null, [Validators.required]),
      description: this.fb.control(null, [Validators.required])
    });
  }

  onSave(): void {
		if (this.planForm) {
			this.addPlan();
		} else {
			this.editPlan();
		}
	}
  
  addPlan(): void {
    console.log("this.planFormValue:", this.planForm.value)
    this.planService.add({data: this.planForm.value}).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Plan was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  editPlan(): void {
    this.planService.edit({data: this.planForm.value}).subscribe(() => {
      this.back();
      this.notificationService.success('Success', 'Plan was successfully edited', NotificationUtil.getDefaultMidConfig());
    });
  }

  back(){
    this.location.back();
  }
}
