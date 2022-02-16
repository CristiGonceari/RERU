import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-type-list',
  templateUrl: './test-type-list.component.html',
  styleUrls: ['./test-type-list.component.scss']
})
export class TestTypeListComponent implements OnInit, AfterViewInit {
  title: string;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }

}
