import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Plan } from '../../../utils/models/plans/plan.model';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-add-edit-plans',
  templateUrl: './add-edit-plans.component.html',
  styleUrls: ['./add-edit-plans.component.scss']
})
export class AddEditPlansComponent implements OnInit {

  planForm: FormGroup;
  isEditForm: boolean = false;
  isLoading: boolean;

  title: string;
  description: string;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
	  public translate: I18nService,
    private planService: PlanService,
    private location: Location,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
	  this.planForm = new FormGroup({ name: new FormControl() });
    this.initForm();
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.isEditForm = true;
        this.retrievePlan(response.id);
      } else {
         this.initForm();
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
      id: this.fb.control(this.isEditForm? plan.id : null, []),
      name: this.fb.control(this.isEditForm? plan.name : null, [Validators.required]),
      fromDate: this.fb.control(this.isEditForm? plan.fromDate : null, [Validators.required]),
      tillDate: this.fb.control(this.isEditForm? plan.tillDate : null, [Validators.required]),
      description: this.fb.control(this.isEditForm? plan.description : null)
    });
  }

  onSave(): void {
		if (!this.isEditForm) {
			this.addPlan();
		} else {
			this.editPlan();
		}
	}
  
  addPlan(): void {
    this.planService.add({data: this.planForm.value}).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('plans.succes-add-plan-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.back();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  editPlan(): void {
    this.planService.edit({data: this.planForm.value}).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('plans.succes-edit-plan-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
        });
      this.back();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  back(){
    this.location.back();
  }
}
