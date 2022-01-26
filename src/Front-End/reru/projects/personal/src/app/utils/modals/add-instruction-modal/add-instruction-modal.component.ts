import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';
import {InstructionModel} from '../../models/contract.model';

@Component({
  selector: 'app-add-instruction-modal',
  templateUrl: './add-instruction-modal.component.html',
  styleUrls: ['./add-instruction-modal.component.scss']
})
export class AddInstructionModalComponent extends EnterSubmitListener implements OnInit{
  @Input() instruction: InstructionModel;
  instructionForm: FormGroup;
  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder) {
    super();
    this.callback = this.close;
   }

   ngOnInit(): void {
    this.initForm(this.instruction);
  }

  initForm(data: InstructionModel = <InstructionModel>{}): void {
    this.instructionForm = this.fb.group({
      id: this.fb.control(data.id),
      thematic: this.fb.control(data.thematic, [ Validators.pattern(/^[0-9a-zA-Z-., ]+$/)]),
      instructorName: this.fb.control(data.instructorName, [Validators.pattern(/^[a-zA-Z- ]+$/)]),
      instructorLastName: this.fb.control(data.instructorLastName, [Validators.pattern(/^[a-zA-Z- ]+$/)]),
      duration: this.fb.control(data.duration, [Validators.pattern(/^[0-9]+$/)]),
      data: this.fb.control(data.date, []),
      contractorId: this.fb.control(data.contractorId, []),
    });
  }

  close(): void {
    this.activeModal.close(this.instructionForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
