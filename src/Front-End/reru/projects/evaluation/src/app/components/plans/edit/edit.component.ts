import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { Location } from '@angular/common';
import { Plan } from '../../../utils/models/plans/plan.model';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  planForm: FormGroup;
  isLoading: boolean = true;
  
  constructor(private fb: FormBuilder,
    private planService: PlanService,
    private route: ActivatedRoute,
    private notificationService: NotificationsService,
    public location: Location) {
}

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

submit(): void {
  this.planService.edit({data: this.planForm.value}).subscribe(() => {
    this.back();
    this.notificationService.success('Success', 'Plan was successfully edited', NotificationUtil.getDefaultMidConfig());
  });
}

initForm(plan: Plan = <any>{}): void {
  this.planForm = this.fb.group({
    id: this.fb.control(plan.id, []),
    name: this.fb.control(plan.name, [Validators.required]),
    fromDate: this.fb.control(plan.fromDate, [Validators.required]),
    tillDate: this.fb.control(plan.tillDate, [Validators.required]),
    description: this.fb.control(plan.description, [Validators.required])
  });
}

back(){
  this.location.back();
}

}
