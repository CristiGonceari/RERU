import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-test-list',
  templateUrl: './add-test-list.component.html',
  styleUrls: ['./add-test-list.component.scss']
})
export class AddTestListComponent implements OnInit {

  testEvent = false;
  event = false;
  isTestEvent = true;
  
  constructor() { }

  ngOnInit(): void {
  }

}
