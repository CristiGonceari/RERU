import { Component } from '@angular/core';

@Component({
  selector: 'app-my-evaluated-tests',
  templateUrl: './my-evaluated-tests.component.html',
  styleUrls: ['./my-evaluated-tests.component.scss']
})
export class MyEvaluatedTestsComponent {  
  constructor(){}
  
  getTitle(): string {
   return document.getElementById('title').innerHTML;
  }
}
