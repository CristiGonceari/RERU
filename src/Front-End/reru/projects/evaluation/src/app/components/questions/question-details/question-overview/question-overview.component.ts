import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionUnitStatusEnum } from '../../../../utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { QuestionService } from '../../../../utils/services/question/question.service';

@Component({
  selector: 'app-question-overview',
  templateUrl: './question-overview.component.html',
  styleUrls: ['./question-overview.component.scss']
})
export class QuestionOverviewComponent implements OnInit {
  questionId: number;
  questionName: string;
  category: string;
  questionType: string;
  status: string;
  isLoading: boolean = true;
  questionPoints: number;
  tags = [];
  fileId: string;
	type = QuestionUnitTypeEnum;
	questionEnum = QuestionUnitStatusEnum;

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
        this.questionType = QuestionUnitTypeEnum[res.data.questionType];
        this.status = QuestionUnitStatusEnum[res.data.status];
        this.questionPoints = res.data.questionPoints;
        if (res.data.tags[0] != null && res.data.tags[0] != 'undefined') {
          this.tags = res.data.tags[0].split(',').join(', ')
        } else this.tags = [];
        this.isLoading = false;
        this.fileId = res.data.mediaFileId;
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
