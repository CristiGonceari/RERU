import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-user-evaluated-tests',
  templateUrl: './user-evaluated-tests.component.html',
  styleUrls: ['./user-evaluated-tests.component.scss']
})
export class UserEvaluatedTestsComponent {

	title: string;

	constructor() { }
  
  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
