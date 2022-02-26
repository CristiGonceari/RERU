import { Component } from '@angular/core';

@Component({
  selector: 'app-test-template-list',
  templateUrl: './test-template-list.component.html',
  styleUrls: ['./test-template-list.component.scss']
})
export class TestTemplateListComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
