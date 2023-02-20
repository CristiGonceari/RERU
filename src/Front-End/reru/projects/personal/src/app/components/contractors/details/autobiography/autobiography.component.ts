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
import { I18nService } from 'projects/personal/src/app/utils/services/i18n.service';
import { forkJoin } from 'rxjs';


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

  notifications = {
    success: 'Success',
    error: 'Error',
    autobiographySuccesAdd:  'Autobiography was added!',
    autobiographyErrorAdd:  'Autobiography was not added!',
    autobiographySuccesUpdate:  'Autobiography was updated!',
    autobiographyErrorUpdate:  'Autobiography was not updated!',
    needCompleteStep: 'Need to complete step ',
    stepSuccesAdd: 'Step was added!',
    stepErrorAdd:  'Step was not added!',
    stepSuccesUpdate: 'Step was updated!',
    stepErrorUpdate:  'Step was not updated!'
  }

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
    private translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.contractorId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId =parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.subscribeForParams();
    
		this.translateData();
		this.subscribeForLanguageChange();
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

  translateData(): void {
    forkJoin([
      this.translate.get('notification.title.success'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.body.success.autobiography-succes-add-msg'),
      this.translate.get('notification.body.autobiography-error-add'),
      this.translate.get('notification.body.success.autobiography-succes-update-msg'),
      this.translate.get('notification.body.autobiography-error-update'),
      this.translate.get('notification.body.need-complete-step'),
      this.translate.get('notification.body.success.step-succes-add-msg'),
      this.translate.get('notification.body.step-error-add'),
      this.translate.get('notification.body.success.step-succes-update-msg'),
      this.translate.get('notification.body.step-error-update'),
    ]).subscribe(([success, error, autobiographySuccesAdd, autobiographyErrorAdd, autobiographySuccesUpdate, 
      autobiographyErrorUpdate, needCompleteStep, stepSuccesAdd, stepErrorAdd,stepSuccesUpdate, stepErrorUpdate]) => {
      this.notifications.success = success;
      this.notifications.error = error;
      this.notifications.autobiographySuccesAdd = autobiographySuccesAdd;
      this.notifications.autobiographyErrorAdd = autobiographyErrorAdd;
      this.notifications.autobiographySuccesUpdate = autobiographySuccesUpdate;
      this.notifications.autobiographyErrorUpdate = autobiographyErrorUpdate;
      this.notifications.needCompleteStep = needCompleteStep;
      this.notifications.stepSuccesAdd = stepSuccesAdd;
      this.notifications.stepErrorAdd = stepErrorAdd;
      this.notifications.stepSuccesUpdate = stepSuccesUpdate;
      this.notifications.stepErrorUpdate = stepErrorUpdate;
    });
  }

  subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  createAutobiography(){
    
    this.autobiographyService.add(this.parseAutobiography(this.autobiographyForm.value, this.contractor.id)).subscribe(res => {
      this.notificationService.success(this.notifications.success, this.notifications.autobiographySuccesAdd, NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractor.id);
    }, error => {
      this.notificationService.error(this.notifications.error, this.notifications.autobiographyErrorAdd, NotificationUtil.getDefaultMidConfig());
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
      this.notificationService.success(this.notifications.success, this.notifications.autobiographySuccesUpdate, NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractor.id);
    }, error => {
      this.notificationService.error(this.notifications.error, this.notifications.autobiographyErrorUpdate, NotificationUtil.getDefaultMidConfig());
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
      this.notificationService.error(this.notifications.error, this.notifications.needCompleteStep + el + '!', NotificationUtil.getDefaultMidConfig());
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
      this.notificationService.success(this.notifications.success, this.notifications.stepSuccesAdd, NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error(this.notifications.error, this.notifications.stepErrorAdd, NotificationUtil.getDefaultMidConfig());
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
      this.notificationService.success(this.notifications.success, this.notifications.stepSuccesUpdate, NotificationUtil.getDefaultMidConfig());
    }, error => {
      this.notificationService.error(this.notifications.error, this.notifications.stepErrorUpdate, NotificationUtil.getDefaultMidConfig());
    })
  }
}
