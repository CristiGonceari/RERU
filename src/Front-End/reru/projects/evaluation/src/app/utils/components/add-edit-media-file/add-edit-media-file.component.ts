import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NotificationsService } from 'angular2-notifications';
import { CloudFileService } from '../../services/cloud-file/cloud-file.service';
import { NotificationUtil } from '../../util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-add-edit-media-file',
  templateUrl: './add-edit-media-file.component.html',
  styleUrls: ['./add-edit-media-file.component.scss']
})
export class AddEditMediaFileComponent implements OnInit {
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;

  filenames: any;
  fileName: string;
  
  addedFiles;
  attachedFile: File;
  isLoadingMedia: boolean = false;

  title: string;
  description: string;

  @Input() fileId: string;
  @Output() handleFile: EventEmitter<File>  = new EventEmitter<File>();

  constructor( private sanitizer: DomSanitizer,
              private notificationService: NotificationsService,
              private fileService: CloudFileService,
              public translate: I18nService,
    ) { }

  ngOnInit(): void {
    if (this.fileId != undefined) {
      this.isLoadingMedia = true;
      this.fileService.get(this.fileId).subscribe( res => {
        this.resportProggress(res);
      })
      this.getMediaFile(this.fileId);
    }
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe( res => {
      this.resportProggress(res);
    })
  }

  
  onSelect(event) {
    event.addedFiles.forEach((element) => {
        const regexImage = new RegExp(/(.*?).(jpg|png|jpeg|svg|gif)$/gmi);
        const regexVideo = new RegExp(/(.*?).(mp4|3gp|ogg|wmv|flv|avi)$/gmi);
        const regexAudio = new RegExp(/(.*?).(mp3|oga|wav)$/gmi);

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
        } else if (regexAudio.test(element.name)) {
            this.audioFiles.push(...event.addedFiles);
            this.readFile(this.audioFiles[0]).then(fileContents => {
                this.audioUrl = fileContents;
                this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            });
        } else {
          forkJoin([
            this.translate.get('modal.error'),
            this.translate.get('media.invalid-type'),
          ]).subscribe(([title, description]) => {
            this.title = title;
            this.description = description;
            });
          this.notificationService.error(this.title, this.description,  NotificationUtil.getDefaultConfig());
        }
        this.attachedFile = event.addedFiles[0];
        this.handleFile.emit(this.attachedFile);
    });
}

onRemoved() {
  this.imageFiles = [];
  this.videoFiles = [];
  this.audioFiles = [];
  this.videoUrl = null;
  this.audioUrl = null;
  this.imageUrl= null;
  this.fileName = '';
  this.handleFile.emit(this.attachedFile = null)
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

private resportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
  switch(httpEvent.type) {
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

}
