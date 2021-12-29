import { Component, OnInit } from '@angular/core';
import { PaginationModel } from '../../../../../evaluation/src/app/utils/models/pagination.model';
import { LoggingService } from '../../utils/services/logging-service/logging.service';

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

  selectedProject: any;
  selectedEvent: any;
  loggingValues: any;

  projects: any;
  events: any;

  constructor(private loggingService: LoggingService) {}

  ngOnInit(): void {
    this.retriveDeopdowns();
    this.getLoggingValues();
  }

  retriveDeopdowns() {
    this.loggingService.getProjectSelectItem().subscribe((res) => (this.projects = res.data));
    this.loggingService.getEventSelectItem().subscribe((res) => (this.events = res.data));
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
      project: this.selectedProject || '',
      event: this.selectedEvent || '',
      page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    };

    this.loggingService.getLoggingValues(params).subscribe((res) => {
      (this.loggingValues = res.data.items), console.log('res', res.data.items);
    });

  }

}
