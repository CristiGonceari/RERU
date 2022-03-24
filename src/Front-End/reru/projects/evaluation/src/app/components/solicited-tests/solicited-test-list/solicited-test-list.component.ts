import { Component, ViewChild } from '@angular/core';

@Component({
	selector: 'app-solicited-test-list',
	templateUrl: './solicited-test-list.component.html',
	styleUrls: ['./solicited-test-list.component.scss']
})
export class SolicitedTestListComponent {
	@ViewChild('user') searchUser: any;
	@ViewChild('test') searchTest: any;
	@ViewChild('event') searchEvent: any;

	title: string;
	constructor() { }

	getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

	clearFields() {
		this.searchEvent.clear();
		this.searchUser.clear();
		this.searchTest.clear();
	}
}
