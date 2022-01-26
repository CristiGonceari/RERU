import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ContactModel } from '../../models/contact.model';
import { Contractor } from '../../models/contractor.model';
import { SelectItem } from '../../models/select-item.model';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-confirmation-contact-delete-modal',
  templateUrl: './confirmation-contact-delete-modal.component.html',
  styleUrls: ['./confirmation-contact-delete-modal.component.scss']
})
export class ConfirmationContactDeleteModalComponent extends EnterSubmitListener {
  @Input() contractor: Contractor;
  @Input() contact: ContactModel;
  types: SelectItem[] = [];
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
