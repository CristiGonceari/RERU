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
import { I18nService } from '../../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';

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
  contractorId;

  userGeneralData;
  autobiographyData;
  registrationFluxStep;

  title: string;
  description: string;

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
    private ds: DataService,
    public translate: I18nService,
  ) { }

  ngOnInit(): void {
    this.userId = parseInt(this.route['_routerState'].snapshot.url.split("/")[2]);

    this.stepId = parseInt(this.route['_routerState'].snapshot.url.split("/").pop());

    this.initForm();
    this.getUserGeneralData();
  }

  initForm(data?): void {

    if (data == null) {

      this.autobiographyForm = this.fb.group({
        id: this.fb.control(null, []),
        text: this.fb.control(null, []),
        contractorId: this.fb.control(null, [])
      });

    }
    else {
      this.autobiographyForm = this.fb.group({
        id: this.fb.control((data && data.id) || null, []),
        text: this.fb.control((data && data.text) || null, []),
        contractorId: this.fb.control(data.contractorId || null, [])
      });

    }
  }

  getUserGeneralData() {

    this.userProfile.getCandidateProfile(this.userId).subscribe(res => {

      this.userGeneralData = res.data;
      this.contractorId = res.data.contractorId;

      const userData = res.data;

      if (userData.autobiographyId != 0) {

        this.getUserAutobiography(this.contractorId);
      }
      else {
        this.addOrEditAutobiographyButton = false;
        this.isLoadingAutobiography = false;
        this.autobiographyData = null;
      }

      this.getExistentStep(this.stepId, this.contractorId);
    })
  }

  getUserAutobiography(contractorId) {
    this.isLoadingAutobiography = true;

    this.autobiographyService.get(contractorId).subscribe(res => {
      this.autobiographyData = res.data;
      this.initForm(this.autobiographyData);
      this.isLoadingAutobiography = false;
      this.addOrEditAutobiographyButton = true;
    })

  }

  createAutobiography() {

    this.autobiographyService.add(this.parseAutobiography(this.autobiographyForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.create-autobiography-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.create-autobiography-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  parseAutobiography(data, contractorId): AutobiographyModel {

    return ObjectUtil.preParseObject({
      id: data.id,
      text: data.text,
      contractorId: contractorId
    })
  }

  updateAutobiography() {
    this.autobiographyService.update(this.parseAutobiography(this.autobiographyForm.value, this.contractorId)).subscribe(res => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('candidate-registration-flux.update-autobiography-success'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getUserAutobiography(this.contractorId);
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.update-autobiography-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  getExistentStep(step, contractorId) {
    const request = {
      contractorId: contractorId,
      step: step
    };

    this.registrationFluxService.get(request).subscribe(res => {
      this.registrationFluxStep = res.data;
    })
  }

  addRegistrationFluxStep() {
    if (this.autobiographyData != null) {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, true, this.contractorId);
    }
    else {
      this.checkRegistrationStep(this.registrationFluxStep, this.stepId, false, this.contractorId, true);
    }
  }

  checkRegistrationStep(stepData, stepId, success, contractorId, inProgress?) {
    const datas = {
      isDone: success,
      stepId: this.stepId,
      inProgress: inProgress
    }
    if (stepData.length == 0) {
      this.addCandidateRegistationStep(success, stepId, contractorId, inProgress);
      this.ds.sendData(datas);
    } else {
      this.updateCandidateRegistationStep(stepData[0].id, success, stepId, contractorId, inProgress);
      this.ds.sendData(datas);
    }
  }

  addCandidateRegistationStep(isDone, step, contractorId, inProgress?) {
    const request = {
      isDone: isDone,
      step: step,
      contractorId: contractorId,
      inProgress: inProgress
    }
    this.registrationFluxService.add(request).subscribe(res => {
      if (!inProgress) {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('candidate-registration-flux.step-success'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      } else {
        forkJoin([
          this.translate.get('step-status.in-progress'),
          this.translate.get('candidate-registration-flux.step-in-progress'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      }
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

  updateCandidateRegistationStep(id, isDone, step, contractorId, inProgress?) {
    const request = {
      id: id,
      isDone: isDone,
      step: step,
      contractorId: contractorId,
      inProgress: inProgress
    }

    this.registrationFluxService.update(request).subscribe(res => {
      if (!inProgress) {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('candidate-registration-flux.step-success'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      } else {
        forkJoin([
          this.translate.get('step-status.in-progress'),
          this.translate.get('candidate-registration-flux.step-in-progress'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.warn(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      }
    }, error => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('candidate-registration-flux.step-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    })
  }

}
