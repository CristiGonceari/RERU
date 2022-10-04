import { Component, OnInit } from '@angular/core';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionUnit } from 'projects/evaluation/src/app/utils/models/question-units/question-unit.model';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { saveAs } from 'file-saver';
import { PrintTemplateService } from 'projects/evaluation/src/app/utils/services/print-template/print-template.service';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
	selector: 'app-question-list-table',
	templateUrl: './question-list-table.component.html',
	styleUrls: ['./question-list-table.component.scss']
})
export class QuestionListTableComponent implements OnInit {
	questionList: QuestionUnit[] = [];
	pagedSummary: PaginationModel = new PaginationModel();
	qType: string[];
	questionEnum = QuestionUnitStatusEnum;

	keyword: string;
	categoryName: string;
	questionTag: string;
	questionStatus: string = '';
	questionType: string = '';

	type = QuestionUnitTypeEnum;
	isLoading: boolean = true;
	title: string;
  	description: string;
  	no: string;
  	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	filters: any = {}

	constructor(
		private questionService: QuestionService,
		private route: ActivatedRoute,
		private router: Router,
		private notificationService: NotificationsService,
		public translate: I18nService,
		private modalService: NgbModal,
		private printTemplateService: PrintTemplateService
	) { }

	ngOnInit(): void {
		this.subscribeForQuestions();
	}

	subscribeForQuestions(): void {
		this.questionService.uploadQuestions.subscribe(() => this.list());
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['question', 'categoryName', 'questionType', 'status'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			questionName: this.filters.questionName,
			categoryName: this.filters.categoryName,
			questionTags: this.filters.questionTags,
			status: +this.filters.questionStatus,
			type: +this.filters.questionType
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
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		let fileName: string;
		this.questionService.print(data).subscribe(response => {
			if (response) {
				fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				fileName = this.ceckFileName(fileName);
				
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	ceckFileName(fileName): string {
		if(fileName.includes("_") && (fileName.match(new RegExp("_", "g")) || []).length <= 3){
			fileName = "Întrebari";
		} else if(fileName.includes("_")){
			fileName = "Вопросы";
		}

		return fileName;
	} 

	list(data: any = {}): void {
		let params = {
			questionName: this.keyword || '',
			categoryName: this.categoryName || '',
			questionTags: this.questionTag || '',
			status: this.questionStatus,
			type: this.questionType,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
			...this.filters
			}
		this.questionService.getAll(params).subscribe((res) => {
			if (res && res.data.items) {
				this.questionList = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.qType = Object.keys(QuestionUnitTypeEnum)
					.map(key => QuestionUnitTypeEnum[key])
					.filter(value => typeof value === 'string') as string[];
				this.isLoading = false;
			}
		});
	}

	setFilter(field: string, value): void {
		this.filters[field] = value;
		this.pagedSummary.currentPage = 1;
		this.list();
	}

	resetFilters(): void {
		this.filters = {};
		this.questionType = '';
		this.questionStatus = '';
		this.pagedSummary.currentPage = 1;
		this.list();
	}

	changeStatus(id, status) {
		let params;

		if (status == QuestionUnitStatusEnum.Draft)
			params = { data: { questionId: id, status: QuestionUnitStatusEnum.Active } }
		else
			params = { data: { questionId: id, status: QuestionUnitStatusEnum.Inactive } }

		this.questionService.editStatus(params).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-update-status-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.list();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		}
		);
	}

	navigate(id) {
		this.router.navigate(['question-detail/', id, 'overview'], { relativeTo: this.route });
	}

	printQuestionUnitPdf(questionId){
		this.printTemplateService.getQuestionUnitPdf(questionId).subscribe((response : any) => {
			let fileName = response.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
			if (response.body.type === 'application/pdf') {
			  fileName = fileName.replace(/(\")|(\.pdf)|(\')/g, '');
			}
	  
			const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
			saveAs(file);
			  });
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
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
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
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteQuestion(id), () => { });
	}
}
