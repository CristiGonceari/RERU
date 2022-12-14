import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CalendarDay } from '../../models/calendar/calendarDay';
import { Events } from '../../models/calendar/events';

@Component({
  selector: 'app-event-calendar',
  templateUrl: './event-calendar.component.html',
  styleUrls: ['./event-calendar.component.scss']
})
export class EventCalendarComponent implements OnInit {

  @Output() onDatePicked = new EventEmitter<any>();
  @Output() listOfDates = new EventEmitter<any>();
  @Output() listOfCountedEvents = new EventEmitter<any>();
  @Input() isLoading: boolean;

  public calendar: CalendarDay[] = [];

  public monthNames = [
    "january",
    "february",
    "march",
    "april",
    "may",
    "june",
    "july",
    "august",
    "september",
    "october",
    "november",
    "december"
  ];

  public displayMonth: string;
  public displayYear: number;
  private monthIndex: number = 0;
  private calendarTotalCells: number = 42;

  onChangeMonth;
  countedPlans: Events[] = [];
  onClickedDay;
  dayValue: Date;

  currentMonth: boolean = false;

  firstDayOfMonth;
  lastDayOfMonth;
  todayDay: CalendarDay;
  dates: boolean = false;
  cellsForDelete = []

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
    this.generateCalendarDays(this.monthIndex);
  }

  private generateCalendarDays(monthIndex: number): void {

    this.calendar = [];
    this.cellsForDelete = [];

    if (monthIndex != 0) {
      this.dayValue = new Date(new Date().setDate(1));
    }
    else {
      this.dayValue = new Date()
    }

    const day: Date = new Date(this.dayValue.setMonth(new Date().getMonth() + monthIndex));

    this.onChangeMonth = day.getMonth()

    let data = {
      fromDate: new Date(day.getFullYear(), day.getMonth(), 1),
      tillDate: new Date(day.getFullYear(), day.getMonth() + 1, 0)
    }

    this.displayMonth = this.monthNames[day.getMonth()];
    this.displayYear = day.getFullYear();

    let dateToAdd = this.getStartDateForCalendar(day);

    for (let i = 0; i < this.calendarTotalCells; i++) {
      this.calendar.push(new CalendarDay(new Date(dateToAdd)));
      dateToAdd = new Date(dateToAdd.setDate(dateToAdd.getDate() + 1));
    }
    
    this.deleteCells();

    for (let calendar of this.calendar) {
      if (calendar.isToday && calendar.month == day.getMonth() + 1) {
        this.todayDay = calendar;
      }
    }

    if (this.todayDay != null) {
      this.getClickedDay(this.todayDay)
    }
    else {
      this.listOfDates.emit(data)
    }

    this.getListOfCoutedEvents(data);
  }

  private deleteCells() {
    this.cellsForDelete.push(
      {
        week: 'firstWeek',
        forDelete: this.calendar.slice(0, 7).every(el =>
          new Date(el.date.getFullYear(), el.month) < new Date(this.dayValue.getFullYear(), this.dayValue.getMonth()))
      },
      {
        week: 'lastWeek',
        forDelete: this.calendar.slice(this.calendarTotalCells - 7).every(el =>
          new Date(el.date.getFullYear(), el.month) > new Date(this.dayValue.getFullYear(), this.dayValue.getMonth()))
      },
    );

    for (let cell of this.cellsForDelete) {
      if (cell.week == 'firstWeek' && cell.forDelete == true) {
        this.calendar = this.calendar.slice(7, 42);
      }
      if (cell.week == 'lastWeek' && cell.forDelete == true) {
        this.calendar = this.calendar.slice(0, -7);
      }
    };
  }

  private getStartDateForCalendar(selectedDate: Date) {

    let lastDayOfPreviousMonth = new Date(selectedDate.setDate(0));

    let startingDateOfCalendar: Date = lastDayOfPreviousMonth;

    if (startingDateOfCalendar.getDay() != 1) {
      do {
        startingDateOfCalendar = new Date(startingDateOfCalendar.setDate(startingDateOfCalendar.getDate() - 1));
      } while (startingDateOfCalendar.getDay() != 1);
    }

    return startingDateOfCalendar;
  }

  public increaseMonth() {
    this.monthIndex++;
    this.todayDay = null;
    this.generateCalendarDays(this.monthIndex);
    
  }

  public decreaseMonth() {
    this.todayDay = null;
    this.monthIndex--
    this.generateCalendarDays(this.monthIndex);
  }

  public setCurrentMonth() {
    this.monthIndex = 0;
    this.generateCalendarDays(this.monthIndex);
  }

  getClickedDay(data: CalendarDay) {
    this.calendar.map(x => x.clickedDay = false);

    data.clickedDay = true;

    const dates = {
      date: data.date,
      clickedDay: data.clickedDay,
      page: 1
    }

    this.onDatePicked.emit(dates)
  }

  getListOfCoutedEvents(data) {
    const request = {
      fromDate: data.fromDate,
      tillDate: data.tillDate,
      calendar: this.calendar
    }

    this.listOfCountedEvents.emit(request);
  }

}
