import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { AutobiographyService } from '../../../utils/services/autobiography.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { AutobiographyModel } from '../../../utils/models/autobiography.model';
import { RegistrationFluxStepService } from '../../../utils/services/registration-flux-step.service';
import { DataService } from '../data.service';

@Component({
  selector: 'app-autobiography',
  templateUrl: './autobiography.component.html',
  styleUrls: ['./autobiography.component.scss']
})
export class AutobiographyComponent implements OnInit {

  public Editor = DecoupledEditor;

  autobiographyForm: FormGroup;

  addOrEditAutobiographyButton: boolean;
  isLoadingAutobiography: boolean = true;

  userId;
  stepId;
  userGeneralData;
  autobiographyData;
  registrationFluxStep;

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  constructor(private fb: FormBuilder,
      private userProfile: UserProfileService,
      private route: ActivatedRoute,
      private autobiographyService: AutobiographyService,
      private notificationService: NotificationsService,
      private registrationFluxService: RegistrationFluxStepService,
      private ds: DataService
    ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());
    this.getExistentStep(this.stepId);

    this.initForm();
    this.getUserGeneralData();
  }

  initForm(data?): void {
    
    if(data == null){

      this.autobiographyForm = this.fb.group({
        id: this.fb.control( null, []),
        text: this.fb.control( null, []),
        userProfileId: this.fb.control( null, [])
      });

    }
    else{
      this.autobiographyForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) || null, []),
        userProfileId: this.fb.control(data.userProfileId || null, [])
      });

    }
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe( res => {

      this.userGeneralData = res.data;
      const userData = res.data;

      if (userData.autobiographyId != 0) {

        this.getUserAutobiography(this.userGeneralData.id);
      }
      else {
        this.addOrEditAutobiographyButton = false;
        this.isLoadingAutobiography = false;
        this.autobiographyData = null;
      }
      
    })
  }

  getUserAutobiography(userId){
    this.isLoadingAutobiography = true;

    this.autobiographyService.get(userId).subscribe(res => {
      this.autobiographyData = res.data;
      this.initForm(this.autobiographyData);
      this.isLoadingAutobiography = false;
      this.addOrEditAutobiographyButton = true;
    })
    
  }

  createAutobiography(){
    
    this.autobiographyService.add(this.parseAutobiography(this.autobiographyForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Autobiography was added!', NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.userId);
    }, error => {
      this.notificationService.error('Error', 'Autobiography was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  parseAutobiography(data, userId): AutobiographyModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      text: data.text,
      userProfileId: userId
    })
  }

  updateAutobiography(){
    this.autobiographyService.update(this.parseAutobiography(this.autobiographyForm.value, this.userId)).subscribe(res => {
      this.notificationService.success('Success', 'Autobiography was updated!', NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.userId);
    }, error => {
      this.notificationService.error('Error', 'Autobiography was not updated!', NotificationUtil.getDefaultMidConfig());
    })
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

  addRegistrationFluxStep(){
    if(this.autobiographyData != null){
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.userId);
    }
    else{
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
