import { Component, OnInit } from '@angular/core';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { EventService } from '../../../utils/services/event/event.service';

@Component({
  selector: 'app-my-events',
  templateUrl: './my-events.component.html',
  styleUrls: ['./my-events.component.scss']
})
export class MyEventsComponent implements OnInit {
  events = [];
  isLoading: boolean = true;
  pagedSummary: PaginationModel = new PaginationModel();

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents(data: any = {}){
    let params = {
      testTypeMode: 0,
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
