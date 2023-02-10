import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserRoleModel } from '../../../utils/models/user-role.model';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { UserRoleService } from '../../../utils/services/user-role.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';
import { ImportRoleModalComponent } from '../../../utils/modals/import-role-modal/import-role-modal.component';

@Component({
  selector: 'app-user-roles-table',
  templateUrl: './user-roles-table.component.html',
  styleUrls: ['./user-roles-table.component.scss']
})
export class UserRolesTableComponent implements OnInit {
  roles: UserRoleModel[] = [];
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

	constructor(private roleService: UserRoleService,
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

		this.roleService.getList(params).subscribe(res => {
			if(res && res.data) {
				this.roles = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
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
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      		this.translate.get('print.select-file-format')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.roleService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	navigate(id) {
		this.router.navigate(['user-role-details/', id, 'overview'], { relativeTo: this.route });
	}

	delete(id): void {
		this.roleService.delete(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user-roles.succes-remove-msg'),
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
			this.translate.get('user-roles.remove'),
			this.translate.get('user-roles.remove-msg'),
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
		const modalRef: any = this.modalService.open(ImportRoleModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
		modalRef.result.then((data) => this.import(data), () => { });
	}

	import(data): void {
		this.isLoading = true;
		const form = new FormData();
		form.append('File', data.file);
		this.roleService.bulkImport(form).subscribe(response => {
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
