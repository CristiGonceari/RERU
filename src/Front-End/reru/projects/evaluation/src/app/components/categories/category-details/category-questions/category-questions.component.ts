import { Component, Input, OnInit } from '@angular/core';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionCategoryService } from 'projects/evaluation/src/app/utils/services/question-category/question-category.service';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { QuestionUnitStatusEnum } from '../../../../utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { QuestionUnit } from '../../../../utils/models/question-units/question-unit.model';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BulkImportQuestionsComponent } from '../../../questions/bulk-import-questions/bulk-import-questions.component';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { QuestionByCategoryService } from 'projects/evaluation/src/app/utils/services/question-by-category/question-by-category.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';

@Component({
  selector: 'app-category-questions',
  templateUrl: './category-questions.component.html',
  styleUrls: ['./category-questions.component.scss'],
})
export class CategoryQuestionsComponent implements OnInit {

  @Input() categoryId;
  categoryList = [];
  questionList: QuestionUnit[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  questionEnum = QuestionUnitStatusEnum;
  type = QuestionUnitTypeEnum;
  isLoading: boolean = true;
  title: string;
  description: string;
  no: string;
  yes: string;
  downloadFile: boolean = false;
  headersToPrint = [];
  printTranslates: any[];

  constructor(
    private questionService: QuestionService,
    private categoryService: QuestionCategoryService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    public router: Router,
    private questionByCategory: QuestionByCategoryService,
    public translate: I18nService,
    private route: ActivatedRoute,
    private printTemplateService: PrintTemplateService
  ) {}

  ngOnInit(): void {
    this.categoryService.category.subscribe((x) => (this.categoryId = x));
    this.getAll();
  }

  getHeaders(name: string): void {
    this.translateData();
    let headersHtml = document.getElementsByTagName('th');
    let headersDto = ['question', 'answersCount', 'status', 'questionType'];
    for (let i = 0; i < headersHtml.length - 1; i++) {
      this.headersToPrint.push({
        value: headersDto[i],
        label: headersHtml[i].innerHTML,
      });
    }
    let printData = {
      tableName: name,
      fields: this.headersToPrint,
      orientation: 2,
      questionCategoryId: +this.categoryId,
    };
    const modalRef: any = this.modalService.open(PrintModalComponent, {
      centered: true,
      size: 'xl',
    });
    modalRef.componentInstance.tableData = printData;
    modalRef.componentInstance.translateData = this.printTranslates;
    modalRef.result.then(
      () => this.printTable(modalRef.result.__zone_symbol__value),
      () => {}
    );
    this.headersToPrint = [];
  }

  translateData(): void {
    this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel'];
    forkJoin([
      this.translate.get('print.print-table'),
      this.translate.get('print.print-msg'),
      this.translate.get('print.sorted-by'),
      this.translate.get('button.cancel'),
    ]).subscribe((items) => {
      for (let i = 0; i < this.printTranslates.length; i++) {
        this.printTranslates[i] = items[i];
      }
    });
  }

  printTable(data): void {
    this.downloadFile = true;
    this.questionService.print(data).subscribe(
      (response) => {
        if (response) {
          const fileName = response.headers
            .get('Content-Disposition')
            .split('filename=')[1]
            .split(';')[0]
            // .substring(2).slice(0, -2);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], fileName, { type: response.body.type });
          saveAs(file);
          this.downloadFile = false;
        }
      },
      () => (this.downloadFile = false)
    );
  }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  getAll(data: any = {}) {
    let params = {
      questionCategoryId: this.categoryId,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    };

    this.questionService.getAll(params).subscribe((res) => {
      if (res && res.data) {
        this.questionList = res.data.items;
        this.pagedSummary = res.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  changeStatus(id, status) {
    let params;

    if (status == QuestionUnitStatusEnum.Draft)
      params = {
        data: { questionId: id, status: QuestionUnitStatusEnum.Active },
      };
    else
      params = {
        data: { questionId: id, status: QuestionUnitStatusEnum.Inactive },
      };

    this.questionService.editStatus(params).subscribe((res) => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('questions.succes-update-status-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.getAll();
      this.notificationService.success(
        this.title,
        this.description,
        NotificationUtil.getDefaultMidConfig()
      );
    });
  }

  printQuestionUnitPdf(questionId) {
    this.printTemplateService
      .getQuestionUnitPdf(questionId)
      .subscribe((response: any) => {
        let fileName = response.headers
          .get('Content-Disposition')
          .split('filename=')[1]
          .split(';')[0];

        if (response.body.type === 'application/pdf') {
          fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
        }

        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
      });
  }

  bulkImport(): void {
    const modalRef = this.modalService.open(BulkImportQuestionsComponent, {
      centered: true,
      size: 'lg',
    });
    modalRef.result.then(() => {});
  }

  addQuestion(): void {
    this.questionByCategory.setData(this.categoryId);
    this.questionByCategory.setValue(true);
    this.router.navigate(['../questions/add-question']);
  }

  deleteQuestion(id): void {
    this.questionService.delete(id).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('questions.succes-delete-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(
        this.title,
        this.description,
        NotificationUtil.getDefaultMidConfig()
      );
      this.getAll();
    });
  }

  openConfirmationDeleteModal(id): void {
    forkJoin([
      this.translate.get('modal.delete'),
      this.translate.get('questions.delete-msg'),
      this.translate.get('button.no'),
      this.translate.get('button.yes'),
    ]).subscribe(([title, description, no, yes]) => {
      this.title = title;
      this.description = description;
      this.no = no;
      this.yes = yes;
    });
    const modalRef: any = this.modalService.open(ConfirmModalComponent, {
      centered: true,
    });
    modalRef.componentInstance.title = this.title;
    modalRef.componentInstance.description = this.description;
    modalRef.componentInstance.buttonNo = this.no;
    modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(
      () => this.deleteQuestion(id),
      () => {}
    );
  }

  goToQuestionOptions(id) {
    this.router.navigate(['../question-options', id], {
      relativeTo: this.route,
    });
  }
}
