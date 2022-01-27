import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { DisableNomenclatureModalComponent } from '../../../utils/modals/disable-nomenclature-modal/disable-nomenclature-modal.component';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { I18nService } from '../../../utils/services/i18n.service';
import { NomenclatureTypeService } from '../../../utils/services/nomenclature-type.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-nomenclature-table',
  templateUrl: './nomenclature-table.component.html',
  styleUrls: ['./nomenclature-table.component.scss']
})
export class NomenclatureTableComponent implements OnInit {
  isLoading: boolean = true;
  list: NomenclatureTypeModel[] = [];
  pagedSummary: PagedSummary = new PagedSummary();
  notification: any = {
    success: 'Success',
    error: 'Error',
    disabled: 'Nomenclature disabled!',
    internal: 'Internal server error!'
  }
  constructor(private noemnclatureTypeService: NomenclatureTypeService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private translate: I18nService) { }

  ngOnInit(): void {
    this.translateData();
    this.translate.change.subscribe(() => this.translateData());
    this.listNomenclatureType();
  }

  listNomenclatureType(data: PagedSummary | any = {}): void {
    const request = {
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }
    this.noemnclatureTypeService.list(request).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.list = response.data.items;
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openDisableModal(nomenclature): void {
    const modalRef: any = this.modalService.open(DisableNomenclatureModalComponent, { centered: true });
    modalRef.componentInstance.name = nomenclature.name;
    modalRef.result.then(() => this.disable(nomenclature.id), () => { });
  }

  disable(id: number): void {
    this.isLoading = true;
    this.noemnclatureTypeService.disable(id).subscribe(response => {
      this.notificationService.success(this.notification.success, this.notification.disabled, NotificationUtil.getDefaultMidConfig());
      this.listNomenclatureType();
    }, error => {
      this.notificationService.error(this.notification.error, this.notification.internal, NotificationUtil.getDefaultMidConfig());
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
