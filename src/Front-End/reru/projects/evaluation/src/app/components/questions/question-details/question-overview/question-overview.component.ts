import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-type.enum';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';

@Component({
  selector: 'app-question-overview',
  templateUrl: './question-overview.component.html',
  styleUrls: ['./question-overview.component.scss']
})
export class QuestionOverviewComponent implements OnInit {
  questionId: number;
  questionName: string;
  category: string;
  type: string;
  status: string;
  isLoading: boolean = true;
  questionPoints: number;
  tags = [];

  constructor(
		private questionService: QuestionService,
		private activatedRoute: ActivatedRoute,
    public router: Router
  ) {  }
  
  ngOnInit(): void {
   this.subsribeForParams();
  }

  get(){
    this.questionService.get(this.questionId).subscribe(res => {
      if (res && res.data) {
        this.questionName = res.data.question;
        this.category = res.data.categoryName;
        this.type = QuestionUnitTypeEnum[res.data.questionType];
        this.status = QuestionUnitStatusEnum[res.data.status];
        this.questionPoints = res.data.questionPoints;
        this.tags = res.data.tags.join(', ');
        this.isLoading = false;
      }
    })
  }

  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.questionId = params.id;
			if (this.questionId) {
        this.get();
    }});
	}
}
