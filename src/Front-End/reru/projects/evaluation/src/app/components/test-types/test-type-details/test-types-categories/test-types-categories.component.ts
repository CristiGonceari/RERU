import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { DragulaService } from 'ng2-dragula';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { QuestionUnitTypeEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-type.enum'
import { TestTypeQuestionCategoryService } from 'projects/evaluation/src/app/utils/services/test-type-question-category/test-type-question-category.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { ConfirmModalComponent } from '@erp/shared';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-test-types-categories',
  templateUrl: './test-types-categories.component.html',
  styleUrls: ['./test-types-categories.component.scss']
})
export class TestTypesCategoriesComponent implements OnInit {
  categories = [];
  testTypeId;
  status;
  type = QuestionUnitTypeEnum;
  isLoading: boolean = false;
  sequence = SequenceTypeEnum;
  order = [];
  title: string;
  description: string;
  no: string;
  yes: string;
  @Input() isActive: boolean ;

  constructor(private service: TestTypeQuestionCategoryService,
    private route: ActivatedRoute,
    private dragulaService: DragulaService,
    private testTypeService: TestTypeService,
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
      this.testTypeId = params.id;
      if (this.testTypeId) {
        this.getList();
        this.get();
      }
    });
  }

  get() {
    this.testTypeService.getTestType(this.testTypeId).subscribe(res => {
      this.sequence = res.data.categoriesSequence;
      this.status = res.data.status;
    })
  }

  getList(id?: number) {
    let testId = this.testTypeId ? this.testTypeId : id;
    this.service.getQuestionCategoryByTestTypeId({ testTemplateId: testId }).subscribe(
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
      testTemplateId: +this.testTypeId,
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
      testTemplateId: +this.testTypeId,
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
        this.getList(this.testTypeId);
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
    this.service.deleteTestTypeQuestionCategory({id: id}).subscribe(() => {
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
