import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from '../../models/contractor.model';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-request-by-hr-modal',
  templateUrl: './add-request-by-hr-modal.component.html',
  styleUrls: ['./add-request-by-hr-modal.component.scss']
})
export class AddRequestByHrModalComponent extends EnterSubmitListener implements OnInit {

  @Input() contractor: Contractor;
  @Input() contractorId: number;
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
      from: this.fb.control(null, []),
      contractorId: this.fb.control(this.contractorId)
    });
  }

  close(): void {
    this.activeModal.close(this.requestForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
