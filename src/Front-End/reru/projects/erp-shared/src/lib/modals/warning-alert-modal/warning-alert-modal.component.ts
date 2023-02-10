import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-warning-alert-modal',
  templateUrl: './warning-alert-modal.component.html',
  styleUrls: ['./warning-alert-modal.component.scss']
})
export class WarningAlertModalComponent {
  constructor(public readonly activeModal: NgbActiveModal) { }
}
