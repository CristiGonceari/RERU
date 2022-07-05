import { Component } from '@angular/core';

@Component({
  selector: 'app-user-evaluations',
  templateUrl: './user-evaluations.component.html',
  styleUrls: ['./user-evaluations.component.scss']
})
export class UserEvaluationsComponent {
  title: string;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}
}
