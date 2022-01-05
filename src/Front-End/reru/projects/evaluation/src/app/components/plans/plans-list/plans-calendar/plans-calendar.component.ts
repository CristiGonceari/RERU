import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { CalendarDay } from '../../../../utils/models/calendar/calendarDay';


@Component({
  selector: 'app-plans-calendar',
  templateUrl: './plans-calendar.component.html',
  styleUrls: ['./plans-calendar.component.scss']
})
export class PlansCalendarComponent implements OnInit {

@Output() onDatePicked = new EventEmitter<any>();
@Output() listOfDates = new EventEmitter<any>();
  
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

 onChangeMonth;
 clickedDay;
 countedPlans:Events [] = [];

 currentMonth: boolean = false;

 firstDayOfMonth;
 lastDayOfMonth;

  constructor(public dialog: MatDialog,
              private planService: PlanService,) {}

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
    
    this.listOfDates.emit(data)
    
    this.getListOfCoutedPlans(data);
    
    this.displayMonth = this.monthNames[day.getMonth()];
    this.displayYear = day.getFullYear();

    let startingDateOfCalendar = this.getStartDateForCalendar(day);

    let dateToAdd = startingDateOfCalendar;

    for (var i = 0; i < 42; i++) {
      this.calendar.push(new CalendarDay(new Date(dateToAdd)));
      dateToAdd = new Date(dateToAdd.setDate(dateToAdd.getDate() + 1));
    }
  }

  private getStartDateForCalendar(selectedDate: Date){

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
    this.generateCalendarDays(this.monthIndex);
  }

  public decreaseMonth() {
    this.monthIndex--
    this.generateCalendarDays(this.monthIndex);
  }

  public setCurrentMonth() {
    this.monthIndex = 0;
    this.generateCalendarDays(this.monthIndex);
  }

  getClickedDay(date){
      
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

     this.clickedDay = `${year}-${monthWithZero}-${dayWithZero}`;

     const data = {
      clickedDay: this.clickedDay
     }

    this.onDatePicked.emit(data)
  }

  getListOfCoutedPlans(data): void
  {
    const request = {
      fromDate: this.parseDates(data.fromDate),
      tillDate: this.parseDates(data.tillDate),
    }

    this.planService.getPlanCount(request).subscribe(response => {
      if(response.success)
      {
        this.countedPlans = response.data;
        
        for(let calendar of this.calendar){

            let data =  new Date(calendar.date);
            
            for(let values of response.data)
            {
             
              let c = new Date(values.date);
              let compararea = +data == +c;

              if (compararea){
                calendar.count = values.count;
              }
            }
        }
      }
    })
  }
  
  parseDates(date){
      
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;
    
  }

}
