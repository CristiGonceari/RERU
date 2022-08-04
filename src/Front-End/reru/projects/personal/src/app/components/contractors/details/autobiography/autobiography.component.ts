import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AutobiographyService } from 'projects/personal/src/app/utils/services/autobiography.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { NotificationsService } from 'angular2-notifications';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { AutobiographyModel } from 'projects/personal/src/app/utils/models/autobiography.model';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { RegistrationFluxStepService } from 'projects/personal/src/app/utils/services/registration-flux-step.service';
import { DataService } from '../data.service';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ApiResponse } from 'projects/personal/src/app/utils/models/api-response.model';


@Component({
  selector: 'app-autobiography',
  templateUrl: './autobiography.component.html',
  styleUrls: ['./autobiography.component.scss']
})
export class AutobiographyComponent implements OnInit {
  @Input() contractor: Contractor;

  public Editor = DecoupledEditor;

  autobiographyForm: FormGroup;

  addOrEditAutobiographyButton: boolean;
  isLoadingAutobiography: boolean = true;
  isLoading: boolean = true;

  stepId;
  contractorId;
  steps;

  userGeneralData;
  autobiographyData;
  registrationFluxStep;

  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private notificationService: NotificationsService,
    private autobiographyService: AutobiographyService,
    private registrationFluxService: RegistrationFluxStepService,
    private ds: DataService,
    private contractorService: ContractorService,
  ) { }

  ngOnInit(): void {
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.subscribeForParams();
  }

  initForm(data?): void {
    
    if(data == null){

      this.autobiographyForm = this.fb.group({
        id: this.fb.control( null, []),
        text: this.fb.control( null, []),
        contractorId: this.fb.control( null, [])
      });

    }
    else{
      this.autobiographyForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) || null, []),
        contractorId: this.fb.control(data.contractorId || null, [])
      });

    }
  }

  subscribeForParams(){
    this.getUser(this.contractorId);

    this.subscribeForContractorSteps(this.contractor.id);
    this.isLoading = false;
  }

  subscribeForData(contractor) {
      if (contractor.hasAutobiography) {
        this.getUserAutobiography(contractor.id);
      }
      else {
        this.addOrEditAutobiographyButton = false;
        this.isLoadingAutobiography = false;
        this.autobiographyData = null;
      }
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.contractor = response.data;
      this.subscribeForData(response.data)
    });
  }

  subscribeForContractorSteps(contractorId: number){
    this.contractorService.getCandidateSteps(contractorId).subscribe(res => {
      this.steps = res.data.unfinishedSteps;
    });
  }

  getUserAutobiography(contractorId){
    this.isLoadingAutobiography = true;

    this.autobiographyService.get(contractorId).subscribe(res => {
      this.autobiographyData = res.data;
      this.initForm(this.autobiographyData);
      this.isLoadingAutobiography = false;
      this.addOrEditAutobiographyButton = true;
    })
    
  }

  createAutobiography(){
    
    this.autobiographyService.add(this.parseAutobiography(this.autobiographyForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Autobiography was added!', NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractor.id);
    }, error => {
      this.notificationService.error('Error', 'Autobiography was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  parseAutobiography(data, contractorId): AutobiographyModel {
    
    return ObjectUtil.preParseObject({
      id: data.id,
      text: data.text,
      contractorId: contractorId
    })
  }

  updateAutobiography(){
    this.autobiographyService.update(this.parseAutobiography(this.autobiographyForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success('Success', 'Autobiography was updated!', NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractor.id);
    }, error => {
      this.notificationService.error('Error', 'Autobiography was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }

  getExistentStep(step, contractorId){
    const request = {
      contractorId : contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep(){
    if(this.autobiographyData != null){
      this.checkRegistrationStep(this.stepId, true, this.contractor.id);
    }
    else{
      this.checkRegistrationStep(this.stepId, false, this.contractor.id);
    }
  }

  checkRegistrationStep(stepId, success, contractorId){

    this.steps.forEach(el => {
      this.getExistentStep(el, contractorId);

      const datas= {
        isDone: success,
        stepId: el
      };

    if(this.steps.length == 2 && (el == stepId || el == stepId + 1))
      {
        if (this.registrationFluxStep == null){
        this.addCandidateRegistationStep(success, el, contractorId);
        this.ds.sendData(datas);
        }else{
          this.updateCandidateRegistationStep(this.registrationFluxStep.id, success, el, contractorId);
          this.ds.sendData(datas);
        }
      }else{
      this.notificationService.error('Error', 'Need to complete step ' + el + '!', NotificationUtil.getDefaultMidConfig());
      }
    });
  }

  addCandidateRegistationStep(isDone, step, contractorId ){
    const request = {
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    this.registrationFluxService.add(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was added!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not added!', NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId ){
    const request = {
      id: id,
      isDone: isDone,
      step : step,
      contractorId: contractorId 
    }
    
    this.registrationFluxService.update(request).subscribe(res => {
      this.notificationService.success('Success', 'Step was updated!', NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error('Error', 'Step was not updated!', NotificationUtil.getDefaultMidConfig());
    })
  }
}
