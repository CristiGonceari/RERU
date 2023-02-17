import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';
import { ValidatorUtil } from '../../util/validator.util';

@Component({
  selector: 'app-bulletin-address-modal',
  templateUrl: './bulletin-address-modal.component.html',
  styleUrls: ['./bulletin-address-modal.component.scss']
})
export class BulletinAddressModalComponent extends EnterSubmitListener {
  @Input() addressForm: FormGroup;
  @Input() isBirthPlace: boolean;

  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.addressForm, field);
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.addressForm, field);
  }
  
  close(): void {
    this.activeModal.close(this.addressForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
