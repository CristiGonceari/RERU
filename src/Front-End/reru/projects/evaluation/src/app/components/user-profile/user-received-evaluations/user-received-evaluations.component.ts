import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-received-evaluations',
  templateUrl: './user-received-evaluations.component.html',
  styleUrls: ['./user-received-evaluations.component.scss']
})
export class UserReceivedEvaluationsComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
