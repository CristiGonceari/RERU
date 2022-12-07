import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PaginationModel } from '../../utils/models/pagination.model';
import { LoggingService } from '../../utils/services/logging-service/logging.service';
import { FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DetailsModalComponent } from '../../utils/modals/details-modal/details-modal.component';
import { DeleteLogsModalComponent } from '../../utils/modals/delete-logs-modal/delete-logs-modal.component';
import { I18nService } from '@erp/shared';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements AfterViewInit {
  pagination: PaginationModel = new PaginationModel();
  isLoading: boolean = false;

  @ViewChild('eventName') searchEventName: any;
  @ViewChild('userName') searchUserName: any;
	@ViewChild('eventMessage') searchEventMessage: any;
	@ViewChild('userIdentifier') searchUserIdentifier: any;
	@ViewChild('jsonMessage') searchJsonMessage: any;

  dateTimeFrom: string;
  dateTimeTo: string;
  searchFrom: string;
  searchTo: string;
  selectedProject: any;
  selectedEvent: any;
  loggingValues: [] = [];

  projects: any = [];
  events: any = [];
  event: string = '';
  form: FormGroup;

  constructor(private loggingService: LoggingService,
              public modalService: NgbModal,
              public translate: I18nService) {}


  ngAfterViewInit(): void {
    this.retriveDropdowns();
    this.getLoggingValues();
    this.getTranslates()
  }

  getTranslates(){
     let word;
    forkJoin([
      this.translate.get('dashboard.event'),
    ]).subscribe(([title]) => {
      word = title;
    });
  }

  initFilters() {
    this.pagination.currentPage = 1;

    this.dateTimeFrom = '';
    this.dateTimeTo = '';

    this.searchFrom = '';
    this.searchTo = '';

    this.selectedProject = '';
    this.selectedEvent = '';
    this.searchUserName.value = '';
    this.searchEventName.value = '';
    this.searchEventMessage.value = '';
    this.searchJsonMessage.value = '';
    this.searchUserIdentifier.value = '';

    this.getLoggingValues();
    this.retriveDropdowns();
  }

  retriveDropdowns() {
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
      projectName: this.selectedProject || '',
      event: this.searchEventName.value || '',
      eventMessage: this.searchEventMessage.value || '',
      jsonMessage: this.searchJsonMessage.value.replace(/\s+/g, '') || '',
      userName:  this.searchUserName.value || '',
      userIdentifier: this.searchUserIdentifier.value || '',
      page: data.page || 1,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
    };

    this.loggingService.getLoggingValues(params).subscribe((res) => {
      if(res && res.data){
        this.loggingValues = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = true;
      }
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
