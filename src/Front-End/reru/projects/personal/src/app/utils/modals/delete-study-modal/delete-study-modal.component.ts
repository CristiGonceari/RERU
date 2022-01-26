import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-study-modal',
  templateUrl: './delete-study-modal.component.html',
  styleUrls: ['./delete-study-modal.component.scss']
})
export class DeleteStudyModalComponent extends EnterSubmitListener {
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
