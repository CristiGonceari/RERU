import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-default-stepper-date-time',
  templateUrl: './default-stepper-date-time.component.html',
  styleUrls: ['./default-stepper-date-time.component.scss']
})
export class DefaultStepperDateTimeComponent implements OnInit {

  @Input() parent: FormGroup;
  @Input() date: string;
  @Input() placeholder: string;
  @Input() isReverse: boolean;
  @Input() isDisabled: boolean;
  isPatternError: boolean;
  
  constructor() { }
  
  dateModel: any;

  ngOnInit(): void {
    this.initDate(this.date);
    this.placeholder = this.placeholder || 'ZZ.LL.AAAA';
  }

  initDate(date: string) {
    if (this.parent && this.parent.get(date) && this.parent.get(date).value) {
      const day = new Date(this.parent.get(date).value).getDate();
      const month = new Date(this.parent.get(date).value).getMonth() + 1;
      const year = new Date(this.parent.get(date).value).getFullYear();
      this.dateModel = { year, month, day };
    } else if (this.parent && (!this.parent.get(date) || this.parent.get(date) && !this.parent.get(date).value)) {
      this.dateModel = null;
    }
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

}
