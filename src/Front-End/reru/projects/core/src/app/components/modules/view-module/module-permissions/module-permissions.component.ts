import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { PermissionModel } from 'projects/core/src/app/utils/models/permission.model';
import { ModulePermissionsService } from 'projects/core/src/app/utils/services/module-permissions.service';
import { ObjectUtil } from 'projects/core/src/app/utils/util/object.util';
import { saveAs } from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintModalComponent } from '@erp/shared';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
	selector: 'app-module-permissions',
	templateUrl: './module-permissions.component.html',
	styleUrls: ['./module-permissions.component.scss'],
})
export class ModulePermissionsComponent implements OnInit {
	isLoading = true;
	moduleId: number;
	permissions: PermissionModel[];
	pagination: PaginationSummary = new PaginationSummary();
	code: string;
	description: string;

	headersToPrint = [];
	printTranslates: any[];
	downloadFile: boolean;

	filters: any = {};

	@ViewChild('code') searchCode: any;
	@ViewChild('description') searchDescription: any;

	constructor(
		private permissionServise: ModulePermissionsService,
		private activatedRoute: ActivatedRoute,
		private modalService: NgbModal,
		public translate: I18nService,

		) {}

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
				this.getPermissions();
			}
		});
	}
	
	getPermissions(data: any = {}): void {
		data = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
			...this.filters,
		};

		this.list(data);
	}

	list(data): void {
		this.permissionServise.get(this.moduleId, ObjectUtil.preParseObject(data)).subscribe(res => {
			if (res && res.data.items) {
				this.isLoading = false;
				this.permissions = res.data.items;
				this.pagination = res.data.pagedSummary;
			} else {
				this.isLoading = false;
			}
		});
		this.isLoading = true;
	}

	setFilter(field: string, value): void {
		this.filters[field] = value;
		this.pagination.currentPage = 1;
	}

	clearFields() {
		this.searchCode.clearSearch();
		this.searchDescription.clearSearch();
		this.filters = {};
		this.getPermissions();
	}

	getTitle(): string {
		return document.getElementById('title').innerHTML;
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['code', 'description'];
         
		for (let i = 0; i <= headersHtml.length - 1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			moduleId: this.moduleId,
			code: this.code,
			description: this.description
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
		this.permissionServise.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}
