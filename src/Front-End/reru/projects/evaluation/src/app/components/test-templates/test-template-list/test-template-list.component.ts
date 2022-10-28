import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'app-test-template-list',
  templateUrl: './test-template-list.component.html',
  styleUrls: ['./test-template-list.component.scss']
})
export class TestTemplateListComponent {
  title: string;
  @ViewChild('status') searchStatus: any;
  @ViewChild('templateName') templateName: any;
  @ViewChild('eventName') eventName: any;
  @ViewChild('mode') mode: any;
  @ViewChild('qualifyingType') qualifyingType: any;

  constructor() { }

  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  clearFields() {
    this.templateName.key='';
    this.eventName.key='';
		this.searchStatus.getTestStatuses();
		this.mode.getStatuses();
		this.qualifyingType.getStatuses();
	}

}
