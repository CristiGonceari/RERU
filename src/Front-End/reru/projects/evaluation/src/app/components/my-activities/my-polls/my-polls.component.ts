import { Component, OnInit } from '@angular/core';
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

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.getEvents();
  }

  toggleShow(id): void {
    id == this.eventId ? this.show = !this.show : this.show = true;
    this.eventId = id;
  }

  getEvents(data: any = {}) {
    let params = {
      testTypeMode: 1,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}

    this.eventService.getMyEvents(params).subscribe(res => {
      this.events = res.data.items;
      this.pagedSummary = res.data.pagedSummary;
      this.isLoading = false;
    });
  }

}
