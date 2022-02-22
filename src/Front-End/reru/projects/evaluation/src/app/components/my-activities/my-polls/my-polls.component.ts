import { Component, OnInit } from '@angular/core';
import { Events } from '../../../utils/models/calendar/events';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { EventService } from '../../../utils/services/event/event.service';

@Component({
  selector: 'app-my-polls',
  templateUrl: './my-polls.component.html',
  styleUrls: ['./my-polls.component.scss']
})
export class MyPollsComponent implements OnInit {
  events = [];
  isLoading: boolean = true;
  pagedSummary: PaginationModel = new PaginationModel();
  show: boolean = false;

  eventId: number;
  selectedDay;

  fromDate;
  tillDate;

  displayMonth: string;
  displayYear: number;
  countedEvents: Events[] = [];
  displayDate;

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
  }

  toggleShow(id): void {
    id == this.eventId ? this.show = !this.show : this.show = true;
    this.eventId = id;
  }

  getEvents(data: any = {}) {

    this.isLoading = true;
    this.selectedDay = null;
    if (data.fromDate != null && data.tillDate != null) {
      this.fromDate = data.fromDate,
        this.tillDate = data.tillDate
    }

    if (data.displayMonth != null && data.displayYear != null) {
      this.displayMonth = data.displayMonth;
      this.displayYear = data.displayYear;
    }

    let params = {
      testTemplateMode: 1,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
      fromDate: this.parseDates(this.fromDate),
      tillDate: this.parseDates(this.tillDate),
    }

    this.eventService.getMyEvents(params).subscribe(res => {
      this.events = res.data.items;
      this.pagedSummary = res.data.pagedSummary;
      this.isLoading = false;
    });
  }

  parseDates(date) {

    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;;

  }

  parseDatesForTable(date) {

    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${dayWithZero}/${monthWithZero}/${year}`;

  }

  getListByDate(data: any = {}): void {

    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date)
    }

    const request = {
      date: this.selectedDay,
      testTemplateMode: 1,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.eventService.getMyEventsByDate(request).subscribe(response => {
      if (response.success) {
        this.events = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  getListOfCoutedEvents(data) {

    const request = {
      testTemplateMode: 1,
      fromDate: this.parseDates(data.fromDate),
      tillDate: this.parseDates(data.tillDate)
    }

    this.eventService.getMyEventsCount(request).subscribe(response => {

      if (response.success) {

        this.countedEvents = response.data;

        for (let calendar of data.calendar) {

          let data = new Date(calendar.date);

          for (let values of response.data) {

            let c = new Date(values.date);
            let compararea = +data == +c;

            if (compararea) {
              calendar.count = values.count;
            }
          }
        }
      }
    })
  }
}
