import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-ask-generate-order-modal',
  templateUrl: './ask-generate-order-modal.component.html',
  styleUrls: ['./ask-generate-order-modal.component.scss']
})
export class AskGenerateOrderModalComponent extends EnterSubmitListener {
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
