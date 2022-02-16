import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.scss']
})
export class EventsListComponent implements OnInit, AfterViewInit {
  title: string;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }

}
