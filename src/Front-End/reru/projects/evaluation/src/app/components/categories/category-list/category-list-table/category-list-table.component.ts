import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { QuestionCategory } from '../../../../utils/models/question-category/question-category.model';
import { QuestionCategoryService } from '../../../../utils/services/question-category/question-category.service';
import { QuestionByCategoryService } from '../../../../utils/services/question-by-category/question-by-category.service';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { NotificationsService } from 'angular2-notifications';
import { BulkImportQuestionsComponent } from '../../../questions/bulk-import-questions/bulk-import-questions.component';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { saveAs } from 'file-saver';
import { ParsePrintTabelService } from '../../../../utils/services/parse-print-table/parse-print-tabel.service';
import { ObjectUtil } from 'projects/evaluation/src/app/utils/util/object.util';

@Component({
  selector: 'app-category-list-table',
  templateUrl: './category-list-table.component.html',
  styleUrls: ['./category-list-table.component.scss']
})
export class CategoryListTableComponent implements OnInit {

  	questionCategories: QuestionCategory[] = [];
  	pagedSummary: PaginationModel = new PaginationModel();
	name = '';
	keyword: string;
	isLoading: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

  constructor(
   		private questionCategoryService: QuestionCategoryService,
		private modalService: NgbModal,
		public translate: I18nService,
		private router: Router,
		private questionByCategory: QuestionByCategoryService,
		private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private parsePrintTabelService: ParsePrintTabelService
  ) { }

  	ngOnInit(): void {
		this.list();
 	}

  	list(data: any = {}) {		
		this.isLoading = true;
		this.keyword = data.keyword;
		let params = ObjectUtil.preParseObject({
			name: this.keyword || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		})

		this.questionCategoryService.getCategories(params).subscribe(
			res => {
				if (res && res.data) {
					this.questionCategories = res.data.items;
					this.pagedSummary = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'questionCount'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true  })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.keyword || ''
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      		this.translate.get('print.select-file-format')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.questionCategoryService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				let fileNameParsed = this.parsePrintTabelService.parseFileName(data.tableName, fileName);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileNameParsed.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	addQuestion(categoryId): void {
		this.questionByCategory.setData(categoryId);
		this.questionByCategory.setValue(true);
		this.router.navigate(['../questions/add-question']);
	}

	bulkImport(): void {
		const modalRef = this.modalService.open(BulkImportQuestionsComponent, { centered: true, size: 'lg' });
		modalRef.result.then(
			() => this.list(),
			() => { }
		);
	}

	navigate(id){
		this.router.navigate(['question-category/', id, 'overview'], {relativeTo: this.route});
	}

	deleteCategory(id): void{
		this.questionCategoryService.delete(id).subscribe(() => 
		{
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('categories.succes-delete-msg'),
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
			this.translate.get('categories.delete-msg'),
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
		modalRef.result.then(() => this.deleteCategory(id), () => { });
	}
}
