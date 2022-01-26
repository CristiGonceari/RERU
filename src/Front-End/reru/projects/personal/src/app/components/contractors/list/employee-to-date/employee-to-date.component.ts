import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-employee-to-date',
  templateUrl: './employee-to-date.component.html',
  styleUrls: ['./employee-to-date.component.scss']
})
export class EmployeeToDateComponent implements OnInit {
  @Input() placeholder: string;
  @Output() handleTo: EventEmitter<string> = new EventEmitter<string>();
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
      this.handleTo.emit(null);
      return;
    }

    this.isPatternError = false;
    if (!isDateStruct && Date.parse(dateStringFormat)) {
      const time = new Date(dateStringFormat);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.handleTo.emit(time.toISOString());
      return;
    }

    if (isDateStruct) {
      const time = new Date(`${event.year}-${event.month}-${event.day}`);
      time.setHours(new Date().getHours());
      time.setMinutes(new Date().getMinutes());
      time.setSeconds(new Date().getSeconds());
      time.setMilliseconds(new Date().getMilliseconds());
      this.handleTo.emit(time.toISOString());
    }
  }

  private hasMatchEvent(event): boolean {
    return event && event.match && typeof event.match === 'function';
  }
}
