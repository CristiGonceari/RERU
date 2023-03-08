import { Component, OnInit } from '@angular/core';
import { SearchPipe } from '../../../utils/pipes/search.pipe';
import { SafeHtmlPipe } from '../../../utils/pipes/safe-html.pipe';
import { AdminModuleModel } from '../../../utils/models/admin-module.model';
import { ModulesService } from '../../../utils/services/modules.service';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ConfirmModalComponent, PermissionCheckerService, PrintModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-list-module',
	templateUrl: './list-module.component.html',
	styleUrls: ['./list-module.component.scss'],
	providers: [SearchPipe, SafeHtmlPipe],
})
export class ListModuleComponent implements OnInit {
	isLoading = true;
	modules: AdminModuleModel[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	viewDetails: boolean = false;
	title: string;
	description: string;
	no: string;
	yes: string;
	documentTitle: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	constructor(
		private moduleService: ModulesService,
		public translate: I18nService,
		private router: Router, 
		private route: ActivatedRoute,
		public permissionService: PermissionCheckerService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
	) {}

	ngOnInit(): void {
		this.getModules();
		this.checkPermission();
	}

	getTitle(): string {
		this.documentTitle = document.getElementById('documentTitle').innerHTML;
		return this.documentTitle
	}

	getModules(data: any = {}): void {
		let params: any = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(params);
	}
	
	list(params){
		this.moduleService.moduleList(params).subscribe(res => {
			if(res && res.data) {
				this.modules = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['icon', 'name', 'code', 'type', 'priority', 'status'];
		
		for (let i=1; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
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
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.moduleService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	navigateToDetails(id): void {
		this.router.navigate(['../', id, 'overview'], {relativeTo: this.route});
	}

	navigate(url: string): void {
		window.open(url, '_blank');
	}

	checkPermission(): void {
		if (this.permissionService.isGranted('P00000004')) 
      		this.viewDetails = true;
	}

	openRemoveModal(id: number, name): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('pages.modules.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = `${this.description} (${name})?`;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.removeModule(id, name), () => {});
	}

	removeModule(id: number, name): void {
		this.moduleService.delete(id).subscribe(res => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('modules.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title,`${name} ${this.description}`, NotificationUtil.getDefaultMidConfig(),
		 			this.getModules()
				);
			},
			err => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

}
