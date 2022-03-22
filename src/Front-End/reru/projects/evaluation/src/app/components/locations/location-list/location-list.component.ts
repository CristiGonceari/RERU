import { Component } from '@angular/core';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.scss']
})
export class LocationListComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
