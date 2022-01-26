import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from '../../models/contractor.model';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-confirmation-delete-contractor',
  templateUrl: './confirmation-delete-contractor.component.html',
  styleUrls: ['./confirmation-delete-contractor.component.scss'],
  providers: [SafeHtmlPipe]
})
export class ConfirmationDeleteContractorComponent extends EnterSubmitListener {
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
