import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteVacationModalComponent } from '../../../utils/modals/delete-vacation-modal/delete-vacation-modal.component';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { VacationModel } from '../../../utils/models/vacation.model';
import { VacationService } from '../../../utils/services/vacation.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-vacation-table',
  templateUrl: './vacation-table.component.html',
  styleUrls: ['./vacation-table.component.scss']
})
export class VacationTableComponent implements OnInit {
  isLoading: boolean = true;
  vacations: VacationModel[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private vacationService: VacationService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any= {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      page: data.page,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    })
    this.vacationService.list(request).subscribe(response => {
      if (response.success) {
        this.vacations = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openVacationDeleteModal(id: number): void {
    const modalRef = this.modalService.open(DeleteVacationModalComponent);
    modalRef.result.then(() => this.deleteVacation(id), () => {});
  }

  deleteVacation(id: number): void {
    this.vacationService.delete(id).subscribe(() => {
      this.list();
      this.notificationService.success('Success', 'Vacation deleted!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error during deletion!', NotificationUtil.getDefaultConfig());
    });
  }
}
