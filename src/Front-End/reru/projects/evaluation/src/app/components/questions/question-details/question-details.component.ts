import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { QuestionService } from '../../../utils/services/question/question.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

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
    public router: Router,
		private notificationService: NotificationsService,
		private modalService: NgbModal,
  ) {  }
  
  ngOnInit(): void {
   this.subsribeForParams();
  }

  getList(){
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
        this.getList();
    }});
	}

  changeStatus(id, status) {
		let params;

		if (status == QuestionUnitStatusEnum.Draft) 
			params = { questionId: id, status: QuestionUnitStatusEnum.Active }
		else 
			params = { questionId: id, status: QuestionUnitStatusEnum.Inactive }

		this.questionService.editStatus(params).subscribe(()=> { this.getList(); this.router.navigate(['questions/question-detail', this.questionId, 'overview'])});
	}

  deleteQuestion(id): void{
		this.questionService.delete(id).subscribe(() => 
		{
			this.notificationService.success('Success', 'Question was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.router.navigate(['/questions']);
      this.getList();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete it?';
		modalRef.result.then(() => this.deleteQuestion(id), () => { });
	}
}
