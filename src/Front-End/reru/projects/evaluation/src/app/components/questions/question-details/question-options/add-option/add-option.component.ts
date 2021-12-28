import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { OptionModel } from 'projects/evaluation/src/app/utils/models/options/option.model';
import { OptionsService } from 'projects/evaluation/src/app/utils/services/options/options.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { CloudFileService } from 'projects/evaluation/src/app/utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-add-option',
  templateUrl: './add-option.component.html',
  styleUrls: ['./add-option.component.scss']
})
export class AddOptionComponent implements OnInit {
  answer: string;
  isCorrect: any;
  questionId: any;
  optionId: any;
  mediaFileId: string;
  uploadFiles;
  addedFiles;
  attachedFile: File;

  fileId: string;
  fileType: string = null;

  allFiles: File[] = [];
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;

  filenames: any;
  fileName: string;

  isLabelHideen = false;
  isLoadingMedia: boolean;
  isLoading: boolean = true;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
    private location: Location,
    private fileService: CloudFileService,
    private notificationService: NotificationsService,
    private sanitizer: DomSanitizer
    ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.questionId = this.route.snapshot.paramMap.get('id');
    this.optionId = +this.route.snapshot.paramMap.get('id2');
    if(this.optionId) this.get();
    this.isLoading = false;
	}

  get(){
    this.optionService.get(this.optionId).subscribe(res => {
      if (res && res.data) {
        this.answer = res.data.answer;
        this.isCorrect = res.data.isCorrect;
        this.fileId = res.data.mediaFileId;
        if (res.data.mediaFileId) this.getMediaFile(this.fileId);
      }
    });
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe( res => {
      this.resportProggress(res);
    })
  }

  onSelect(event) {

    event.addedFiles.forEach((element) => {
        const regexImage = new RegExp('(.*?).(jpg|png|jpeg|svg|gif)$');
        const regexVideo = new RegExp('(.*?).(mp4|3gp|ogg|wmv|flv|avi)$');
        const regexAudio = new RegExp('(.*?).(mp3|oga|wav)$');

        this.onRemoved();

        if (regexImage.test(element.name)) {
            this.imageFiles.push(...event.addedFiles);
            this.readFile(this.imageFiles[0]).then(fileContents => {
                this.imageUrl = fileContents;
            });
        } else if (regexVideo.test(element.name)) {
            this.videoFiles.push(...event.addedFiles);
            this.readFile(this.videoFiles[0]).then(fileContents => {
                this.videoUrl = fileContents;
            });
        } else if(regexAudio.test(element.name)){
            this.audioFiles.push(...event.addedFiles);
            this.readFile(this.audioFiles[0]).then(fileContents => {
                this.audioUrl = fileContents;
                this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            });
        } else {
          this.notificationService.error('Error', 'Invalid file type',  NotificationUtil.getDefaultConfig());
        }
        this.attachedFile = event.addedFiles[0];
    });
}   

onRemoved() {
    this.imageFiles = [];
    this.videoFiles = [];
    this.audioFiles = [];
    this.videoUrl = null;
    this.audioUrl = null;
    this.imageUrl= null;
    this.isLabelHideen = false;
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

  parse() {
    this.optionId = null ? null : this.optionId;
    this.isCorrect == 'true' ? this.isCorrect : this.isCorrect = 'false';
    return {
      data: new OptionModel({
        id: this.optionId,
        answer: this.answer,
        isCorrect: this.isCorrect,
        questionUnitId: this.questionId,
        mediaFileId: this.mediaFileId
      })
    }
  }

  add(){
    const request = new FormData();
    if (this.attachedFile) {
      this.fileType = '4';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
      request.append('Answer', this.answer);
      request.append('IsCorrect', this.isCorrect);
      request.append('QuestionUnitId', this.questionId);

    this.optionService.create(request).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Option was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  edit(){
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

    this.optionService.edit(request).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Option was successfully updated', NotificationUtil.getDefaultMidConfig());
    });
  }

  confirm(){
    if(this.optionId)
      this.edit();
    else this.add();
  }

  back(){
    this.location.back();
  }
}
