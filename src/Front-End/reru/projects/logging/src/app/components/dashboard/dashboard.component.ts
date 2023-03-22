import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
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

  dateTimeFrom: string;
  dateTimeTo: string;
  searchFrom: string;
  searchTo: string;
  loggingValues: [] = [];

  event: string = '';
  form: FormGroup;

  title: string;
  description: string;
  no: string;
  yes: string;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];

  filters: any = {};
  showFilters: boolean = true;

  constructor(
    private loggingService: LoggingService,
    public modalService: NgbModal,
    public translate: I18nService,
    private cd: ChangeDetectorRef) { }

  ngAfterViewInit(): void {
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

  resetFilters() {
    this.filters = {};
		this.pagination.currentPage = 1;

    this.dateTimeFrom = '';
    this.dateTimeTo = '';

    this.searchFrom = '';
    this.searchTo = '';

    this.showFilters = false;
		this.cd.detectChanges();
		this.showFilters = true;

    this.getLoggingValues();
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
    this.isLoading = true;

    let params = {
      fromDate: this.searchFrom || '',
      toDate: this.searchTo || '',
      projectName: this.filters.projectName || '',
      event: this.filters.eventName || '',
      eventMessage: this.filters.eventMessage || '',
      jsonMessage: this.filters.jsonMessage && this.filters.jsonMessage.replace(/\s+/g, '') || '',
      userName: this.filters.userName || '',
      userIdentifier: this.filters.userIdentifier || '',
      page: data.page || 1,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      ...this.filters
    };

    this.loggingService.getLoggingValues(params).subscribe((res) => {
      if (res && res.data) {
        this.loggingValues = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  setFilter(field: string, value): void {
		this.filters[field] = value;
		this.pagination.currentPage = 1;
		this.getLoggingValues();
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
      projectName: this.filters.projectName || '',
      event: this.filters.eventName || '',
      eventMessage: this.filters.eventMessage || '',
      jsonMessage: this.filters.jsonMessage && this.filters.jsonMessage.replace(/\s+/g, '') || '',
      userName: this.filters.userName || '',
      userIdentifier: this.filters.userIdentifier || ''
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
