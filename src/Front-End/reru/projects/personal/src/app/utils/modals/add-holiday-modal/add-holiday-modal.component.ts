import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-holiday-modal',
  templateUrl: './add-holiday-modal.component.html',
  styleUrls: ['./add-holiday-modal.component.scss']
})
export class AddHolidayModalComponent extends EnterSubmitListener implements OnInit {
  holidayForm: FormGroup;
  isHideToDate: boolean;
  constructor(private fb: FormBuilder, private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.holidayForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      from: this.fb.control(null, [Validators.required]),
      to: this.fb.control(null, [Validators.required])
    });
  }

  close(): void {
    this.activeModal.close(this.holidayForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  changeVacationDuration(event): void {
    if (event.target.checked) {
      this.holidayForm.get('to').patchValue(null);
      this.holidayForm.get('to').setValidators([]);
      this.isHideToDate = true;
      this.holidayForm.get('to').updateValueAndValidity();
    } else {
      this.holidayForm.get('to').setValidators([Validators.required]);
      this.isHideToDate = false;
      this.holidayForm.get('to').updateValueAndValidity();
    }
  }
}
