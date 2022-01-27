import { Component, OnInit } from '@angular/core';
import { NomenclatureService } from '../../../utils/services/nomenclature.service';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteNomenclatureModalComponent } from '../../../utils/modals/delete-nomenclature-modal/delete-nomenclature-modal.component';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.scss']
})
export class LanguagesListComponent implements OnInit {
  isLoading:boolean;
  list: any[] = [];
  pagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  type='2';
  constructor(private nomenclatureService: NomenclatureService,
    private modalService:NgbModal,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.listNomenclatureTypes();
  }

  listNomenclatureTypes( data :any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      page: data.page,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    })
    this.nomenclatureService.nomenclatureTypeList(this.type, data).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.list = response.data.items;
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openDeleteNomenclatureModal(nomenclatureId: number): void {
    const modalRef = this.modalService.open(DeleteNomenclatureModalComponent, { centered: true });
    modalRef.result.then(() => this.deleteNomenclature({ nomenclatureType: +this.type, nomenclatureId: +nomenclatureId }), () => {});
  }

  deleteNomenclature(data): void {
    this.nomenclatureService.delete(data).subscribe(() => {
      this.listNomenclatureTypes();
      this.notificationService.success('Success', 'You\'ve successfully deleted nomenclature!', NotificationUtil.getDefaultConfig());
    });
  }

}
