import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';
import { VacationTypeEnum } from '../../models/vacation-type.enum';
import { ValidatorUtil } from '../../util/validator.util';
import { VacationProfileService } from '../../services/vacation-profile.service';
import { CkEditorConfigComponent } from '../../components/ck-editor-config/ck-editor-config.component';


@Component({
  selector: 'app-add-vacation-modal',
  templateUrl: './add-vacation-modal.component.html',
  styleUrls: ['./add-vacation-modal.component.scss']
})
export class AddVacationModalComponent extends EnterSubmitListener implements OnInit {
  @Input() id: number;
  @ViewChild(CkEditorConfigComponent) child!: CkEditorConfigComponent;

  vacationForm: FormGroup;
  isSingleDay: boolean;
  vacationTypes = VacationTypeEnum;
  availableDays: number;
  intervalDays: number;
  value: number;
  documentEditedValue: string;

  constructor(private activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private vacationProfileService: VacationProfileService) {
    super();
    this.callback = this.close;
  }

  ngOnInit(): void {
    this.initForm();
    this.subscribeForChanges();
  }

  availableDaysForTypesOfVacantion(event): void{
    if(this.id ){
    this.vacationProfileService.getAvailableDaysForSelectedContractor({contractorId: this.id, vacantionTypeId: event}).subscribe(res => {
       this.availableDays = res.data;
    })
    return
  }
    this.vacationProfileService.getAvailableDays({vacantionTypeId: event}).subscribe(response => {
      this.availableDays = response.data;
    });
  }

  retriveDocumentValueFromComponent($event){
    this.documentEditedValue = $event;
    this.vacationForm.controls['docsValue'].setValue($event);
  }

  retrieveIntervalDays(data): void {
    this.vacationProfileService.getIntervalDays(data).subscribe(response => {
      this.intervalDays = response.data;
    });
  }

  subscribeForChanges(): void {
    this.vacationForm.get('fromDate').valueChanges.subscribe(response => {
      if (response && this.vacationForm.get('toDate').value) this.retrieveIntervalDays({ from: this.vacationForm.get('fromDate').value, to: this.vacationForm.get('toDate').value });
    })

    this.vacationForm.get('toDate').valueChanges.subscribe(response => {
      if (response && this.vacationForm.get('fromDate').value) this.retrieveIntervalDays({ from: this.vacationForm.get('fromDate').value, to: this.vacationForm.get('toDate').value });
    })
  }

  initForm(): void {
    this.vacationForm = this.fb.group({
      mentions: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      toDate: this.fb.control(null, [Validators.required]),
      vacationType: this.fb.control(null, [Validators.required]),
      institution: this.fb.control(null),
      childAge: this.fb.control(null),
      docsValue: this.fb.control(null, [Validators.required])
    });

    this.vacationForm.get('vacationType').valueChanges.subscribe(response => {
      if (response == VacationTypeEnum.ChildCare) {
        this.vacationForm.get('childAge').setValidators([Validators.pattern(/^[0-9]+$/)]);
        this.vacationForm.get('childAge').updateValueAndValidity();
      } else {
        this.vacationForm.get('childAge').setValidators([]);
        this.vacationForm.get('childAge').updateValueAndValidity();
      }
    })
  }

  close(): void {
    this.activeModal.close(this.vacationForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  changeVacationDuration(event): void {
    if (event.target.checked) {
      this.vacationForm.get('toDate').patchValue(null);
      this.vacationForm.get('toDate').setValidators([]);
      this.isSingleDay = true;
      this.vacationForm.get('toDate').updateValueAndValidity();
      this.intervalDays = 1;
    } else {
      this.vacationForm.get('toDate').setValidators([Validators.required]);
      this.isSingleDay = false;
      this.vacationForm.get('toDate').updateValueAndValidity();
    }
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.vacationForm, field);
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.vacationForm, field);
  }

}
