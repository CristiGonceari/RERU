import { Component, OnInit } from '@angular/core';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { QuestionUnit } from 'projects/evaluation/src/app/utils/models/question-units/question-unit.model';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-question-list-table',
  templateUrl: './question-list-table.component.html',
  styleUrls: ['./question-list-table.component.scss']
})
export class QuestionListTableComponent implements OnInit {
  questionList: QuestionUnit[] = [];
	pagination: PaginationModel = new PaginationModel();
	qType: string[];
	questionEnum = QuestionUnitStatusEnum;
	keyword: string;
	type = QuestionUnitTypeEnum;
	isLoading: boolean = true;

	constructor(private questionService: QuestionService, private route: ActivatedRoute, private router: Router) { }

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
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}
		this.questionService.getAll(params).subscribe((res) => {
			console.log('pagedSummary', res.data.pagedSummary);
			
			if (res && res.data.items) {
				this.questionList = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.qType = Object.keys(QuestionUnitTypeEnum)
					.map(key => QuestionUnitTypeEnum[key])
					.filter(value => typeof value === 'string') as string[];
				this.isLoading = false;
				console.log("res", res);
				
			}
		});
	}

	changeStatus(id, status) {
		let params;

		if (status == QuestionUnitStatusEnum.Draft) 
			params = {data:{ questionId: id, status: QuestionUnitStatusEnum.Active }}
		else 
			params = {data:{ questionId: id, status: QuestionUnitStatusEnum.Inactive }}

		this.questionService.editStatus(params).subscribe(() => this.list());
	}

	navigate(id){
		this.router.navigate(['question-detail/', id, 'overview'], {relativeTo: this.route});
	}

}
