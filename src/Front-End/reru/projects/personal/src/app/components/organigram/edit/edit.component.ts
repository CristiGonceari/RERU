import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { OrganigramService } from '../../../utils/services/organigram.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { Location } from '@angular/common';
import { OrganigramModel } from '../../../utils/models/organigram.model';
import { I18nService } from '../../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit {
  organigramForm: FormGroup;
  isLoading: boolean = true;
  notification = {
    success: 'Success',
    error: 'Error',
    validationServiceError: 'Validation service error!',
    organigramUpdated: 'Organigram updated!',
    serverError: 'Server error occured!',
    anError: 'An error occured!'
  };
  constructor(private fb: FormBuilder,
              private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              public translate: I18nService,
              private notificationService: NotificationsService,
              public location: Location) {
    super();
    this.callback = this.submit;
  }

  ngOnInit(): void {
    this.subscribeForParams();
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrieveOrganigram(response.id);
      }
    })
  }

  retrieveOrganigram(id: number): void {
    this.organigramService.get(id).subscribe(response => {
      this.initForm(response.data);
      this.isLoading = false;
    });
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('validations.validations-service-error'),
      this.translate.get('organigram.organigram-updated'),
      this.translate.get('organigram.an-error'),
      this.translate.get('organigram.server-error'),
    ]).subscribe(([success, error, validationServiceError, organigramUpdated, anError, serverError]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.validationServiceError = validationServiceError;
      this.notification.organigramUpdated = organigramUpdated;
      this.notification.anError = anError;
      this.notification.serverError = serverError;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  submit(): void {
    this.organigramService.edit(this.organigramForm.value).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.ngZone.run(() => this.router.navigate(['../../', this.organigramForm.get('id').value], { relativeTo: this.route }));
        this.notificationService.success(this.notification.success, this.notification.organigramUpdated, NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.warn(this.notification.error, this.notification.anError, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn(this.notification.error, this.notification.validationServiceError, NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error(this.notification.serverError, null, NotificationUtil.getDefaultMidConfig());
    });
  }

  initForm(organigram: OrganigramModel = <any>{}): void {
    this.organigramForm = this.fb.group({
      id: this.fb.control(organigram.id, []),
      name: this.fb.control(organigram.name, [Validators.required]),
      fromDate: this.fb.control(organigram.fromDate, [Validators.required]),
    });
  }
}
