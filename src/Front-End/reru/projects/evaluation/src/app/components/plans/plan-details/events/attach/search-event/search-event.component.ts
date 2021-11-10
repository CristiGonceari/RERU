import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';

@Component({
  selector: 'app-search-event',
  templateUrl: './search-event.component.html',
  styleUrls: ['./search-event.component.scss']
})
export class SearchEventComponent implements OnInit {
  list = [];
  form = new FormControl();
  planId;
  eventId;

  constructor(private planService: PlanService,
    private eventService: EventService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.getList();
  }

  initData() {
    this.planId = this.activatedRoute.snapshot.paramMap.get('id');
    this.eventService.getEvents({}).subscribe(() => {
      this.form.setValue(null)
    })
  }

  getList() {
   this.initData();

    this.form.valueChanges.subscribe(term => {
      if (!term)
        this.eventService.eventsWihoutPlan({ planId: this.planId }).subscribe(data => this.list = data.data);
      else if (term != '')
        this.eventService.eventsWihoutPlan({ keyword: term, planId: this.planId }).subscribe(data => this.list = data.data);
      
      this.planService.setEvent(term);
    });
  }

  getTitle(eventId) {
    if (eventId)
      return this.list.find(u => u.id === eventId).name;
  }
}
