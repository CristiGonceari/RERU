import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-employee-from-date',
  templateUrl: './employee-from-date.component.html',
  styleUrls: ['./employee-from-date.component.scss']
})
export class EmployeeFromDateComponent implements OnInit {
  @Input() placeholder: string;
  @Output() handleFrom: EventEmitter<string> = new EventEmitter<string>();
  dateModel: any;
  isPatternError: boolean;
  constructor() { }

  ngOnInit(): void {
    this.placeholder = this.placeholder || 'ZZ.LL.AAAA';
  }

  handleChange(event): void {
    const isDate = this.hasMatchEvent(event) && event.match(new RegExp(/^\d\d\.\d\d\.\d\d\d\d$/));
    const isDateStruct = event && event.year && event.month && event.day;
    const dateStringFormat = isDate && event.split('.').reverse().join('-');
    if (!event || !isDate && !event.year || !isDateStruct && !Date.parse(dateStringFormat)) {
      this.isPatternError = true;
      this.handleFrom.emit(null);
      return;
    }

    this.isPatternError = false;
    if (!isDateStruct && Date.parse(dateStringFormat)) {
      const time = new Date(dateStringFormat);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.handleFrom.emit(time.toISOString());
      return;
    }

    if (isDateStruct) {
      const time = new Date(`${event.year}-${event.month}-${event.day}`);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.handleFrom.emit(time.toISOString());
    }
  }

  private hasMatchEvent(event): boolean {
    return event && event.match && typeof event.match === 'function';
  }
}
