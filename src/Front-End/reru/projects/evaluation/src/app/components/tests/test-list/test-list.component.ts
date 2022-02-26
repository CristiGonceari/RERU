import { Component } from '@angular/core';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.scss']
})
export class TestListComponent {

  title: string;

  constructor() { }

	getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
