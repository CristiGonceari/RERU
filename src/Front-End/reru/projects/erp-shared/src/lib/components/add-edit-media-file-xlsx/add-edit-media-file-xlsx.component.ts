import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { CloudFileService } from '../../services/cloud-file.service';
import { I18nService } from '../../services/i18n.service';

@Component({
  selector: 'app-add-edit-media-file-xlsx',
  templateUrl: './add-edit-media-file-xlsx.component.html'
})
export class AddEditMediaFileXlsxComponent implements OnInit {

  files: File[] = [];
  fileUrl: any;

  filenames: any;
  fileName: string;
  fileStatus = { requestType: '', percent: 1 }

  addedFiles;
  attachedFile: File;
  isLoadingMedia: boolean = false;

  @Input() fileId: string;
  @Output() handleFile: EventEmitter<File> = new EventEmitter<File>();
  @Output() disableBtn: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor(
    private sanitizer: DomSanitizer,
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
    event.addedFiles.forEach(() => {
      this.files.push(...event.addedFiles);
      this.readFile(this.files[0]).then(fileContents => {
        this.fileUrl = fileContents;
        this.fileUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.fileUrl);
      });

      if((this.files).length > 0){
        this.fileName = event.addedFiles[0].name;
        this.attachedFile = event.addedFiles[0];
      }
      
      this.handleFile.emit(this.attachedFile);
    });
  }

  onRemoved(toDelete?: Boolean) {
    this.files = [];
    this.fileUrl = null;
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
    let status = '';
    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.isLoadingMedia = true;
        this.fileStatus.percent = 1;
        break;
      case HttpEventType.UploadProgress:
        this.disableBtn.emit(true);
        this.translate.get('processes.upload').subscribe((res: string) => {
          status = res;
        });
        this.updateStatus(httpEvent.loaded, httpEvent.total, status);
        break;
      case HttpEventType.DownloadProgress:
        this.translate.get('processes.download').subscribe((res: string) => {
          status = res;
        });
        this.updateStatus(httpEvent.loaded, httpEvent.total, status);
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
          this.readFile(file).then(fileContents => { this.fileUrl = fileContents; });
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