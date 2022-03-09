import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTemplateStatusEnum } from '../../../utils/enums/test-template-status.enum';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { Location } from '@angular/common';
import { ConfirmModalComponent } from '@erp/shared';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'app-test-template-details',
  templateUrl: './test-template-details.component.html',
  styleUrls: ['./test-template-details.component.scss']
})
export class TestTemplateDetailsComponent implements OnInit {
  testId: number;
  testName;
  status;
  mode;
  statusEnum = TestTemplateStatusEnum;
  isLoading: boolean = false;

  title: string;
  description: string;
  no: string;
  yes: string;

  constructor(
    private service: TestTemplateService,
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
    this.service.getTestTemplate(this.testId).subscribe(
      (res) => {
        if(res && res.data) {
          this.isLoading = false;
          this.testName = res.data.name;
          this.mode = res.data.mode;
          this.status = res.data.status;
        }
    })
  }

  cloneTestTemplate(id): void {
		this.service.clone(id).subscribe(res => {
			if (res && res.data) this.router.navigate(['../test-type']);
		});
	}

  backClicked() {
		this.location.back();
	}

  changeStatus() {
		let params;

		if (this.status == TestTemplateStatusEnum.Draft)
			params = { testTemplateId: +this.testId, status: TestTemplateStatusEnum.Active }
		else if (this.status == TestTemplateStatusEnum.Active)
			params = { testTemplateId: +this.testId, status: TestTemplateStatusEnum.Canceled }

		this.service.changeStatus({ data: params }).subscribe(() => { 
			
			this.get(); this.router.navigate(['test-type/type-details', this.testId, 'overview'])
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

	validateTestTemplate() {
    const params = {
		testTemplateId: +this.testId
    }
		this.service.validateTestTemplate(params).subscribe(() => {
      this.changeStatus();
    });
    
	}

  deleteTestTemplate(testId): void {
		this.service.deleteTestTemplate(testId).subscribe(() => {
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
		modalRef.result.then(() => this.deleteTestTemplate(testId), () => { });
	}

  
}
