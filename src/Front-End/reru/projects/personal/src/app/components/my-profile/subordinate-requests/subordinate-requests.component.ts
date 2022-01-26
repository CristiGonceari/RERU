import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { FileService } from '../../../utils/services/file.service';
import { RequestsProfileService } from '../../../utils/services/requests-profile.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-subordinate-requests',
  templateUrl: './subordinate-requests.component.html',
  styleUrls: ['./subordinate-requests.component.scss']
})
export class SubordinateRequestsComponent implements OnInit {
  requests: any[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }
  isLoading: boolean = true;
  constructor(private requestsProfileService: RequestsProfileService,
              private fileService: FileService,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      pageSize: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.requestsProfileService.getSubordinateRequests(request).subscribe((response: any) => {
      this.requests = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {}, () => {this.isLoading = false});
  }

  processRequest(data): void {
    this.isLoading = true;
    this.requestsProfileService.update(data).subscribe(response => {
      this.list();
      this.notificationService.success('Success', "Updated!", NotificationUtil.getDefaultConfig());
      this.isLoading = false;
    });
  }

  download(id: number): void {
    this.fileService.download(id);
  }

}
