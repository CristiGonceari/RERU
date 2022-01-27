import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-confirm-reset-password-modal',
  templateUrl: './confirm-reset-password-modal.component.html',
  styleUrls: ['./confirm-reset-password-modal.component.scss']
})
export class ConfirmResetPasswordModalComponent extends EnterSubmitListener {
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
