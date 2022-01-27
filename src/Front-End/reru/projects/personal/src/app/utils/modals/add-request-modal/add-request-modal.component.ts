import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from '../../models/contractor.model';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-request-modal',
  templateUrl: './add-request-modal.component.html',
  styleUrls: ['./add-request-modal.component.scss']
})
export class AddRequestModalComponent extends EnterSubmitListener implements OnInit {
  
  @Input() contractor: Contractor;
  requestForm: FormGroup;
  today = new Date().toISOString();

  constructor(private fb: FormBuilder, private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.requestForm = this.fb.group({
      fromDate: this.fb.control(null, [])
    });
  }

  close(): void {
    this.activeModal.close(this.requestForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
