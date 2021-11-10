import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  id;

	constructor(private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.id = params.id;
    });
	}
}
