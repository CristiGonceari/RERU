import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-delete-department-modal',
  templateUrl: './delete-department-modal.component.html',
  styleUrls: ['./delete-department-modal.component.scss']
})
export class DeleteDepartmentModalComponent extends EnterSubmitListener {
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
