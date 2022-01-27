import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-nomenclature-modal',
  templateUrl: './delete-nomenclature-modal.component.html',
  styleUrls: ['./delete-nomenclature-modal.component.scss']
})
export class DeleteNomenclatureModalComponent extends EnterSubmitListener {
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
