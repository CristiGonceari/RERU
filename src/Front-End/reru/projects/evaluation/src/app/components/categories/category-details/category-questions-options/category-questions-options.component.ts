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
import { CloudFileService } from 'projects/evaluation/src/app/utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { forkJoin } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

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

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;

  options = [];
  type: number;
  status: number;
  isLoading: boolean = true;
  disable: boolean = false;
  edit: boolean = false;
  questionName: string;
  fromCategory: boolean = false;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
	  public translate: I18nService,
    private sanitizer: DomSanitizer,
    private fileService : CloudFileService,
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

  parseEdit(element){
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

      return request;
  }

  updateOptions() {
    this.options.forEach(element => {
      this.optionService.edit(this.parseEdit(element)).subscribe(() => {
        this.getOptions();
        this.edit = true;
        //this.back();
      });
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

  getQuestion() {
    this.questionService.get(this.questionId).subscribe(res => {
      this.questionName = res.data.question;
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
        this.options.map ( (option) => {
          // TODO add type Option -> options = array<Option>
            option.videoUrl = null
            option.imageUrl = null
            option.audioUrl = null
            return option;
        })
        this.fileId = res.data.map(el => el.mediaFileId);
        for (let i = 0; i < this.fileId.length; i++) {
          if (this.fileId[i] !== null) this.getMediaFile(this.fileId[i], i);
        }
      }
    });
  }

  getMediaFile(fileId, index) {
    this.fileService.get(fileId).subscribe(res => {
      this.resportProggress(res, index);
    })
  }

  private resportProggress(httpEvent: HttpEvent<string[] | Blob>, index): void
  { 
    switch(httpEvent.type)
    {
      case HttpEventType.Response:
        if (httpEvent.body instanceof Array) {
          for (const filename of httpEvent.body) {
            this.filenames.unshift(filename);
          }
        } else {
          this.fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
          const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
          const file = new File([blob], this.fileName, { type: httpEvent.body.type });
          this.readFile(file).then(fileContents => {
            if (blob.type.includes('image')) {
              this.options[index].imageUrl = fileContents;
            }
            else if (blob.type.includes('video')) {
              this.options[index].videoUrl = fileContents;
            }
            else if (blob.type.includes('audio')) {
              this.options[index].audioUrl = fileContents;
              this.options[index].audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.options[index].audioUrl);
            }
            this.isLoadingMedia = false;
          });
        }
      break;
    }
  }

  public readFile(file: File): Promise<string | ArrayBuffer> {
    
    return new Promise<string | ArrayBuffer>((resolve, reject) => {
      const reader = new FileReader();
  
      reader.onload = e => {
        return resolve((e.target as FileReader).result);
      };

      reader.onerror = e => {
        console.error(`FileReader failed on file ${file.name}.`);
        return reject(null);
      };

      if (!file) {
        console.error('No file to read.');
        return reject(null);
      }

      reader.readAsDataURL(file);
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
}
