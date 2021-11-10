import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { OptionsService } from '../../../../utils/services/options/options.service'
import { OptionModel } from 'projects/evaluation/src/app/utils/models/options/option.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@erp/shared';


@Component({
  selector: 'app-question-options',
  templateUrl: './question-options.component.html',
  styleUrls: ['./question-options.component.scss']
})
export class QuestionOptionsComponent implements OnInit {
  options = [];
  questionId;
  type: number;
  newList = [];
  status: number;
  isLoading: boolean = true;
  disable: boolean = false;
  edit: boolean = false;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
    private questionService: QuestionService,
    private notificationService: NotificationsService,
		private modalService: NgbModal,) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.questionId = params.id;
      if (this.questionId) {
        this.getOptions();
        this.getQuestion();
      }
    });
  }

  onItemChange(event) {
    this.options.forEach(el => {
      if (el.id == event.target.value) {
        el.isCorrect = event.target.checked;
      } else {
        if (this.type == 3) {
          el.isCorrect = false;
        }
      }
    });
  }

  updateOptions() {
    this.options.forEach(el => {
      this.optionService.edit(this.parse(el)).subscribe(() => {
        this.getOptions();
        this.edit = true;
      });
    });

    this.notificationService.success('Success', 'Options was successfully updated', NotificationUtil.getDefaultMidConfig());
  }

  parse(element) {
    return {
      data: new OptionModel({
        id: element.id,
        answer: element.answer,
        isCorrect: element.isCorrect,
        questionUnitId: element.questionUnitId
      })
    }
  }

  getQuestion() {
    this.questionService.get(this.questionId).subscribe(res => {
      this.type = res.data.questionType;
      this.status = res.data.status;
      if(this.status == QuestionUnitStatusEnum.Active || this.status == QuestionUnitStatusEnum.Inactive)
        this.disable = true
    })
  }

  getOptions(): void {
    this.optionService.getAll(this.questionId).subscribe(res => {
      if (res && res.data) {
        this.options = res.data;
        this.isLoading = false;
      }
    });
  }

  deleteQuestion(id): void{
		this.optionService.delete(id).subscribe(() => 
		{
			this.notificationService.success('Success', 'Option was successfully deleted', NotificationUtil.getDefaultMidConfig());
      this.getOptions();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this option?';
		modalRef.result.then(() => this.deleteQuestion(id), () => { });
	}

}
