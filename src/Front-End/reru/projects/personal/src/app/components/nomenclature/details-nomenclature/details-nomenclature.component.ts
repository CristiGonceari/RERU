import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { DisableNomenclatureModalComponent } from '../../../utils/modals/disable-nomenclature-modal/disable-nomenclature-modal.component';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';
import { I18nService } from '../../../utils/services/i18n.service';
import { NomenclatureTypeService } from '../../../utils/services/nomenclature-type.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-details-nomenclature',
  templateUrl: './details-nomenclature.component.html',
  styleUrls: ['./details-nomenclature.component.scss']
})
export class DetailsNomenclatureComponent implements OnInit {
  isLoading: boolean = true;
  nomenclature: NomenclatureTypeModel;
  notification: any = {
    success: 'Success',
    error: 'Error',
    deleted: 'Nomenclature disabled!',
    internal: 'Internal server error!'
  }
  isRecordView: boolean;
  constructor(private nomenclatureTypeService: NomenclatureTypeService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private translate: I18nService) { }

  ngOnInit(): void {
    this.translateData();
    this.translate.change.subscribe(() => this.translateData());
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id && !isNaN(response.id)) {
        this.retrieveNomenclature(response.id);
      } else {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      }
    });
  }

  retrieveNomenclature(id: number): void {
    this.nomenclatureTypeService.get(id).subscribe(response => {
      this.nomenclature = response.data;
      this.isLoading = false;
    });
  }

  openDisableModal(): void {
    const modalRef: any = this.modalService.open(DisableNomenclatureModalComponent, { centered: true });
    modalRef.componentInstance.name = this.nomenclature.name;
    modalRef.result.then(() => this.disable(), () => { });
  }

  disable(): void {
    this.isLoading = true;
    this.nomenclatureTypeService.disable(this.nomenclature.id).subscribe(() => {
      this.notificationService.success(this.notification.success, this.notification.disabled, NotificationUtil.getDefaultMidConfig());
      this.retrieveNomenclature(this.nomenclature.id);
    });
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('notification.nomenclature-disable'),
      this.translate.get('notification.internal')
    ]).subscribe(([success, error, disabled, internal]) => {
      this.notification = { success, error, disabled, internal };
    });
  }
}
