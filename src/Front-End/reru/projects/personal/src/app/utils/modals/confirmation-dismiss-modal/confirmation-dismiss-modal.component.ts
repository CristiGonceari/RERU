import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from '../../models/contractor.model';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-confirmation-dismiss-modal',
  templateUrl: './confirmation-dismiss-modal.component.html',
  styleUrls: ['./confirmation-dismiss-modal.component.scss'],
  providers: [SafeHtmlPipe]
})
export class ConfirmationDismissModalComponent extends EnterSubmitListener {
  @Input() contractor: Contractor;
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
