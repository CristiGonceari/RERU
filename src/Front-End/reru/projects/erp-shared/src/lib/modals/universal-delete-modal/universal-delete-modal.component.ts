import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../utils/util/submit.util';

@Component({
  selector: 'erp-shared-universal-delete-modal',
  templateUrl: './universal-delete-modal.component.html',
  styleUrls: ['./universal-delete-modal.component.scss']
})
export class UniversalDeleteModalComponent extends EnterSubmitListener {
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
