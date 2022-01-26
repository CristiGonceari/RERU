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

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit {
  organigramForm: FormGroup;
  isLoading: boolean = true;
  constructor(private fb: FormBuilder,
              private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              private notificationService: NotificationsService,
              public location: Location) {
    super();
    this.callback = this.submit;
  }

  ngOnInit(): void {
    this.subscribeForParams();
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

  submit(): void {
    this.organigramService.edit(this.organigramForm.value).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.ngZone.run(() => this.router.navigate(['../../', this.organigramForm.get('id').value], { relativeTo: this.route }));
        this.notificationService.success('Success', 'Organigram updated!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.warn('Error', 'An error occured!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Error', 'Validation service error!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
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
