import { Component } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent { 
	title: string;
  mobileButtonLength: string = "100%";

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
