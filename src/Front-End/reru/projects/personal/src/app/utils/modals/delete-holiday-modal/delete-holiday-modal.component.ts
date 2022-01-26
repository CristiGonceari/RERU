import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-holiday-modal',
  templateUrl: './delete-holiday-modal.component.html',
  styleUrls: ['./delete-holiday-modal.component.scss']
})
export class DeleteHolidayModalComponent extends EnterSubmitListener {
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
