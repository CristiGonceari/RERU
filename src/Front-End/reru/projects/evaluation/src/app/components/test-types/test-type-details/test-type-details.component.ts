import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTypeStatusEnum } from '../../../utils/enums/test-type-status.enum';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { Location } from '@angular/common';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

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

  title: string;
  description: string;
  no: string;
  yes: string;

  constructor(
    private service: TestTypeService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
	public translate: I18nService,
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
			params = { testTemplateId: +this.testId, status: TestTypeStatusEnum.Active }
		else if (this.status == TestTypeStatusEnum.Active)
			params = { testTemplateId: +this.testId, status: TestTypeStatusEnum.Canceled }

		this.service.changeStatus({ data: params }).subscribe(() => { 
			
			this.get(); this.router.navigate(['test-type/type-details', this.testId, 'overview'])
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-update-status-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
	});
	}

	validateTestType() {
    const params = {
		testTemplateId: +this.testId
    }
		this.service.validateTestType(params).subscribe(() => {
      this.changeStatus();
    });
    
	}

  deleteTestType(testId): void {
		this.service.deleteTestType(testId).subscribe(() => {
      		forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('test-template.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.router.navigate(['/test-type']);
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
	}

	openConfirmationDeleteModal(testId): void {
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
		modalRef.result.then(() => this.deleteTestType(testId), () => { });
	}

  
}
