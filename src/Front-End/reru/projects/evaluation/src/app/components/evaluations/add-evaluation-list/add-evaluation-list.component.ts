import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-evaluation-list',
  templateUrl: './add-evaluation-list.component.html',
  styleUrls: ['./add-evaluation-list.component.scss']
})
export class AddEvaluationListComponent implements OnInit {
  testEvent = false;
  event = false;
  
  constructor() { }

  ngOnInit(): void {
  }
}
