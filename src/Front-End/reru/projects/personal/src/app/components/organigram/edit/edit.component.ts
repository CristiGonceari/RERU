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
    successEdit: 'Organigram has been edited successfully',
    errorEdit: 'Organigram was not edited successfully',
    serverWarn: 'Server error occured!'
  };

  constructor(private fb: FormBuilder,
              private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              private notificationService: NotificationsService,
              public location: Location,
              public translate: I18nService) {
    super();
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
      this.translate.get('organigram.succes-edit-organigram'),
      this.translate.get('organigram.error-edit-organigram'),
      this.translate.get('organigram.server-warn')
    ]).subscribe(([success, error, successEdit, errorEdit, serverWarn]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.successEdit = successEdit;
      this.notification.errorEdit = errorEdit;
      this.notification.serverWarn = serverWarn;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  submit(): void {
    this.organigramService.edit(this.organigramForm.value).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.ngZone.run(() => this.router.navigate(['../../', this.organigramForm.get('id').value], { relativeTo: this.route }));
        return;
      }
      this.notificationService.success(this.notification.success, this.notification.successEdit, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn(this.notification.error, this.notification.serverWarn, NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error(this.notification.error, this.notification.errorEdit, NotificationUtil.getDefaultMidConfig());
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
