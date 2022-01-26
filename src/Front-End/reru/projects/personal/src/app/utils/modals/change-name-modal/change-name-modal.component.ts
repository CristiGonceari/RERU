import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from '../../models/contractor.model';
import { EnterSubmitListener } from '../../util/submit.util';

interface ChangeUserNameModel {
  id: number;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-change-name-modal',
  templateUrl: './change-name-modal.component.html',
  styleUrls: ['./change-name-modal.component.scss']
})
export class ChangeNameModalComponent extends EnterSubmitListener implements OnInit {
  @Input() contractor: Contractor;
  user: ChangeUserNameModel = {
    id: null,
    firstName: '',
    lastName: ''
  };
  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.user.id = this.contractor.id;
    this.user.firstName = this.contractor.firstName;
    this.user.lastName = this.contractor.lastName;
  }

  close(): void {
    this.activeModal.close(this.user);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
