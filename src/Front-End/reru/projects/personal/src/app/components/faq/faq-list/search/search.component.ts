import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent{
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
