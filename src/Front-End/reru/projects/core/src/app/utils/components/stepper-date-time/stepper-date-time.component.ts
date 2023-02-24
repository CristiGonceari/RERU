import { Component, Input, OnChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidatorUtil } from '../../util/validator.util';

@Component({
  selector: 'app-stepper-date-time',
  templateUrl: './stepper-date-time.component.html',
  styleUrls: ['./stepper-date-time.component.scss']
})
export class StepperDateTimeComponent implements OnChanges {
  @Input() parent: FormGroup;
  @Input() date: string;
  @Input() placeholder: string;
  @Input() isReverse: boolean;
  @Input() isDisabled: boolean;
  @Input() isLarge: boolean;
  @Input() isSmall: boolean;
  @Input() isSolid: boolean = true;
  isPatternError: boolean;

  constructor() { }

  dateModel: any;

  ngOnChanges(): void {
    this.initDate(this.date);
    this.placeholder = this.placeholder || 'ZZ.LL.AAAA';
  }

  initDate(date: string) {
    if (this.parent && this.parent.get(date) && this.parent.get(date).value) {
      const day = new Date(this.parent.get(date).value).getDate();
      const month = new Date(this.parent.get(date).value).getMonth() + 1;
      const year = new Date(this.parent.get(date).value).getFullYear();
      this.dateModel = { day, month, year };
    } else if (this.parent && (!this.parent.get(date) || this.parent.get(date) && !this.parent.get(date).value)) {
      this.dateModel = null;
    }
  }

  get isInvalid(): boolean {
    if(this.isPatternError == false){
      return false;
    }
    return this.parent.invalid &&
          (this.parent.dirty ||
          this.parent.touched);
  }

  handleChange(event: any): void {

    const isDate = this.hasMatchEvent(event) && event.match(new RegExp(/^\d\d\.\d\d\.\d\d\d\d$/));
    const isDateStruct = event && event.year && event.month && event.day;
    const dateStringFormat = isDate && event.split('.').reverse().join('-');

    if (!event || !isDate && !event.year || !isDateStruct && !Date.parse(dateStringFormat)) {
      this.isPatternError = true;
      this.parent.controls[this.date].patchValue(null);
      return;
    }

    this.isPatternError = false;
    if (!isDateStruct && Date.parse(dateStringFormat)) {
      const time = new Date(dateStringFormat);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.parent.controls[this.date].patchValue(time.toISOString());
      return;
    }

    if (isDateStruct) {
      const time = new Date(`${event.year}-${event.month}-${event.day}`);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.parent.controls[this.date].patchValue(time.toISOString());
    }
  }

  private hasMatchEvent(event): boolean {
    return event && event.match && typeof event.match === 'function';
  }

  inputValidator(form, field) {
    return !ValidatorUtil.isInvalidPattern(form, field) && form.get(field).valid;
  }
}
