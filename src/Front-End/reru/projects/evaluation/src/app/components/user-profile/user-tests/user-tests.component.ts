import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-tests',
  templateUrl: './user-tests.component.html',
  styleUrls: ['./user-tests.component.scss']
})
export class UserTestsComponent{

  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
