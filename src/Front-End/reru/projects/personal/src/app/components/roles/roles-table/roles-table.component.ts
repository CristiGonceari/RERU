import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '../../../utils/modals/confirm-modal/confirm-modal.component';
import { ImportRoleModalComponent } from '../../../utils/modals/import-role-modal/import-role-modal.component';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { RoleModel } from '../../../utils/models/role.model';
import { RoleService } from '../../../utils/services/role.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { saveAs } from 'file-saver';
import { I18nService } from '../../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';
import { PrintModalComponent } from '@erp/shared';


@Component({
  selector: 'app-roles-table',
  templateUrl: './roles-table.component.html',
  styleUrls: ['./roles-table.component.scss']
})
export class RolesTableComponent implements OnInit {
  isLoading: boolean = true;
  roles: RoleModel[] = [];
  pagedSummary: PagedSummary = new PagedSummary();
  searchWord: string;

  title: string;
  description: string;

  headersToPrint: any[] = [];
  filters: any = {};
  printTranslates: string[];
  downloadFile: boolean = false;

  constructor(private roleService: RoleService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    public translate: I18nService,
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
    this.roleService.list(data).subscribe(response => {
      if (response.success) {
        this.roles = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  // openConfirmationDeleteModal(id: number): void {
  //   const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
  //   modalRef.componentInstance.title = 'Delete';
  //   modalRef.componentInstance.description = 'Are you sure you want to delete it?';
  //   modalRef.result.then(() => this.delete(id), () => { });
  // }

  // delete(id: number): void {
  //   this.isLoading = true;
  //   this.roleService.delete(id).subscribe(response => {
  //     this.list();
  //     this.notificationService.success('Success', 'Role deleted!', NotificationUtil.getDefaultMidConfig());
  //   });
  // }

  openImportModal(): void {
    const modalRef: any = this.modalService.open(ImportRoleModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
    modalRef.result.then((data) => this.importRoles(data), () => {});
  }

  importRoles(data): void {
    this.isLoading = true;
    const form = new FormData();
    form.append('file', data.file);
    this.roleService.bulkImport(form).subscribe(response => {
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
		let headersDto = ['colaboratorId', 'name', 'code', 'shortCode'];
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
		this.roleService.print(data).subscribe(response => {
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
