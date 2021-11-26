import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTypeStatusEnum } from '../../../utils/enums/test-type-status.enum';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { Location } from '@angular/common';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-test-type-details',
  templateUrl: './test-type-details.component.html',
  styleUrls: ['./test-type-details.component.scss']
})
export class TestTypeDetailsComponent implements OnInit {
  testId: number;
  testName;
  status;
  mode;
  statusEnum = TestTypeStatusEnum;
  isLoading: boolean = false;

  constructor(
    private service: TestTypeService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private router: Router,
    private modalService: NgbModal,
		private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
      this.get();
		});
  }

  get(){
    this.service.getTestType(this.testId).subscribe(
      (res) => {
        if(res && res.data) {
          this.isLoading = false;
          this.testName = res.data.name;
          this.mode = res.data.mode;
          this.status = res.data.status;
        }
    })
  }

  cloneTestType(id): void {
		this.service.clone(id).subscribe(res => {
			if (res && res.data) this.router.navigate(['../test-type']);
		});
	}

  backClicked() {
		this.location.back();
	}

  changeStatus() {
		let params;

		if (this.status == TestTypeStatusEnum.Draft)
			params = { testTypeId: +this.testId, status: TestTypeStatusEnum.Active }
		else if (this.status == TestTypeStatusEnum.Active)
			params = { testTypeId: +this.testId, status: TestTypeStatusEnum.Canceled }

		this.service.changeStatus({ data: params }).subscribe(() => { this.get(); this.router.navigate(['test-type/type-details', this.testId, 'overview'])});
	}

	validateTestType() {
    const params = {
      testTypeId: +this.testId
    }
		this.service.validateTestType(params).subscribe(() => {
      this.changeStatus();
    });
    
	}

  deleteTestType(testId): void {
		this.service.deleteTestType(testId).subscribe(() => {
			this.router.navigate(['/test-type']);
			this.notificationService.success('Success', 'Test type was successfully deleted', NotificationUtil.getDefaultMidConfig());
    });
	}

	openConfirmationDeleteModal(testId): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this test type?';
		modalRef.result.then(() => this.deleteTestType(testId), () => { });
	}

  
}
