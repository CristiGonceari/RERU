import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { OptionModel } from 'projects/evaluation/src/app/utils/models/options/option.model';
import { OptionsService } from 'projects/evaluation/src/app/utils/services/options/options.service';
import { QuestionService } from 'projects/evaluation/src/app/utils/services/question/question.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { BulkImportOptionsComponent } from '../../bulk-import-options/bulk-import-options.component';

@Component({
  selector: 'app-category-questions-options',
  templateUrl: './category-questions-options.component.html',
  styleUrls: ['./category-questions-options.component.scss']
})
export class CategoryQuestionsOptionsComponent implements OnInit {
  optionForm: FormGroup;

  answer: string;
  isCorrect: any;
  questionId: any;
  optionId: any;

  attachedFile: File;
  fileType: string = null;
  filenames: any;
  fileName: string;
  fileId = [];
  isLoadingMedia: boolean = true;
  questionFileId: string;
  
  title: string;
  description: string;
  no: string;
  yes: string;

  options = [];
  type: number;
  status: number;
  isLoading: boolean = true;
  disable: boolean = false;
  questionName: string;
  fromCategory: boolean = false;
  questionType;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
	  public translate: I18nService,
    private questionService: QuestionService,
    private notificationService: NotificationsService,
		private modalService: NgbModal,
    private location: Location
    ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    if (window.location.href.includes('question-category')) {
      this.fromCategory = true;
      this.route.params.subscribe(params => {
        this.questionId = params.id;
      });
    } else {
      this.route.parent.params.subscribe(params => {
        this.questionId = params.id;
      });
    }
    if (this.questionId) {
      this.getOptions();
      this.getQuestion();
    }
  }

  onItemChange(event) {
    const request = new FormData();

    if (this.attachedFile)
    {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
      request.append('Data.Id', this.optionId);
      request.append('Data.Answer', this.answer);
      request.append('Data.IsCorrect', this.isCorrect);
      request.append('Data.QuestionUnitId', this.questionId);

    this.options.forEach(request => {
      if (request.id == event.target.value) {
        request.isCorrect = event.target.checked;
      } else {
        if (this.type == 3) {
          request.isCorrect = false;
        }
      }
    });
  }

  parseEdit(element) {
    const request = new FormData();
    
    if (this.attachedFile)
    {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
      request.append('Data.Id', element.id);
      request.append('Data.Answer', element.answer);
      request.append('Data.IsCorrect', element.isCorrect);
      request.append('Data.QuestionUnitId', element.questionUnitId);
      if (element.mediaFileId) {
        request.append('Data.MediaFileId', element.mediaFileId);
      }

      return request;
  }

  updateOptions() {
    if (this.options.length) {
      this.options.forEach(element => {
        this.optionService.edit(this.parseEdit(element)).subscribe(
          () => {},
          () => this.getOptions()
        );
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('options.succes-update-options-msg'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
          });
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }
  }

  getQuestion() {
    this.questionService.get(this.questionId).subscribe(res => {
      this.questionName = res.data.question;
      this.questionType = res.data.questionType;
      this.type = res.data.questionType;
      this.status = res.data.status;
      this.questionFileId = res.data.mediaFileId;
      if(this.status == QuestionUnitStatusEnum.Active || this.status == QuestionUnitStatusEnum.Inactive)
        this.disable = true
    })
  }

  getOptions() {
    this.optionService.getAll(this.questionId).subscribe(res => {
      if (res && res.data) {
        this.isLoadingMedia = false;
        this.options = res.data;
      }
    });
  }

  deleteQuestion(id): void{
		this.optionService.delete(id).subscribe(() => 
		{
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('options.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.getOptions();
		});
	}

	openConfirmationDeleteModal(id): void {
    forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('options.delete-msg'),
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

  back(){
    this.location.back();
  }

  bulkImport(): void { 
		const modalRef = this.modalService.open(BulkImportOptionsComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.questionId = this.questionId
		modalRef.result.then(() => this.subsribeForParams(),
			() => { }
		);
	}
}
