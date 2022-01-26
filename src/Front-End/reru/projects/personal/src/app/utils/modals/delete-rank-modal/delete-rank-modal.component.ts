import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-rank-modal',
  templateUrl: './delete-rank-modal.component.html',
  styleUrls: ['./delete-rank-modal.component.scss']
})
export class DeleteRankModalComponent extends EnterSubmitListener {
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
