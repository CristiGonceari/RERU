import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-template-list',
  templateUrl: './test-template-list.component.html',
  styleUrls: ['./test-template-list.component.scss']
})
export class TestTemplateListComponent implements OnInit, AfterViewInit {
  title: string;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }

}
