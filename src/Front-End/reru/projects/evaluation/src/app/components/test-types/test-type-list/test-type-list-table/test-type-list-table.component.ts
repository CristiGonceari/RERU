import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTypeModeEnum } from 'projects/evaluation/src/app/utils/enums/test-type-mode.enum';
import { TestTypeStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-type-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { TestType } from '../../../../utils/models/test-types/test-type.model';
import { TestTypeService } from '../../../../utils/services/test-type/test-type.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { saveAs } from 'file-saver';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
	selector: 'app-test-type-list-table',
	templateUrl: './test-type-list-table.component.html',
	styleUrls: ['./test-type-list-table.component.scss']
})
export class TestTypeListTableComponent implements OnInit {
	testTypeList: TestType[] = [];
	testTypeStatusEnumList: SelectItem[] = [];
	pagination: PaginationModel = new PaginationModel();
	eventName: string;
	testName: StringConstructor;
	modeEnum = TestTypeModeEnum;

	keyword: string;
	status: string = '';
	public id: number;

	statusEnum = TestTypeStatusEnum;
	isActive: boolean = false;
	isLoading: boolean = false;

	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		public referenceService: ReferenceService,
		public translate: I18nService,
		public router: Router,
		private testTypeService: TestTypeService,
		private route: ActivatedRoute,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		private printTemplateService: PrintTemplateService
	) { }

	ngOnInit(): void {
		this.list();
		this.getStatusForDropdown();
	}

	list(data: any = {}) {
		this.isLoading = true;
		let params: any = {
			name: this.testName || '',
			eventName: this.eventName || '',
			status: this.status,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		}

		this.testTypeService.getTestTypes(params).subscribe(res => {
			if (res && res.data) {
				this.isLoading = false;
				this.testTypeList = res.data.items;
				this.pagination = res.data.pagedSummary;
			}
		});
	}

	changeStatus(id, status) {
		let params;

		if (status == TestTypeStatusEnum.Draft)
			params = { testTypeId: id, status: TestTypeStatusEnum.Active }
		else if (status == TestTypeStatusEnum.Active)
			params = { testTypeId: id, status: TestTypeStatusEnum.Canceled }

		this.testTypeService.changeStatus({ data: params }).subscribe(() => 
		{
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-update-status-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.list()
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	validateTestType(id, status) {
		this.testTypeService.validateTestType({ testTypeId: id }).subscribe(() => this.changeStatus(id, status));
	}

	getStatusForDropdown() {
		this.referenceService.getTestTypeStatuses().subscribe(res => this.testTypeStatusEnumList = res.data);
	}

	navigate(id) {
		this.router.navigate(['type-details/', id, 'overview'], { relativeTo: this.route });
	}

	cloneTestType(id): void {
		this.testTypeService.clone(id).subscribe(res => {
			if (res && res.data) this.list();
		});
	}

	deleteTestType(id): void {
		this.testTypeService.deleteTestType(id).subscribe(() => {
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
		modalRef.result.then(() => this.deleteTestType(id), () => { });
	}

	printTestTemplate(id){
		this.printTemplateService.getTestTemplatePdf(id).subscribe((response : any) => {
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
