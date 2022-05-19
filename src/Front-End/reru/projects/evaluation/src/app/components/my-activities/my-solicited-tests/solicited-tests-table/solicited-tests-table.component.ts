import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { ConfirmModalComponent, PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { SolicitedTestStatusEnum } from 'projects/evaluation/src/app/utils/enums/solicited-test-status.model';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
	selector: 'app-solicited-tests-table',
	templateUrl: './solicited-tests-table.component.html',
	styleUrls: ['./solicited-tests-table.component.scss']
})
export class SolicitedTestsTableComponent implements OnInit {
	solicitedTests: [] = [];
	pagedSummary: PaginationModel = new PaginationModel();
	userId: number;
	isLoading: boolean = true;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	title: string;
	description: string;
	no: string;
	yes: string;
	enum = SolicitedTestStatusEnum;
	statuses: [] = [];

	constructor(
		private solicitedTestService: SolicitedTestService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
		private modalService: NgbModal,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.getMySolicitedTests();
	}

	getMySolicitedTests(data: any = {}) {
		this.isLoading = true;
		const params: any = {
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}
		this.solicitedTestService.getMySolicitedTests(params).subscribe(
			res => {
				if (res && res.data) {
					this.solicitedTests = res.data.items;
					this.pagedSummary = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

	openDeleteModal(id) {
		forkJoin([
			this.translate.get('solicited-test.remove'),
			this.translate.get('solicited-test.remove-msg'),
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

	delete(id) {
		this.solicitedTestService.deleteMySolicitedTest(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('solicited-test.succes-remove-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.getMySolicitedTests();
		})
	}

	// getHeaders(name: string): void {
	// 	this.translateData();
	// 	let testTable = document.getElementById('testsTable')
	// 	let headersHtml = testTable.getElementsByTagName('th');
	// 	let headersDto = ['programmedTime', 'testStatus', 'testTemplateName', 'accumulatedPercentage', 'result'];
	// 	for (let i=0; i<headersHtml.length; i++) {
	// 		this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
	// 	}
	// 	let printData = {
	// 		tableName: name,
	// 		fields: this.headersToPrint,
	// 		orientation: 2,
	// 		userId: this.userId
	// 	};
	// 	const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
	// 	modalRef.componentInstance.tableData = printData;
	// 	modalRef.componentInstance.translateData = this.printTranslates;
	// 	modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
	// 	this.headersToPrint = [];
	// }

	// translateData(): void {
	// 	this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
	// 	forkJoin([
	// 		this.translate.get('print.print-table'),
	// 		this.translate.get('print.print-msg'),
	// 		this.translate.get('print.sorted-by'),
	// 		this.translate.get('button.cancel')
	// 	]).subscribe(
	// 		(items) => {
	// 			for (let i=0; i<this.printTranslates.length; i++) {
	// 				this.printTranslates[i] = items[i];
	// 			}
	// 		}
	// 	);
	// }

	// printTable(data): void {
	// 	this.downloadFile = true;
	// 	this.testService.printUserTests(data).subscribe(response => {
	// 		if (response) {
	// 			const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
	// 			const blob = new Blob([response.body], { type: response.body.type });
	// 			const file = new File([blob], fileName, { type: response.body.type });
	// 			saveAs(file);
	// 			this.downloadFile = false;
	// 		}
	// 	}, () => this.downloadFile = false);
	// }
}
