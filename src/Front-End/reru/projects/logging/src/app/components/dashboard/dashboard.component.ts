import { Component, OnInit } from '@angular/core';
import { PaginationModel } from '../../utils/models/pagination.model';
import { LoggingService } from '../../utils/services/logging-service/logging.service';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  pagination: PaginationModel = new PaginationModel();

  dateTimeFrom: string;
  dateTimeTo: string;
  searchFrom: string;
  searchTo: string;
  userName: string;
  userIdentifier: string;

  selectedProject: any;
  selectedEvent: any;
  loggingValues: [] = [];

  projects: any;
  events: any;

  form: FormGroup;

  constructor(private loggingService: LoggingService) {}

  ngOnInit(): void {
    this.retriveDeopdowns();
    this.getLoggingValues();
  }

  retriveDeopdowns() {
    this.loggingService
      .getProjectSelectItem()
      .subscribe((res) => (this.projects = res.data));
    this.loggingService
      .getEventSelectItem()
      .subscribe((res) => (this.events = res.data));
  }

  setTimeToSearch(): void {
    if (this.dateTimeFrom) {
      const date = new Date(this.dateTimeFrom);
      this.searchFrom = new Date(
        date.getTime() - new Date(this.dateTimeFrom).getTimezoneOffset() * 60000
      ).toISOString();
    }
    if (this.dateTimeTo) {
      const date = new Date(this.dateTimeTo);
      this.searchTo = new Date(
        date.getTime() - new Date(this.dateTimeTo).getTimezoneOffset() * 60000
      ).toISOString();
    }
  }

  getLoggingValues(data: any = {}) {
    this.setTimeToSearch();

    let params = {
      fromDate: this.searchFrom || '',
      toDate: this.searchTo || '',
      projectName: this.selectedProject || '',
      event: this.selectedEvent || '',
      userName:  this.userName || '',
      userIdentifier: this.userIdentifier || '',
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
    };

    this.loggingService.getLoggingValues(params).subscribe((res) => {
      (this.loggingValues = res.data.items),
        (this.pagination = res.data.pagedSummary);
    });
  }

  atachProject(item: any) {
    this.selectedProject = item.target.value;
  }

  atachEvent(item: any) {
    this.selectedEvent = item.target.value;
  }
}