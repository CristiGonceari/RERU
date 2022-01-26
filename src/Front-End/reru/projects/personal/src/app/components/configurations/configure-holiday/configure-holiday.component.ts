import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { differenceInDays } from 'date-fns';
import { HolidayModel } from '../../../utils/models/holiday.model';
import { HolidayService } from '../../../utils/services/holiday.service';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { DeleteHolidayModalComponent } from '../../../utils/modals/delete-holiday-modal/delete-holiday-modal.component';
import { EditHolidayModalComponent } from '../../../utils/modals/edit-holiday-modal/edit-holiday-modal.component';
@Component({
  selector: 'app-configure-holiday',
  templateUrl: './configure-holiday.component.html',
  styleUrls: ['./configure-holiday.component.scss']
})
export class ConfigureHolidayComponent implements OnInit {

  holidays: HolidayModel[];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  isLoading: boolean = true;
  
  constructor(private holidayService: HolidayService,
              private notificationService: NotificationsService,
              private modalService: NgbModal) { }

  ngOnInit(): void {
    this.retrieveHolidays();
  }

  retrieveHolidays(data :any ={}): void {
    this.isLoading=true;
    const request = ObjectUtil.preParseObject({
      page: data.page,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.holidayService.get(request).subscribe((response: ApiResponse<any>) => {
      this.holidays = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  addHoliday(data: HolidayModel): void {
    this.isLoading = true;
    this.holidayService.add(this.parseData(data)).subscribe(response => {
      this.notificationService.success('Success', 'Holiday added!', NotificationUtil.getDefaultConfig());
      this.retrieveHolidays({ page: this.pagedSummary.currentPage });
      this.isLoading = false;
    }, (error) => {
      this.isLoading = false;
      if (error.status === 400) {
        this.notificationService.error('Error', 'Error on adding a holiday!', NotificationUtil.getDefaultConfig());
        return;
      }

      this.notificationService.error('Error', 'Internal server error!', NotificationUtil.getDefaultConfig());
    });
  }

  parseData(data: HolidayModel): HolidayModel {
    const days = differenceInDays(new Date(data.to), new Date(data.from));
    return {
      ...data,
      to: days === 1 ? null : data.to
    }
  }

  openHolidayDeleteModal(holiday: HolidayModel): void {
    const modalRef = this.modalService.open(DeleteHolidayModalComponent, { centered: true });
    modalRef.componentInstance.name = holiday.name;
    modalRef.result.then(() => this.deleteHoliday(holiday.id), () => {});
  }

  deleteHoliday(id: number): void {
    this.isLoading = true;
    this.holidayService.delete(id).subscribe(response => {
      this.notificationService.success('Success', 'Holiday deleted!', NotificationUtil.getDefaultConfig());
      this.retrieveHolidays();
    }, (error) => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Internal server error!', NotificationUtil.getDefaultConfig());
    });
  }

  openHolidayEditModal(holiday: HolidayModel): void {
    const modalRef = this.modalService.open(EditHolidayModalComponent, { centered: true });
    modalRef.componentInstance.holiday = holiday;
    modalRef.result.then((response) => this.editHoliday(response), () => {});
  }

  editHoliday(holiday: HolidayModel): void {
    this.isLoading = true;
    this.holidayService.update(holiday).subscribe(response => {
      this.notificationService.success('Success', 'Holiday updated!', NotificationUtil.getDefaultConfig());
      this.retrieveHolidays({ page: this.pagedSummary.currentPage });
    }, (error) => {
      this.isLoading = false;
      this.notificationService.error('Error', 'Internal server error!', NotificationUtil.getDefaultConfig());
    });
  }

}
