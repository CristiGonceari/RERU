import { Component, OnInit, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';
import { NomenclatureTypeService } from '../../../utils/services/nomenclature-type.service';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
  selector: 'app-add',
  templateUrl: './add-nomenclature.component.html',
  styleUrls: ['./add-nomenclature.component.scss']
})
export class AddNomenclatureComponent extends EnterSubmitListener implements OnInit {
  isLoading: boolean;
  list: any[];
  nomenclatureForm: FormGroup;
  notification = {
    success: 'Success',
    error: 'Error',
    create: 'Nomenclature created!',
    internal: 'Interval server error!'
  }
  constructor(private fb: FormBuilder,
              private nomenclatureTypeService: NomenclatureTypeService,
              private router: Router,
              private route: ActivatedRoute,
              private ngZone: NgZone,
              private notificationService: NotificationsService,
              private translate: I18nService) {
    super();
    this.callback = this.submit;
  }

  ngOnInit(): void {
    this.translateData();
    this.translate.change.subscribe(() => this.translateData());
    this.initForm();
  }

  initForm(): void {
    this.nomenclatureForm = this.fb.group({
      name: this.fb.control(null, [Validators.required])
    });
  }

  submit(): void {
    this.isLoading = true;
    this.nomenclatureTypeService.add(this.nomenclatureForm.value).subscribe((response: ApiResponse<number>) => {
      if (response.success) {
        this.notificationService.success(this.notification.success, this.notification.create, NotificationUtil.getDefaultMidConfig());
        this.ngZone.run(() => this.router.navigate(['../', response.data], { relativeTo: this.route }));
      }
    }, () => {
      this.notificationService.error(this.notification.error, null, NotificationUtil.getDefaultMidConfig());
    })
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('notification.nomenclature-create'),
      this.translate.get('notification.internal')
    ]).subscribe(([success, error, create, internal]) => {
      this.notification = { success, error, create, internal };
    });
  }
}
