import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { DragulaService } from 'ng2-dragula';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { QuestionUnitTypeEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-type.enum'
import { TestTemplateQuestionCategoryService } from 'projects/evaluation/src/app/utils/services/test-template-question-category/test-template-question-category.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { ConfirmModalComponent } from '@erp/shared';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { TestTemplateStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-template-status.enum';

@Component({
  selector: 'app-test-template-categories',
  templateUrl: './test-templates-categories.component.html',
  styleUrls: ['./test-templates-categories.component.scss']
})
export class TestTemplatesCategoriesComponent implements OnInit {
  categories = [];
  testTemplateId;
  status;
  type = QuestionUnitTypeEnum;
  isLoading: boolean = false;
  sequence = SequenceTypeEnum;
  order = [];
  title: string;
  description: string;
  no: string;
  yes: string;
  @Input() isActive: boolean;
	disable: boolean = false;

  constructor(private service: TestTemplateQuestionCategoryService,
    private route: ActivatedRoute,
    private dragulaService: DragulaService,
    private testTemplateService: TestTemplateService,
    private notificationService: NotificationsService,
	  public translate: I18nService,
    private router: Router,
    private modalService: NgbModal, ) { }

  ngOnInit(): void {
    this.dragulaService.createGroup('Categories', {});
    this.isLoading = true;
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.testTemplateId = params.id;
      if (this.testTemplateId) {
        this.get();
      }
    });
  }

  get() {
    this.testTemplateService.getTestTemplate(this.testTemplateId).subscribe(res => {
      this.sequence = res.data.categoriesSequence;
      this.status = res.data.status;
      this.getList();
      if (this.status == TestTemplateStatusEnum.Active || this.status == TestTemplateStatusEnum.Canceled) {
        this.disable = true;
      }
    })
  }

  getList(id?: number) {
    let testId = this.testTemplateId ? this.testTemplateId : id;
    this.service.getQuestionCategoryByTestTemplateId({ testTemplateId: testId }).subscribe(
      res => {
        if (res && res.data) {
          this.categories = res.data;
          this.isLoading = false;
        }
      }
    )
  }

  checkEvent(event): void {
    this.sequence = event.target.checked;
    let sequenceData = {
      testTemplateId: +this.testTemplateId,
      categoriesSequenceType: this.sequence ? 1 : 0
    }
    this.service.categorySequenceType(sequenceData).subscribe(res => {
    })
  }

  setSequence(categories) {
    let idList = categories.map(x => x.id);
    this.order = Object.keys(idList).map((id, index) => {
      return {
        index: +id + 1,
        id: Object.values(idList)[index]
      }
    });

    let params = {
      testTemplateId: +this.testTemplateId,
      itemsOrder: this.order,
      sequenceType: this.sequence ? 1 : 0
    }
    this.service.setSequence(params).subscribe(res => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('tests.succes-order-update'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      if (res && res.data) {
        this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
        this.getList(this.testTemplateId);
      }
    });
  }

  confirm(categories): void {
    this.setSequence(categories);
  }

  ngOnDestroy(): void {
    this.dragulaService.destroy('Categories');
  }

  openView(id){
      this.router.navigate(['../categories-view/',id], {relativeTo:this.route});
  }

  openConfirmationDeleteModal(id, name): void {
    forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('categories.delete-msg'),
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
		modalRef.result.then(() => this.deleteTestTemplateCategory(id), () => { });
	}

  deleteTestTemplateCategory(id){
    this.service.deleteTestTemplateQuestionCategory({id: id}).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('categories.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getList();
    });
  }
}
