import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { CloudFileService } from '../../services/cloud-file/cloud-file.service';

@Component({
  selector: 'app-get-media-file',
  templateUrl: './get-media-file.component.html',
  styleUrls: ['./get-media-file.component.scss']
})
export class GetMediaFileComponent implements OnInit, OnChanges {
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;

  filenames: any;
  fileName: string;
  isLoadingMedia: boolean = false;
  fileStatus = { requestType: '', percent: 1 }

  @Input() fileId: string;

  constructor(private sanitizer: DomSanitizer,
    private fileService: CloudFileService
  ) { }

  ngOnInit(): void { }

  ngOnChanges( changes: SimpleChanges ) {
    if (this.fileId != 'null' && changes.fileId.previousValue != this.fileId) {
      this.getMediaFile(this.fileId);
    }
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe( res => {
      this.reportProggress(res);
    })
  }

  private reportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.isLoadingMedia = true;
        this.fileStatus.percent = 1;
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
          const fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
          const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
          const file = new File([blob], fileName, { type: httpEvent.body.type });
          this.readFile(file).then(fileContents => {
            if (blob.type.includes('image')) this.imageUrl = fileContents;
            else if (blob.type.includes('video')) this.videoUrl = fileContents;
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
  
}
