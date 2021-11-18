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

	constructor(
		public referenceService: ReferenceService,
		public router: Router,
		private testTypeService: TestTypeService,
		private route: ActivatedRoute,
		private modalService: NgbModal,
		private notificationService: NotificationsService
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

		this.testTypeService.changeStatus({ data: params }).subscribe(() => this.list());
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
			this.notificationService.success('Success', 'Test type was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this test type?';
		modalRef.result.then(() => this.deleteTestType(id), () => { });
	}
}
