import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search-category',
  templateUrl: './search-category.component.html',
  styleUrls: ['./search-category.component.scss']
})
export class SearchCategoryComponent{

  key: string;
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  search(value: string): void {
    this.handleSearch.emit(value);
  }

  clearSearch(value: string): void {
    if (!value) {
      this.handleSearch.emit('');
    }
  }

}
