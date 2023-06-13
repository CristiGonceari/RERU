import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DomSanitizer } from '@angular/platform-browser';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../services/i18n.service';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from '../../utils/notification.util';

@Component({
  selector: 'erp-shared-upload-file-modal',
  templateUrl: './upload-file-modal.component.html',
  styleUrls: ['./upload-file-modal.component.scss']
})
export class UploadFileModalComponent implements OnInit {
  imageUrl: any;
  docUrl: any;

  fileName: string;
  attachedFile: File;

  titleNotify: string;
  descriptionNotify: string;

  files: File[] = [];
  @Input() title: string;
  @Input() description: string;

  constructor(
    public activeModal: NgbActiveModal,
    private sanitizer: DomSanitizer,
    private notificationService: NotificationsService,
    public translate: I18nService,
  ) {}

  ngOnInit(): void {
  }

  checkLength(fileName) {
    return fileName.length <= 18 ? fileName : fileName.slice(0, 18) + "...";
  }

  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  onSelect(event) {
    event.addedFiles.forEach((element) => {
      const regexImage = new RegExp(/(.*?).(jpg|png|jpeg|svg|gif)$/gmi);
      const regexDocument = new RegExp(/(.*?).(pdf|doc|docx|ppt|pptx|xlsx|mkv|txt|xls|avi|mov|flv|odp|key|tiff)$/gmi);

      if (regexImage.test(element.name)) {
        this.files.push(element);
        this.readFile(this.files[0]).then(fileContents => {
          this.imageUrl = fileContents;
        });
      } else if (regexDocument.test(element.name)) {
        this.files.push(element);
        this.readFile(this.files[0]).then(fileContents => {
          this.docUrl = fileContents;
          this.docUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.docUrl);
        });
        } else {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('media.invalid-type'),
        ]).subscribe(([title, description]) => {
          this.titleNotify = title;
          this.descriptionNotify = description;
        });
        this.notificationService.error(this.titleNotify, this.descriptionNotify, NotificationUtil.getDefaultConfig());
      }

      if((this.files).length > 0){
        this.fileName = event.addedFiles[0].name;
        this.attachedFile = event.addedFiles[0];
      }
    });
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

  uploadFile(): void {
    this.activeModal.close(this.files);
  };
}