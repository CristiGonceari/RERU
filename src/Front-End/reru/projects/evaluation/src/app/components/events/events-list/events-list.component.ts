import { Component } from '@angular/core';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.scss']
})
export class EventsListComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
