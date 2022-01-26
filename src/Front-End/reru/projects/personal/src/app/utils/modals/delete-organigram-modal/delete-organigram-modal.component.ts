import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-organigram-modal',
  templateUrl: './delete-organigram-modal.component.html',
  styleUrls: ['./delete-organigram-modal.component.scss']
})
export class DeleteOrganigramModalComponent extends EnterSubmitListener {
  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
