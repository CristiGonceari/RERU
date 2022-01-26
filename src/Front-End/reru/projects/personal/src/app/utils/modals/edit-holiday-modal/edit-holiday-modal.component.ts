import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HolidayModel } from '../../models/holiday.model';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-edit-holiday-modal',
  templateUrl: './edit-holiday-modal.component.html',
  styleUrls: ['./edit-holiday-modal.component.scss']
})
export class EditHolidayModalComponent extends EnterSubmitListener implements OnInit {
  @Input() holiday: HolidayModel;
  holidayForm: FormGroup;
  constructor(private fb: FormBuilder, private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.initForm(this.holiday);
  }

  initForm(holiday: HolidayModel): void {
    this.holidayForm = this.fb.group({
      id: this.fb.control(holiday.id),
      name: this.fb.control(holiday.name, [Validators.required]),
      from: this.fb.control(holiday.from, [Validators.required]),
      to: this.fb.control(holiday.to, []),
    });
  }

  close(): void {
    this.activeModal.close(this.holidayForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
