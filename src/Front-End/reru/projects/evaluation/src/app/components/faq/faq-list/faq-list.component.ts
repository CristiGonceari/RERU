import { Component } from '@angular/core';

@Component({
  selector: 'app-faq-list',
  templateUrl: './faq-list.component.html',
  styleUrls: ['./faq-list.component.scss']
})
export class FaqListComponent {

  title: string;

  constructor( ) { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
