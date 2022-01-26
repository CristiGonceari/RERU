import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';
import { NomenclatureModel } from '../../../utils/models/nomenclature.model';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { NomenclatureTypeService } from '../../../utils/services/nomenclature-type.service';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit',
  templateUrl: './edit-nomenclature.component.html',
  styleUrls: ['./edit-nomenclature.component.scss']
})
export class EditNomenclatureComponent extends EnterSubmitListener implements OnInit {
  isLoading: boolean = true;
  list: any[];
  nomenclatureForm: FormGroup;
  notification = {
    success: 'Success',
    error: 'Error',
    update: 'Nomenclature updated!',
    internal: 'Interval server error!'
  }
  constructor(private fb: FormBuilder,
    private nomenclatureTypeService: NomenclatureTypeService,
    private router: Router,
    private route: ActivatedRoute,
    private ngZone: NgZone,
    private notificationService: NotificationsService,
    private translate: I18nService,
    public location: Location) {
      super();
      this.callback = this.submit;
    }

  ngOnInit(): void {
    this.translateData();
    this.translate.change.subscribe(() => this.translateData());
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrieveNomenclature(response.id);
      }
    });
  }

  retrieveNomenclature(id: number): void {
    this.nomenclatureTypeService.get(id).subscribe((response: ApiResponse<NomenclatureModel>) => {
      this.initForm(response.data);
      this.isLoading = false;
    });
  }

  initForm(nomenclature: NomenclatureTypeModel): void {
    this.nomenclatureForm = this.fb.group({
      id: this.fb.control(nomenclature.id, [Validators.required]),
      name: this.fb.control(nomenclature.name, [Validators.required])
    });
  }

  submit(): void {
    this.nomenclatureTypeService.edit(this.nomenclatureForm.value).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.notificationService.success(this.notification.success, this.notification.update, NotificationUtil.getDefaultMidConfig());
        this.ngZone.run(() => this.router.navigate(['../../', this.nomenclatureForm.get('id').value], { relativeTo: this.route }));
      }
    }, () => {
      this.notificationService.error(this.notification.error, null, NotificationUtil.getDefaultMidConfig());
    });
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('notification.nomenclature-edit'),
      this.translate.get('notification.internal')
    ]).subscribe(([success, error, update, internal]) => {
      this.notification = { success, error, update, internal };
    });
  }
}
