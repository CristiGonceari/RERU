import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTemplateModeEnum } from 'projects/evaluation/src/app/utils/enums/test-template-mode.enum';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestTemplate } from '../../../../utils/models/test-templates/test-template.model';
import { TestTemplateService } from '../../../../utils/services/test-template/test-template.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { GenerateDocumentModalComponent } from 'projects/evaluation/src/app/utils/modals/generate-document-modal/generate-document-modal.component';
import { FileTypeEnum } from '../../../../utils/enums/file-type.enum';
@Component({
	selector: 'app-test-template-list-table',
	templateUrl: './test-template-list-table.component.html',
	styleUrls: ['./test-template-list-table.component.scss']
})
export class TestTemplateListTableComponent implements OnInit {
	testTemplateList: TestTemplate[] = [];
	testTemplateStatusEnumList: SelectItem[] = [];
	pagination: PaginationModel = new PaginationModel();
	eventName: string;
	testName: string;
	modeEnum = TestTemplateModeEnum;

	keyword: string;
	status: string = '';
	public id: number;

	statusEnum = TestTemplateStatusEnum;
	fileTypeEnum = FileTypeEnum;
	isActive: boolean = false;
	isLoading: boolean = false;

	title: string;
	description: string;
	no: string;
	yes: string;

	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	filters: any = {}

	constructor(
		public referenceService: ReferenceService,
		public translate: I18nService,
		public router: Router,
		private testTemplateService: TestTemplateService,
		private route: ActivatedRoute,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		private printTemplateService: PrintTemplateService
	) { }

	ngOnInit(): void {
		this.list();
		this.getStatusForDropdown();
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'categoriesCount', 'questionCount', 'duration', 'minPercent', 'mode', 'status', "canBeSolicited"];
		for (let i = 0; i < headersHtml.length - 1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.filters.name || this.testName || '',
			eventName: this.filters.eventName || this.eventName || '',
			status: +this.filters.status || ''
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
		this.testTemplateService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	list(data: any = {}) {
		this.isLoading = true;
		let params: any = {
			name: this.testName || '',
			eventName: this.eventName || '',
			status: this.status || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
			...this.filters
		}

		this.testTemplateService.getTestTemplates(params).subscribe(res => {
			if (res && res.data) {
				this.isLoading = false;
				this.testTemplateList = res.data.items;
				this.pagination = res.data.pagedSummary;
			}
		});
	}

	resetFilters(): void {
		this.filters = {};
		this.status = '';
		this.pagination.currentPage = 1;
		this.list();
	}

	setFilter(field: string, value): void {
		this.filters[field] = value;
		this.status = this.filters.status;
		this.pagination.currentPage = 1;
		this.list();
	}

	changeStatus(id, status) {
		let params;

		if (status == TestTemplateStatusEnum.Draft)
			params = { testTemplateId: id, status: TestTemplateStatusEnum.Active }
		else if (status == TestTemplateStatusEnum.Active)
			params = { testTemplateId: id, status: TestTemplateStatusEnum.Canceled }

		this.testTemplateService.changeStatus({ data: params }).subscribe(() => {
			this.list();
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-update-test-status-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	validateTestTemplate(id, status) {
		this.testTemplateService.validateTestTemplate({ testTemplateId: id }).subscribe(() => this.changeStatus(id, status));
	}

	getStatusForDropdown() {
		this.referenceService.getTestTemplateStatuses().subscribe(res => this.testTemplateStatusEnumList = res.data);
	}

	navigate(id) {
		this.router.navigate(['type-details/', id, 'overview'], { relativeTo: this.route });
	}

	cloneTestTemplate(id): void {
		this.testTemplateService.clone(id).subscribe(res => {
			if (res && res.data) this.list();
		});
	}

	deleteTestTemplate(id): void {
		this.testTemplateService.deleteTestTemplate(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('test-template.succes-delete-msg'),
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
			this.translate.get('test-template.delete-msg'),
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
		modalRef.result.then(() => this.deleteTestTemplate(id), () => { });
	}

	openGenerateDocumentModal(id, testName) {
		const modalRef: any = this.modalService.open(GenerateDocumentModalComponent, { centered: true, size: 'xl', windowClass: 'my-class', scrollable: true });
		modalRef.componentInstance.id = id;
		modalRef.componentInstance.testName = testName;
		modalRef.componentInstance.fileType = 1;
		modalRef.result.then((response) => (response), () => { });
	}

	printTestTemplate(id) {
		this.printTemplateService.getTestTemplatePdf(id).subscribe((response: any) => {
			let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];

			if (response.body.type === 'application/pdf') {
				fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
			}

			const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
			saveAs(file);
		});
	}
}
