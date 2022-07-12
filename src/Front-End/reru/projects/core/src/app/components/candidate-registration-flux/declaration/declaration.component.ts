import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { RegistrationFluxStepEnum } from '../../../utils/models/registrationFluxStep.enum';
import { ProfileService } from '../../../utils/services/profile.service';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DataService } from '../data.service';

@Component({
  selector: 'app-declaration',
  templateUrl: './declaration.component.html',
  styleUrls: ['./declaration.component.scss']
})
export class DeclarationComponent implements OnInit {
  
  @Output() counterChange = new EventEmitter<boolean>();

  isChecked: boolean = false;
  userId;
  stepId;
  userRegistrationFluxStepData;
  registrationFluxStep;
  registrationFluxSteps: [];
  unfinishedSteps;
  allowToFinish: boolean;

  constructor(
    private route: ActivatedRoute,
    private registrationFluxService: RegistrationFluxStepService,
    private notificationService: NotificationsService,
    private router: Router,
    private ds: DataService,
    private profileService: ProfileService
    ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);
    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop())

    this.getExistentStep(this.stepId);
    this.getUnfinishedSteps();
  }

  ngOnDestroy(){
    // clear message
    this.ds.clearData();
  }

  getExistentStep(step){
    const request = {
      userProfileId : this.userId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  getUnfinishedSteps(){
    this.profileService.GetCandidateRegistrationSteps().subscribe(res => {
      this.unfinishedSteps = res.data.unfinishedSteps;

      if(this.unfinishedSteps.length <= 1 && this.unfinishedSteps[0] == this.stepId){
        this.allowToFinish = true;
      }else{
        this.allowToFinish = false;
      }
      
    });
  }

  addRegistrationFluxStep(){
    if(this.isChecked){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.userId);

        this.counterChange.emit(this.isChecked == true);
       this.router.navigate(["../../../../"], { relativeTo: this.route });
    }else{
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.userId);
    }
  }

  checkRegistrationStep(stepData, stepId, success, userId){
    const datas= {
      isDone: success,
      stepId: this.stepId
    }
    if(stepData.length == 0){
      this.addCandidateRegistationStep(success, stepId, userId);
      this.ds.sendData(datas);
    }else{
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, userId);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, userId ){
    const request = {
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, userId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      userProfileId: userId 
    }
    
    this.registrationFluxService.update(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }


}
