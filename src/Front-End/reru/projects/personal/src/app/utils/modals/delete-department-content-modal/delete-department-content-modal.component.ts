import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-department-content-modal',
  templateUrl: './delete-department-content-modal.component.html',
  styleUrls: ['./delete-department-content-modal.component.scss']
})
export class DeleteDepartmentContentModalComponent {
  @Input() name: string;
  constructor(private activeModal: NgbActiveModal) { }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
