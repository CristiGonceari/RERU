import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentModel } from '../../../utils/models/department.model';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';
import { ImportDepartmentModalComponent } from '../../../utils/modals/import-department-modal/import-department-modal.component';

@Component({
	selector: 'app-departments-table',
	templateUrl: './departments-table.component.html',
	styleUrls: ['./departments-table.component.scss']
})
export class DepartmentsTableComponent implements OnInit {
	departments: DepartmentModel[] = [];
	keyword: string;
	pagedSummary: PaginationSummary = new PaginationSummary();
	isLoading: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

	constructor(private departmentService: DepartmentService,
		private route: ActivatedRoute,
		public translate: I18nService,
		private router: Router,
		private notificationService: NotificationsService,
		private modalService: NgbModal) { }

	ngOnInit(): void {
		this.list();
	}

	list(data: any = {}) {
		this.keyword = data.keyword;
		let params = {
			name: this.keyword || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}

		this.departmentService.getList(params).subscribe(res => {
			if (res && res.data) {
				this.departments = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name'];
		for (let i = 0; i < headersHtml.length - 1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.keyword || ''
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
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
		this.departmentService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	navigate(id) {
		this.router.navigate(['department-details/', id, 'overview'], { relativeTo: this.route });
	}

	delete(id): void {
		this.departmentService.delete(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('departments.succes-remove-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		forkJoin([
			this.translate.get('departments.remove'),
			this.translate.get('departments.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
		});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.delete(id), () => { });
	}

	openImportModal(): void {
		const modalRef: any = this.modalService.open(ImportDepartmentModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
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
			this.notificationService.success('Success', 'Users Imported!', NotificationUtil.getDefaultMidConfig());
			this.list();
		}, () => { }, () => {
			this.isLoading = false;
		})
	}
}
