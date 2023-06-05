import { Component, OnInit } from '@angular/core';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { RequiredDocumentService } from '../../../utils/services/required-document/required-document.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-required-documents-table',
	templateUrl: './required-documents-table.component.html',
	styleUrls: ['./required-documents-table.component.scss']
})
export class RequiredDocumentsTableComponent implements OnInit {
	isLoading: boolean = true;

	name: string;
	mandatory: boolean = null;

	tableData: [] = [];
	pagedSummary: PaginationModel = new PaginationModel();

	title: string;
	description: string;
	no: string;
	yes: string;

	filters: any = {}

	printTranslates: any[];
	headersToPrint = [];
	downloadFile: boolean;

	constructor(
		private requiredDocumentService: RequiredDocumentService,
		public translate: I18nService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
	) { }

	ngOnInit(): void {
		this.list();
	}

	getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'mandatory'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			name: this.filters.name,
			orientation: 2,
			mandatory: this.checkIfUndefined(this.filters.mandatory) 
		};

		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}
	
	checkIfUndefined(item){
		if (typeof item == 'undefined') {
			return null
		} else {
			return item
		}
	}

	printTable(data): void {
		this.downloadFile = true;
		this.requiredDocumentService.print(data).subscribe(response => {
			if (response) {
				let fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
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

	setFilter(field: string, value, startSearch: boolean = false): void {
		this.filters[field] = value;
		if (startSearch) {
			this.getDocuments()
		}
	}

	getDocuments(): void {
		this.pagedSummary.currentPage = 1;
		this.list();
	}

	resetFilters(): void {
		this.filters = {};
		this.pagedSummary.currentPage = 1;
		this.list();
	}

	list(data: any = {}): void {
		let params = {
			name: this.name || '',
			mandatory: this.mandatory || null,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
			...this.filters
		}
		this.requiredDocumentService.getRequiredDocuments(params).subscribe((res) => {
			if (res && res.data.items) {
				this.tableData = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	deleteRequiredFile(id): void {
		this.requiredDocumentService.delete(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('require-documents.success-delete-mgs'),
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
			this.translate.get('modal.delete'),
			this.translate.get('require-documents.want-to-del-msg'),
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
		modalRef.result.then(() => this.deleteRequiredFile(id), () => { });
	}

}
