import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-confirmation-delete-document',
  templateUrl: './confirmation-delete-document.component.html',
  styleUrls: ['./confirmation-delete-document.component.scss']
})
export class ConfirmationDeleteDocumentComponent extends EnterSubmitListener {
  @Input() name: string;
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
