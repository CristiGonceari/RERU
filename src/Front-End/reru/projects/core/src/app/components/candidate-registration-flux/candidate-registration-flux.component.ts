import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {
  ApplicationUserService,
  AvailableModulesService,
  ApplicationUserModuleModel,
} from '@erp/shared';

import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../utils/services/i18n.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { GeneralDataFormComponent } from '../general-data-form/general-data-form.component';
import { UserService } from '../../utils/services/user.service';
import { RegistrationFluxStepService } from '../../utils/services/registration-flux-step.service';
import { RegistrationFluxStepEnum } from '../../utils/models/registrationFluxStep.enum';
import { NotificationUtil } from '../../utils/util/notification.util';
import { MatHorizontalStepper, MatStepper } from '@angular/material/stepper';
import { GeneralDataFormComponent } from './general-data-form/general-data-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ReferenceService } from '../../utils/services/reference.service';
import { ProfileService } from '../../utils/services/profile.service';
import { DataService } from './data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-candidate-registration-flux',
  templateUrl: './candidate-registration-flux.component.html',
  styleUrls: ['./candidate-registration-flux.component.scss']
})

export class CandidateRegistrationFluxComponent implements OnInit {
  
  @ViewChild('stepper') stepper: MatStepper;

  counter;
  stepsEnum = RegistrationFluxStepEnum;
  state: string;
  userId;
  steps;
  stepId;
  stepperEvent: any;
  public selectedStepIndex: number = 0;
  public loadingStep: boolean = false;
  public isLoading: boolean = true;

  childComponent;

  registrationFluxStepsEnum;
  registrationFluxStepsData;

  dataPassed: any;
  subscription: Subscription;
  
  constructor(
    public notificationService: NotificationsService,
    public translate: I18nService,
    private router: Router,
    private route: ActivatedRoute,
    private referenceService: ReferenceService,
    private registrationFluxService: RegistrationFluxStepService,
    private profileService: ProfileService,
    private ds: DataService
    ) { 
      this.subscription = this.ds.getData().subscribe((x) => {
      this.dataPassed = x;
      
      if(this.dataPassed){
        var stepIndex = this.steps.findIndex(x => x.value == this.dataPassed.stepId );
        this.steps[stepIndex].isDone = this.dataPassed.isDone;
      }
      
    }); }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = parseInt(params.id);
    });

    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.getStepEnum();
    this.getStepssSelectValue();

    const event = {
      selectedIndex: this.route['_routerState'].snapshot.url.split("/").pop() - 1,
      previouslySelectedIndex: "-1"
    };
    
    this.stepperEvent = event;
    
    this.selectionChanged(event);
    this.isLoading = false;
    this.navigate(1)
  }
  
  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
  }
    
  ngAfterViewInit() {
      setTimeout(()=>{
        this.stepper.selectedIndex = this.stepperEvent.selectedIndex; 
      },0);
      
  }

  parseInt(value){
    return parseInt(value);
        
  }

  selectionChanged(event: any) {
      
      this.selectedStepIndex = event.selectedIndex;
      
      if (event.previouslySelectedIndex > event.selectedIndex) {
          this.loadingStep = true;
          setTimeout(() => {
              this.navigate(this.selectedStepIndex);
              this.loadingStep = false;
          }, 1);
      } else {
          this.navigate(this.selectedStepIndex);
      }
  }

  private navigate(step): void {
          this.router.navigate(["./",step + 1], { relativeTo: this.route });
  }

  getStepEnum(){
    let step = [];

    this.profileService.getCandidateRegistrationSteps().subscribe(res => {
      step = res.data.checkedSteps;
      step.sort(function(a, b){return a.value - b.value});
      this.steps = step;
    })
  }

  getStepssSelectValue(){
    this.referenceService.getRegistrationFluxStepsEnum().subscribe(res => {
      this.registrationFluxStepsEnum = res.data;
    })
  }

  endRegistrationFluxStep(steps, existentSteps){

    for (let i = 0; i < steps.length; i++){

        for(let i = 0; i < existentSteps.length; i ++){

            if(steps[i].step == existentSteps[i].step && existentSteps[i].isDone == true){

            }else{
              this.router.navigate([this.userId,"step",steps[i].value], { relativeTo: this.route });
              break;
            }
        }
    }
  }
}

