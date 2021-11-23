import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-sort-button',
  templateUrl: './sort-button.component.html',
  styleUrls: ['./sort-button.component.scss']
})
export class SortButtonComponent implements OnInit {
  @Input() desc: boolean;
  
  constructor() { }

  ngOnInit(): void { }
  
}
