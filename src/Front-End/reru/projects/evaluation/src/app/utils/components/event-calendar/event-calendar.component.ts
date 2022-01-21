import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CalendarDay } from '../../models/calendar/calendarDay';
import { Events } from '../../models/calendar/events';
import { PlanService } from '../../services/plan/plan.service';

@Component({
  selector: 'app-event-calendar',
  templateUrl: './event-calendar.component.html',
  styleUrls: ['./event-calendar.component.scss']
})
export class EventCalendarComponent implements OnInit {

  @Output() onDatePicked = new EventEmitter<any>();
  @Output() listOfDates = new EventEmitter<any>();
  @Output() listOfCountedEvents = new EventEmitter<any>();

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

  url

  public displayMonth: string;
  public displayYear: number;
  private monthIndex: number = 0;

  onChangeMonth;
  countedPlans: Events[] = [];
  onClickedDay;
  
  currentMonth: boolean = false;
  isLoading: boolean = true;

  firstDayOfMonth;
  lastDayOfMonth;
  todayDay;
  dates: boolean = false;

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
    this.generateCalendarDays(this.monthIndex);
  }

  private generateCalendarDays(monthIndex: number): void {

    this.calendar = [];

    let day: Date = new Date(new Date().setMonth(new Date().getMonth() + monthIndex));
    this.onChangeMonth = day.getMonth()

    let data = {
      fromDate: new Date(day.getFullYear(), day.getMonth(), 1),
      tillDate: new Date(day.getFullYear(), day.getMonth() + 1, 0)
    }

    this.displayMonth = this.monthNames[day.getMonth()];
    this.displayYear = day.getFullYear();

    let startingDateOfCalendar = this.getStartDateForCalendar(day);

    let dateToAdd = startingDateOfCalendar;

    for (var i = 0; i < 42; i++) {
      this.calendar.push(new CalendarDay(new Date(dateToAdd)));
      dateToAdd = new Date(dateToAdd.setDate(dateToAdd.getDate() + 1));
    }

    for(let calendar of this.calendar){
      if(calendar.isToday){
        this.todayDay = calendar
      }
    }

    if(this.todayDay != null){
      this.getClickedDay(this.todayDay)
    }
    else{
        this.listOfDates.emit(data)
    }

    this.getListOfCoutedEvents(data);
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

  getClickedDay(data) {
     
    this.calendar.map(x => x.clickedDay = false);

    data.clickedDay = true;

    const dates = {
      date: data.date,
      clickedDay: data.clickedDay
    }
    
    this.onDatePicked.emit(dates)
  }

  getListOfCoutedEvents(data): void {
    const request = {
      fromDate: data.fromDate,
      tillDate: data.tillDate,
      calendar: this.calendar
    }

    this.listOfCountedEvents.emit(request)
  }

}
