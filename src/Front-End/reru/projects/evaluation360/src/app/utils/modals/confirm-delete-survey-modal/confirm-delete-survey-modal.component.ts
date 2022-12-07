import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-delete-survey-modal',
  templateUrl: './confirm-delete-survey-modal.component.html',
  styleUrls: ['./confirm-delete-survey-modal.component.scss']
})
export class ConfirmDeleteSurveyModalComponent {
  constructor(private activeModal: NgbActiveModal) {}

  submit(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
} 
