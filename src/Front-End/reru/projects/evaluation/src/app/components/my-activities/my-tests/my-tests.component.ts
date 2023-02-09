import { Component } from '@angular/core';

@Component({
  selector: 'app-my-tests',
  templateUrl: './my-tests.component.html',
  styleUrls: ['../table-inherited.component.scss']
})
export class MyTestsComponent {
  getTitle(): string {
    return document.getElementById('title').innerHTML;
  }
}
