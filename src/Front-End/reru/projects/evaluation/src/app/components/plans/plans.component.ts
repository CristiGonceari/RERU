import { Component } from '@angular/core';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.scss']
})
export class PlansComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
