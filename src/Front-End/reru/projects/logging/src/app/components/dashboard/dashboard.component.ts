import { Component, OnInit } from '@angular/core';
import { PaginationModel } from '../../utils/models/pagination.model';
import { LoggingService } from '../../utils/services/logging-service/logging.service';
import { FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DetailsModalComponent } from '../../utils/modals/details-modal/details-modal.component';
import { DeleteLogsModalComponent } from '../../utils/modals/delete-logs-modal/delete-logs-modal.component';

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

  constructor(private loggingService: LoggingService,
		public modalService: NgbModal) {}

  ngOnInit(): void {
    this.retriveDeopdowns();
    this.getLoggingValues();
  }

  initFilters(){
    this.dateTimeFrom = '';
    this.dateTimeTo = '';
    
    this.searchFrom = '';
    this.searchTo = '';

    this.selectedProject = '';
    this.selectedEvent = '';
    this.userName = '';
    this.userIdentifier = '';

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

  viewJSON(id, items): void {
    const modalRef = this.modalService.open(DetailsModalComponent, { centered: true,  size : <any>'xl' });
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.items = items;
		modalRef.result.then(
			() => { },
      () => {modalRef.close()}
		);
  }

  openDeleteModal(): void{
    const modalRef = this.modalService.open(DeleteLogsModalComponent, { centered: true,  size : 'md' });
    modalRef.result.then(
			() => {},
      () => {modalRef.close()}
		);
  }

}
