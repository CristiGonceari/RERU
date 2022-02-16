import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-faq-list',
  templateUrl: './faq-list.component.html',
  styleUrls: ['./faq-list.component.scss']
})
export class FaqListComponent implements OnInit, AfterViewInit {

  title: string;

  constructor( ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }
}
