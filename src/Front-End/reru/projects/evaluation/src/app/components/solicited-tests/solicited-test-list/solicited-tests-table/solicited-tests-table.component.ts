import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { SolicitedTestStatusEnum } from 'projects/evaluation/src/app/utils/enums/solicited-test-status.model';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { forkJoin } from 'rxjs';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-solicited-tests-table',
	templateUrl: './solicited-tests-table.component.html',
	styleUrls: ['./solicited-tests-table.component.scss']
})
export class SolicitedTestsTableComponent implements OnInit {
	solicitedTests: [] = [];
	enum = SolicitedTestStatusEnum;
	pagination: PaginationModel = new PaginationModel();
	isLoading = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;

	headersToPrint = [];
	printTranslates: any[];
	filters: any = {}

	constructor(private solicitedTestService: SolicitedTestService,
		public translate: I18nService,
		private modalService: NgbModal) { }

	ngOnInit(): void {
		this.list();
	}

	list(data: any = {}) {
		this.isLoading = true;
		let params: any = {
			eventName: this.filters.eventName || '',
			userName: this.filters.userName || '',
			testName: this.filters.testName || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
		}

		this.solicitedTestService.getSolicitedTests(params).subscribe(res => {
			if (res && res.data) {
				this.isLoading = false;
				this.solicitedTests = res.data.items;
				this.pagination = res.data.pagedSummary;
			}
		});
	}

	openConfirmationChangeModal(id): void {
		forkJoin([
			this.translate.get('solicited-test.change-status'),
			this.translate.get('solicited-test.refuse-msg'),
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
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.changeStatus(id), () => { });
	}

	changeStatus(id) {
		let params: any = {
			id: id,
			status: SolicitedTestStatusEnum.Refused
		}

		this.solicitedTestService.changeStatus(params).subscribe(res => this.list());
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['userProfileName', 'testTemplateName', 'eventName', 'solicitedTestStatus'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			status: +this.filters.status || '',
			eventName: this.filters.eventName,
			userName: this.filters.userName,
			testName: this.filters.testName
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
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.solicitedTestService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	setFilter(field: string, value): void {
		this.filters[field] = value;
		this.list();
	}

	resetFilters(): void {
		this.filters = {};
		this.list();
	}
}
