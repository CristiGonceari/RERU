import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NotificationsService } from 'angular2-notifications';
import { CloudFileService } from '../../services/cloud-file.service';
import { I18nService } from '../../services/i18n.service';
import { NotificationUtil } from '../../utils/notification.util';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html'
})
export class AddDocumentComponent implements OnInit {

  imageFiles: File[] = [];
  docFiles: File[] = [];
  imageUrl: any;
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
    public translate: I18nService
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
      const regexDocument = new RegExp(/(.*?).(pdf|doc|docx|txt)$/gmi);

      this.onRemoved();

      if (regexImage.test(element.name)) {
        this.imageFiles.push(...event.addedFiles);
        this.readFile(this.imageFiles[0]).then(fileContents => {
          this.imageUrl = fileContents;
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

      if((this.imageFiles || this.docFiles).length > 0){
        this.fileName = event.addedFiles[0].name;
        this.attachedFile = event.addedFiles[0];
      }
      
      this.handleFile.emit(this.attachedFile);
    });
  }

  onRemoved(toDelete?: Boolean) {
    this.imageFiles = this.docFiles = [];
    this.imageUrl= this.docUrl = null;
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
        this.updateStatus(httpEvent.loaded, httpEvent.total, 'Se încarcă...')
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
            else if (blob.type.includes('application')) this.docUrl = fileContents;
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
}