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
	type = QuestionUnitTypeEnum;
	isLoading: boolean = true;
	title: string;
  	description: string;
  	no: string;
  	yes: string;

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

	list(data: any = {}): void {
		this.keyword = data.keyword;
		let params = {
			questionName: this.keyword || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
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
