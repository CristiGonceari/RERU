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

	constructor(
		private questionService: QuestionService,
		private route: ActivatedRoute,
		private router: Router,
		private notificationService: NotificationsService,
		private modalService: NgbModal
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

		this.questionService.editStatus(params).subscribe(() => this.list());
	}

	navigate(id) {
		this.router.navigate(['question-detail/', id, 'overview'], { relativeTo: this.route });
	}

	deleteQuestion(id): void {
		this.questionService.delete(id).subscribe(() => {
			this.notificationService.success('Success', 'Question was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this question?';
		modalRef.result.then(() => this.deleteQuestion(id), () => { });
	}
}
