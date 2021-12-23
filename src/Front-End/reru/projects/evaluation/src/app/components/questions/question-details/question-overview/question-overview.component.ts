import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionUnitStatusEnum } from '../../../../utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from '../../../../utils/enums/question-unit-type.enum';
import { CloudFileService } from '../../../../utils/services/cloud-file/cloud-file.service';
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
  type: string;
  status: string;
  isLoading: boolean = true;
  isLoadingMedia: boolean;
  questionPoints: number;
  tags = [];
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];
  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  filenames: any;
  fileName: string;
  fileId: string;

  constructor(
		private questionService: QuestionService,
		private activatedRoute: ActivatedRoute,
    public router: Router,
    private fileService : CloudFileService,
    private sanitizer: DomSanitizer
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
        this.tags = res.data.tags[0] != 'undefined' ? this.tags = res.data.tags.join(', ') : this.tags = null;
        this.isLoading = false;
        this.fileId = res.data.mediaFileId;
        if (res.data.mediaFileId) this.getMediaFile(this.fileId);
      }
    })
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe( res => {
      this.resportProggress(res);
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
  
  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.questionId = params.id;
			if (this.questionId) {
        this.get();
    }});
	}
}
