import { Component } from '@angular/core';

@Component({
  selector: 'app-solicited-test-list',
  templateUrl: './solicited-test-list.component.html',
  styleUrls: ['./solicited-test-list.component.scss']
})
export class SolicitedTestListComponent {
  title: string;
  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
