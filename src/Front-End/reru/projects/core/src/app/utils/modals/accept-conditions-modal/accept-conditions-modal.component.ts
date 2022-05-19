import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-accept-conditions-modal',
  templateUrl: './accept-conditions-modal.component.html',
  styleUrls: ['./accept-conditions-modal.component.scss']
})
export class AcceptConditionsModalComponent implements OnInit {

  constructor(
    private activeModal: NgbActiveModal,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
