import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-templates-modal',
  templateUrl: './view-templates-modal.component.html',
  styleUrls: ['./view-templates-modal.component.scss']
})
export class ViewTemplatesModalComponent implements OnInit {
  list = [];
  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }
}
