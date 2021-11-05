import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimesheetdatesService {
  
  private deparmentId: BehaviorSubject<any> = new BehaviorSubject(null);
  department = this.deparmentId.asObservable();
  
  tableMounth: Subject<number>;
  tableYear: Subject<number>;
  firstDayOfMonth: Subject<string>;
  lastDayOfMonth: Subject<string>;
  nextMonth:  Subject<number>;
  previousMonth:  Subject<number>;
  
  constructor() { 
    this.tableMounth = new Subject<any>()
    this.tableYear = new Subject<any>()
    this.firstDayOfMonth = new Subject<any>()
    this.lastDayOfMonth = new Subject<any>()
    this.nextMonth = new Subject<any>()
    this.previousMonth = new Subject<any>()
  }

  setData(value: any){
      this.deparmentId.next(value);
  }
}