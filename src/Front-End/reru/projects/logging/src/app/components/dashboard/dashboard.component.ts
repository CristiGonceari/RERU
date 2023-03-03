import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PaginationModel } from '../../utils/models/pagination.model';
import { LoggingService } from '../../utils/services/logging-service/logging.service';
import { FormGroup } from '@angular/forms';
import { DetailsModalComponent } from '../../utils/modals/details-modal/details-modal.component';
import { DeleteLogsModalComponent } from '../../utils/modals/delete-logs-modal/delete-logs-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent, I18nService } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements AfterViewInit {
  pagination: PaginationModel = new PaginationModel();
  isLoading: boolean = true;

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

  title: string;
  description: string;
  no: string;
  yes: string;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];

  constructor(private loggingService: LoggingService,
    public modalService: NgbModal,
    public translate: I18nService) { }


  ngAfterViewInit(): void {
    this.retriveDropdowns();
    this.getLoggingValues();
    this.getTranslates()
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  getTranslates() {
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

    this.selectedProject.value = '';
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
      userName: this.searchUserName.value || '',
      userIdentifier: this.searchUserIdentifier.value || '',
      page: data.page || 1,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
    };

    this.loggingService.getLoggingValues(params).subscribe((res) => {
      if (res && res.data) {
        this.loggingValues = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
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
    const modalRef = this.modalService.open(DetailsModalComponent, { centered: true, size: <any>'xl' });
    modalRef.componentInstance.id = id;
    modalRef.componentInstance.items = items;
    modalRef.result.then(
      () => { },
      () => { modalRef.close() }
    );
  }

  openDeleteModal(): void {
    const modalRef = this.modalService.open(DeleteLogsModalComponent, { centered: true, size: 'md' });
    modalRef.result.then(
      () => { },
      () => { modalRef.close() }
    );
  }

  getHeaders(name: string): void {
    this.translateData();
    let headersHtml = document.getElementsByTagName('th');
    let headersDto = ['project', 'userName', 'userIdentifier', 'event', 'eventMessage', 'jsonMessage'.toString(), 'date'];
    for (let i = 0; i < headersHtml.length; i++) {
      this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
    }
    let printData = {
      tableName: name,
      fields: this.headersToPrint,
      orientation: 2,
      fromDate: this.searchFrom || '',
      toDate: this.searchTo || '',
      projectName: this.selectedProject || '',
      event: this.searchEventName.value || '',
      eventMessage: this.searchEventMessage.value || '',
      jsonMessage: this.searchJsonMessage.value.replace(/\s+/g, '') || '',
      userName: this.searchUserName.value || '',
      userIdentifier: this.searchUserIdentifier.value || ''
    };
    const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.tableData = printData;
    modalRef.componentInstance.translateData = this.printTranslates;
    modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
    this.headersToPrint = [];
  }

  translateData(): void {
    this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows']
    forkJoin([
      this.translate.get('print.print-table'),
      this.translate.get('print.print-msg'),
      this.translate.get('print.sorted-by'),
      this.translate.get('button.cancel'),
			this.translate.get('print.select-file-format'),
			this.translate.get('print.max-print-rows')
    ]).subscribe(
      (items) => {
        for (let i = 0; i < this.printTranslates.length; i++) {
          this.printTranslates[i] = items[i];
        }
      }
    );
  }

  printTable(data): void {
    this.downloadFile = true;
    this.loggingService.print(data).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], data.tableName.trim(), { type: response.body.type });
        saveAs(file);
        this.downloadFile = false;
      }
    }, () => this.downloadFile = false);
  }

}
