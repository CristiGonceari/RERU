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
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { CloudFileService } from 'projects/evaluation/src/app/utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';


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
	pagedSummary: PaginationModel = new PaginationModel();
  isLoadingTable: boolean = true;
  files:[] = [];
  status: number;
  isLoading: boolean = true;
  disable: boolean = false;
  edit: boolean = false;
  filenames: any;
  fileName: string;
  fileId: string;
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];
  isLoadingMedia: boolean;
  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
    private questionService: QuestionService,
    private notificationService: NotificationsService,
    private sanitizer: DomSanitizer,
    private fileService : CloudFileService,
		private modalService: NgbModal) { }

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
        console.warn('options', this.options);
        
        this.isLoading = false;
        for(let i = 0; i < this.options.length; i++){
          this.fileId = this.options[i].mediaFileId;
          console.log('file id from for', this.fileId);
          if (this.options[i].mediaFileId !== null) this.getMediaFile(this.fileId);
        }
        console.log('file iddd blabla', this.fileId);
        console.log('response', res.data);
      }
    });
  }

  getMediaFile(index) {
    console.warn('getMedia', index);
    
  //  if(this.options[index].mediaFileId !== null){
  //   this.fileId = this.options[index].mediaFileId;
  //   this.isLoadingMedia = true;
  //   this.fileService.get(this.fileId).subscribe( res => {
  //     console.log("res:", res)
  //     this.resportProggress(res);
  //   })
  //  }

  this.isLoadingMedia = true;
  this.fileService.get(index).subscribe( res => {
    console.warn('res', res);
    if (this.fileId == index)  this.resportProggress(res);
  })
    
  }

  private resportProggress(httpEvent: HttpEvent<string[] | Blob>): void
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
            if (blob.type.includes('image')) this.imageUrl = fileContents;
            else if (blob.type.includes('video')) this.videoUrl = fileContents;
            else if (blob.type.includes('audio')) {
              this.audioUrl = fileContents;
              this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            }
          });
          console.log("audioUrl")
        this.isLoadingMedia = false;
      }
      break;
    }
  }

  public async readFile(file: File): Promise<string | ArrayBuffer> {
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
