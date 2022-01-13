import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { QuestionService } from '../../../utils/services/question/question.service';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

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
  title: string;
  description: string;
  no: string;
  yes: string;

  constructor(
		private questionService: QuestionService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
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
			params = { questionId: +id, status: QuestionUnitStatusEnum.Active }
		else 
			params = { questionId: +id, status: QuestionUnitStatusEnum.Inactive }

		this.questionService.editStatus({data : params}).subscribe(()=> {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-update-status-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				}); 
      this.getList(); 
      this.router.navigate(['questions/question-detail', this.questionId, 'overview'])
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
	}

  deleteQuestion(id): void{
		this.questionService.delete(id).subscribe(() =>
		{
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.router.navigate(['/questions']);
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
