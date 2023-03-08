import { Component, OnInit } from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { ObjectUtil } from '../../../utils/util/object.util';
import { EmployeeFunctionService } from '../../../utils/services/employee-function.service';
import { EmployeeFunctionModel } from '../../../utils/models/employee-function.model';
import { PrintModalComponent } from '@erp/shared';
import { I18nService } from '../../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ImportEmployeeFunctionModalComponent } from '../../../utils/modals/import-employee-function-modal/import-employee-function-modal.component'
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-employee-function-table',
  templateUrl: './employee-function-table.component.html',
  styleUrls: ['./employee-function-table.component.scss']
})
export class EmployeeFunctionTableComponent implements OnInit {

  isLoading: boolean = true;
  searchWord: string;
  pagedSummary: PagedSummary = new PagedSummary();
  functions: EmployeeFunctionModel[] = [];

  headersToPrint: any[] = [];
  filters: any = {};
  printTranslates: any[];
  downloadFile: boolean = false;

  title: string;
  description: string;

  constructor(
    private employeeFunctionService: EmployeeFunctionService,
    public translate: I18nService,
    private modalService: NgbModal,
    private notificationService: NotificationsService
    ) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    if (data.hasOwnProperty('searchWord')) this.searchWord = data.searchWord;
    if (data.hasOwnProperty('searchBy') && data.searchBy !== 'null') {
      data[data.searchBy] = this.searchWord;
    }
    data = ObjectUtil.preParseObject({
      ...data,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize,
      searchWord: (!data.searchBy || data.searchBy === 'null') && (data.searchWord || this.searchWord),
      searchBy: null
    });
    this.employeeFunctionService.list(data).subscribe(response => {
      if (response.success) {
        this.functions = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openImportModal(): void {
    const modalRef: any = this.modalService.open(ImportEmployeeFunctionModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
    modalRef.result.then((data) => this.importRoles(data), () => {});
  }

  importRoles(data): void {
    this.isLoading = true;
    const form = new FormData();
    form.append('file', data.file);
    this.employeeFunctionService.bulkImport(form).subscribe(response => {
			if(response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			}
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('roles.bulk-import-roles-succes'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.list();
    }, (error) => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('roles.bulk-import-roles-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.isLoading = false;
    })
  }

  getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['colaboratorId', 'name', 'type'];
		for (let i = 0; i < headersHtml.length - 1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			...this.filters
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
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
		let fileName: string;
		this.employeeFunctionService.print(data).subscribe(response => {
			if (response) {
				fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}
