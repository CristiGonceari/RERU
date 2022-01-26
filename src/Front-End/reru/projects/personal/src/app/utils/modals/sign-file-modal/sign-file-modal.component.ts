import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-sign-file-modal',
  templateUrl: './sign-file-modal.component.html',
  styleUrls: ['./sign-file-modal.component.scss']
})
export class SignFileModalComponent extends EnterSubmitListener {
  documents: any[] = [];
  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  close(): void {
    if (!this.documents[0]) return;

    this.activeModal.close(this.documents[0]);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  onSelectDocument(event): void {
    this.documents[0] = event.addedFiles[0];
  }

  onRemoveDocument(event): void {
    this.documents.splice(this.documents.indexOf(event), 1);
  }
}
