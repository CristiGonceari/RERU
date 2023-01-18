import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteDepartmentModalComponent } from '../../../utils/modals/delete-department-modal/delete-department-modal.component';
import { DepartmentModel } from '../../../utils/models/department.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { ImportDepartmentsModalComponent } from '../../../utils/modals/import-departments-modal/import-departments-modal.component';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { I18nService, PrintModalComponent } from '@erp/shared';

@Component({
  selector: 'app-department-table',
  templateUrl: './department-table.component.html',
  styleUrls: ['./department-table.component.scss']
})
export class DepartmentTableComponent implements OnInit {
  isLoading: boolean = true;
  departments: DepartmentModel[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  
  title: string;
  description: string
  headersToPrint: any[] = [];
  filters: any = {};
  printTranslates: string[];
  downloadFile: boolean = false;

  constructor(private departmentService: DepartmentService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    public translate: I18nService,
    ) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      page: data.page,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    });
    this.departmentService.list(request).subscribe(response => {
      if (response.success) {
        this.departments = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openImportDepartmentsModal(){
    const modalRef: any = this.modalService.open(ImportDepartmentsModalComponent, { centered: true });
    modalRef.result.then((data) => this.import(data), () => { });
  }

  import(data): void {
		this.isLoading = true;
		const form = new FormData();
		form.append('File', data.file);
		this.departmentService.bulkImport(form).subscribe(response => {
			if(response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			}
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('departments.bulk-import-departments-succes'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		}, () => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('departments.bulk-import-departments-error'),
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
		let headersDto = ['colaboratorId', 'name'];
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
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
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
		this.departmentService.print(data).subscribe(response => {
			if (response) {
				fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

  // openConfirmationDeleteModal(department: DepartmentModel): void {
  //   const modalRef: any = this.modalService.open(DeleteDepartmentModalComponent, { centered: true });
  //   modalRef.componentInstance.name = department.name;
  //   modalRef.result.then(() => this.delete(department.id), () => { });
  // }

  // delete(id: number): void {
  //   this.isLoading = true;
  //   this.departmentService.delete(id).subscribe(response => {
  //     this.list();
  //     this.notificationService.success('Success', 'Department deleted!', NotificationUtil.getDefaultMidConfig());
  //   });
  // }

}
