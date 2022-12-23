import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { CloudFileService } from '../../services/cloud-file.service';
import { I18nService } from '../../services/i18n.service';
import { NotificationUtil } from '../../utils/notification.util';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-add-edit-media-file',
  templateUrl: './add-edit-media-file.component.html',
  styleUrls: ['./add-edit-media-file.component.scss']
})
export class AddEditMediaFileComponent implements OnInit {

  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];
  docFiles: File[] = [];

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  docUrl: any;

  filenames: any;
  fileName: string;
  fileStatus = { requestType: '', percent: 1 }

  addedFiles;
  attachedFile: File;
  isLoadingMedia: boolean = false;

  title: string;
  description: string;

  @Input() fileId: string;
  @Output() handleFile: EventEmitter<File> = new EventEmitter<File>();
  @Output() disableBtn: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor(
    private sanitizer: DomSanitizer,
    private notificationService: NotificationsService,
    private fileService: CloudFileService,
    public translate: I18nService,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
    if (this.fileId != undefined && this.fileId != 'null') {
      this.isLoadingMedia = true;
      this.getMediaFile(this.fileId);
    }
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe(res => {
      this.reportProggress(res);
    })
  }

  onSelect(event) {
    event.addedFiles.forEach((element) => {
      const regexImage = new RegExp(/(.*?).(jpg|png|jpeg|svg|gif)$/gmi);
      const regexVideo = new RegExp(/(.*?).(mp4|webm|ogv)$/gmi);
      const regexAudio = new RegExp(/(.*?).(mp3|oga|wav|ogg|aac|opus)$/gmi);
      const regexDocument = new RegExp(/(.*?).(pdf|doc|docx|ppt|pptx|xlsx|mkv|txt|xls|avi|mov|flv|odp|key|tiff)$/gmi);

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
      } else if (regexDocument.test(element.name)) {
        this.docFiles.push(...event.addedFiles);
        this.readFile(this.docFiles[0]).then(fileContents => {
          this.docUrl = fileContents;
          this.docUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.docUrl);
        });
        } else {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('media.invalid-type'),
        ]).subscribe(([title, description]) => {
          this.title = title;
          this.description = description;
        });
        this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultConfig());
        this.onRemoved();
      }

      if((this.videoFiles || this.imageFiles || this.audioFiles || this.docFiles).length > 0){
        this.fileName = event.addedFiles[0].name;
        this.attachedFile = event.addedFiles[0];
      }
      
      this.handleFile.emit(this.attachedFile);
    });
  }

  onRemoved(toDelete?: Boolean) {
    this.imageFiles = this.videoFiles = this.audioFiles = this.docFiles = [];
    this.videoUrl = this.audioUrl = this.imageUrl= this.docUrl = null;
    this.attachedFile = null;
    this.fileName = '';
    if(toDelete){
      this.handleFile.emit(this.attachedFile);
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

  private reportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.isLoadingMedia = true;
        this.fileStatus.percent = 1;
        break;
      case HttpEventType.UploadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'Uploading...')
        this.disableBtn.emit(true);
        break;
      case HttpEventType.DownloadProgress:
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'Downloading...')
        break;
      case HttpEventType.Response:
        if (httpEvent.body instanceof Array) {
          for (const filename of httpEvent.body) {
            this.filenames.unshift(filename);
          }
        } else {
          this.disableBtn.emit(false);
          this.fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
          const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
          const file = new File([blob], this.fileName, { type: httpEvent.body.type });
          this.readFile(file).then(fileContents => {
            if (blob.type.includes('image')) this.imageUrl = fileContents;
            else if (blob.type.includes('video')) this.videoUrl = fileContents;
            else if (blob.type.includes('doc')) this.docUrl = fileContents;
            else if (blob.type.includes('audio')) {
              this.audioUrl = fileContents;
              this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            }
          });
        }
        this.isLoadingMedia = false;
        break;
    }
  }

  updateStatus(loaded: number, total: number | undefined, requestType: string) {
    this.fileStatus.requestType = requestType;
    this.fileStatus.percent = Math.round(100 * loaded / total);
  }

  // showImage(url): void {
  //   const modalRef = this.modalService.open(ShowImageModalComponent, { centered: true, size: 'xl' });
  //   modalRef.componentInstance.imageUrl = url;
  // 	modalRef.result.then(
  // 		() => { }
  // 	);
  // }

}
