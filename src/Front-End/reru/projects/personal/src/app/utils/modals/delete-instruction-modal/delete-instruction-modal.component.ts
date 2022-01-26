import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-instruction-modal',
  templateUrl: './delete-instruction-modal.component.html',
  styleUrls: ['./delete-instruction-modal.component.scss']
})
export class DeleteInstructionModalComponent extends EnterSubmitListener {

  constructor( private activeModal: NgbActiveModal) { 
    super();
    this.callback=this.close;
  }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
