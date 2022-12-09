import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: './confirm-delete-evaluation-modal.component.html'
})
export class ConfirmDeleteEvaluationModalComponent {
  constructor(private readonly activeModal: NgbActiveModal) {}

  submit(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
} 
