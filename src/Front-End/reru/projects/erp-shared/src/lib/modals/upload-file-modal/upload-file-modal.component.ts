import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'erp-shared-upload-file-modal',
  templateUrl: './upload-file-modal.component.html',
  styleUrls: ['./upload-file-modal.component.scss']
})
export class UploadFileModalComponent implements OnInit {
  files: File[] = [];
  @Input() title: string;
  @Input() description: string;

  constructor(
    public activeModal: NgbActiveModal,
  ) {}

  ngOnInit(): void {
  }

  onSelect(event) {
    this.files.push(...event.addedFiles);
  }

  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  uploadFile(): void {
    this.activeModal.close(this.files);
  };

}
