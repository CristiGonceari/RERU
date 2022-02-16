import { AfterViewInit, Component } from '@angular/core';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements AfterViewInit{
  title: string;

  constructor() { }

  ngAfterViewInit(): void {
    this.title = document.getElementById('title').innerHTML;
  }

}
