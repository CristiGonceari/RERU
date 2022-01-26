import { Component, OnInit } from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { VacationProfileModel } from '../../../utils/models/vacation-profile.model';
import { FileService } from '../../../utils/services/file.service';
import { VacationProfileService } from '../../../utils/services/vacation-profile.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { VacationStateEnum } from '../../../utils/models/vacation-state.enum';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-subordinate-vacations',
  templateUrl: './subordinate-vacations.component.html',
  styleUrls: ['./subordinate-vacations.component.scss']
})
export class SubordinateVacationsComponent implements OnInit {
  vacations: VacationProfileModel[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }
  isLoading: boolean = true;
  vacationStates = VacationStateEnum;
  constructor(private vacationProfileService: VacationProfileService,
              private notificationService: NotificationsService,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      pageSize: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.vacationProfileService.getSubordinateVacations(request).subscribe((response: any) => {
      this.vacations = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  processVacation(data): void {
    this.isLoading = true;
    this.vacationProfileService.update(data).subscribe(response => {
      this.notificationService.success('Success', "Updated!", NotificationUtil.getDefaultConfig());
      this.list();
      this.isLoading = false;
    });
  }

  download(id: number): void {
    this.fileService.download(id);
  }

}
