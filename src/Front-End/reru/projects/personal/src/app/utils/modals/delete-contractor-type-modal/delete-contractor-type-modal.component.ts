import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-contractor-type-modal',
  templateUrl: './delete-contractor-type-modal.component.html',
  styleUrls: ['./delete-contractor-type-modal.component.scss']
})
export class DeleteContractorTypeModalComponent {
  @Input() name: string;
  constructor(private activeModal: NgbActiveModal) { }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
