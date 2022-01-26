import { renderFlagCheckIfStmt } from '@angular/compiler/src/render3/view/template';
import { Component, OnInit } from '@angular/core';
import { NgbDatepickerNavigateEvent } from '@ng-bootstrap/ng-bootstrap';
import { endOfMonth } from 'date-fns';
import { ContractorTimesheetDataModel } from '../../../utils/models/contractor-timesheet-data.model';
import { ProfileTimeSheetTableModel, TimesheetDataModel } from '../../../utils/models/timesheet-data.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';

@Component({
  selector: 'app-time-sheet-table',
  templateUrl: './time-sheet-table.component.html',
  styleUrls: ['./time-sheet-table.component.scss']
})
export class TimeSheetTableComponent implements OnInit {

  selectedMonth: number;
  selectedYear: number;

  firstDay: any;
  lastDay: any;

  contractorTimesheetTable: ProfileTimeSheetTableModel[];

  workedHours: any;
  freeHours: any;

  daylist: any;
  isLoading: boolean = true;


  constructor(private contractorProfileService: ContractorProfileService) { }

  ngOnInit(): void {

  }

  dateNavigate($event: NgbDatepickerNavigateEvent) {
    this.isLoading = true;
    this.selectedMonth = $event.next.month;
    this.selectedYear = $event.next.year;
    const firstDay = `${this.selectedYear}-${this.parseMonth(this.selectedMonth)}-01T00:00:00.000Z`;
    const lastDayDate = endOfMonth(new Date(`${this.selectedYear}-${this.selectedMonth}-10`));
    const lastDay = `${lastDayDate.getFullYear()}-${this.parseMonth(this.selectedMonth)}-${lastDayDate.getDate()}T00:00:00.000Z`;
    this.firstDay = firstDay;
    this.lastDay = lastDay;
    this.calculateDays();
    this.retrieveTimeSheetValues();

  }

  calculateDays(): void {
    this.daylist = this.getArrayOfDays(new Date(this.firstDay), new Date(this.lastDay));
  }

  getArrayOfDays(fromDate, toDate) {
    let dayArray = [];
    for(let currentDay = fromDate.getDate(); currentDay <= toDate.getDate(); currentDay++){
      dayArray.push(currentDay);
    }
    return dayArray;
  }


  private parseMonth(month): string {
    return month >= 10 ? month : `0${month}`;
  }

  retrieveTimeSheetValues(data: any = []): void {
    const request = {
      FromDate: this.firstDay,
      ToDate: this.lastDay
    }
    this.contractorProfileService.getProfileTimeSheetTable(request).subscribe((response) => {
      this.contractorTimesheetTable = response.data.content;
      this.workedHours = response.data.workedHours;
      this.freeHours = response.data.freeHours;
      this.isLoading = false;
    });
  }


}
