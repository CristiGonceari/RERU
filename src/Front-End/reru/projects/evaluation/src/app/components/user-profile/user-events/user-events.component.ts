import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { EventService } from '../../../utils/services/event/event.service';

@Component({
  selector: 'app-user-events',
  templateUrl: './user-events.component.html',
  styleUrls: ['./user-events.component.scss']
})
export class UserEventsComponent implements OnInit {
  events = [];
  isLoading: boolean = true;
  pagedSummary: PaginationModel = new PaginationModel();
  userId;

  constructor(private eventService: EventService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      if (params.id) {
        this.userId = params.id;
        this.getEvents();
      }
    });
  }

  getEvents(data: any = {}){
    const params = {
      testTemplateMode: 0,
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.eventService.getUserEvents(params).subscribe(res => {
      this.events = res.data.items;
      this.pagedSummary = res.data.pagedSummary;
      this.isLoading = false;
    });
  }
}
