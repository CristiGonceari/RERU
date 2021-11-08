import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { QuestionService } from '../../../utils/services/question/question.service';

@Component({
  selector: 'app-question-details',
  templateUrl: './question-details.component.html',
  styleUrls: ['./question-details.component.scss']
})
export class QuestionDetailsComponent implements OnInit {
  questionId: number;
  questionName: string;
  type: string;
  status: string;
  isLoading: boolean = true;

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
        this.type = res.data.questionType;
        this.status = res.data.status;
        this.isLoading = false;
      }
    })
  }
  
  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.questionId = params.id;
			if (this.questionId) {
        this.get();
    }});
	}

  changeStatus(id, status) {
		let params;

		if (status == QuestionUnitStatusEnum.Draft) 
			params = { questionId: id, status: QuestionUnitStatusEnum.Active }
		else 
			params = { questionId: id, status: QuestionUnitStatusEnum.Inactive }

		this.questionService.editStatus(params).subscribe(()=> { this.get(); this.router.navigate(['questions/question-detail', this.questionId, 'overview'])});
	}
}
