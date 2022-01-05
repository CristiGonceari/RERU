import {Events} from './events'

export class CalendarDay {
    public date: Date;
    public count: number;
    public title: string;
    public isPastDate: boolean;
    public isToday: boolean;
    public month: number;
  
    constructor(d: Date) {
      this.date = d;
      this.isPastDate = d.setHours(0, 0, 0, 0) < new Date().setHours(0, 0, 0, 0);
      this.isToday = d.setHours(0, 0, 0, 0) == new Date().setHours(0, 0, 0, 0);
      this.month = d.getMonth();
      this.count = 0;
    }
  
  }