import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';

@Component({
  selector: 'app-event-overview',
  templateUrl: './event-overview.component.html',
  styleUrls: ['./event-overview.component.scss']
})
export class EventOverviewComponent implements OnInit {
  id: number;
  name: string;
  startDate: Date;
  endDate: Date;
  description: string;
  isLoading: boolean = true;

  constructor(
		private service: EventService,
		private activatedRoute: ActivatedRoute,
    public router: Router
  ) {  }
  
  ngOnInit(): void {
   this.subsribeForParams();
  }

  get(){
    this.service.getDetailsEvent(this.id).subscribe(res => {
      if (res && res.data) {
        this.name = res.data.name;
        this.startDate = res.data.fromDate;
        this.endDate = res.data.tillDate;
        this.description = res.data.description;
        this.isLoading = false;
      }
    });
  }
  
  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.id = params.id;
			if (this.id) {
        this.get();
    }});
	}
}
