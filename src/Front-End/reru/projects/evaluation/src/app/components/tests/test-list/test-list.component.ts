import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.scss']
})
export class TestListComponent {

  title: string;
  @ViewChild('testName') testName: any;
  @ViewChild('testEvent') testEvent: any;
  @ViewChild('testLocation') testLocation: any;
  @ViewChild('userName') userName: any;
  @ViewChild('userEmail') userEmail: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('selectedStatus') selectedStatus: any;
  @ViewChild('selectedResult') selectedResult: any;

  constructor() { }

	getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

  clearFields() {
		this.testName.key = '';
		this.testEvent.key = '';
		this.testLocation.key = '';
    this.userName.key = '';
    this.userEmail.key = '';
    this.idnp.key = '';
    this.selectedStatus.getTestStatuses();
    this.selectedResult.getTestResults();
  }
}
