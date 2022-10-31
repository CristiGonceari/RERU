import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-users-modal',
  templateUrl: './view-users-modal.component.html',
  styleUrls: ['./view-users-modal.component.scss']
})
export class ViewUsersModalComponent implements OnInit {
  list = [];
  title = '';
  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  close(): void {
    this.activeModal.close();
  }

}
