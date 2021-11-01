import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-logout-modal',
  templateUrl: './confirm-logout-modal.component.html',
  styleUrls: ['./confirm-logout-modal.component.scss']
})
export class ConfirmLogoutModalComponent {
  @Input() title: string;
  @Input() description: string;
  @Input() buttonNo: string;
  @Input() buttonYes: string;

  constructor(private activeModal: NgbActiveModal) { }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}