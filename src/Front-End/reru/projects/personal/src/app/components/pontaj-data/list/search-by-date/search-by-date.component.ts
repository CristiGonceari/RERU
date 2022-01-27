import { Component, Input, OnInit } from '@angular/core';
import { NgbDatepickerNavigateEvent } from '@ng-bootstrap/ng-bootstrap';
import { endOfMonth, startOfMonth } from 'date-fns';
import { TimesheetdatesService } from 'projects/personal/src/app/utils/services/timesheetdates.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-search-by-date',
  templateUrl: './search-by-date.component.html',
  styleUrls: ['./search-by-date.component.scss']
})
export class SearchByDateComponent implements OnInit {

  selectedMonth: number;
  selectedYear: number;

  firstDayInMonth: any;
  lastDayInMonth: any;

  constructor(private timesheetdatesService: TimesheetdatesService) { }

  ngOnInit(): void {
  }
 
  dateNavigate($event: NgbDatepickerNavigateEvent) {
    this.selectedMonth = $event.next.month;
    this.selectedYear = $event.next.year;
  }

  sendData(): void {
    const firstDay = `${this.selectedYear}-${this.parseMonth(this.selectedMonth)}-01T00:00:00.000Z`;
    const lastDayDate = endOfMonth(new Date(`${this.selectedYear}-${this.selectedMonth}-10`));
    const lastDay = `${lastDayDate.getFullYear()}-${this.parseMonth(this.selectedMonth)}-${lastDayDate.getDate()}T00:00:00.000Z`;
    this.timesheetdatesService.tableMounth.next(this.selectedMonth);
    this.timesheetdatesService.tableYear.next(this.selectedYear);
    this.timesheetdatesService.firstDayOfMonth.next(firstDay);
    this.timesheetdatesService.lastDayOfMonth.next(lastDay);
  }

  private parseMonth(month): string {
    return month >= 10 ? month : `0${month}`;
  }

 

}
