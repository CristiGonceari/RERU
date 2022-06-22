import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { InregistrationUserService } from '../../services/inregistration-user.service';
import { ValidatorUtil } from '../../util/validator.util';

@Component({
  selector: 'app-verify-email-code-modal',
  templateUrl: './verify-email-code-modal.component.html',
  styleUrls: ['./verify-email-code-modal.component.scss']
})
export class VerifyEmailCodeModalComponent implements OnInit {
  codeForm: FormGroup;

  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private inregistrationService: InregistrationUserService
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.codeForm = this.fb.group({
      code: this.fb.control(null, [Validators.required, Validators.maxLength(4), Validators.minLength(4)])
    })
  }

  hasErrors(field): boolean {
    return this.codeForm.touched && this.codeForm.get(field).invalid;
  }

  isIdnpLengthValidator(field: string): boolean {
    return ValidatorUtil.isIdnpLengthValidator(this.codeForm, field);
  }

  hasError(field: string, error = 'required'): boolean {
    return (
      this.codeForm.get(field).invalid &&
      this.codeForm.get(field).touched &&
      this.codeForm.get(field).hasError(error)
    );
  }

  getCode(){
    this.inregistrationService.shareCode(this.codeForm.value.code);
  }
 
  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
