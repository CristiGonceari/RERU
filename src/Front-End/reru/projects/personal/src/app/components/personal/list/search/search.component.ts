import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  key: string;
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();
  @Output() handleRewrite: EventEmitter<string> = new EventEmitter<string>();
  constructor() { }

  search(value: string): void {
    this.key = value.trim();
    this.handleSearch.emit(value.trim());
  }

  clearSearch(value: string): void {
    if (!value) {
      this.handleSearch.emit('');
    }
    this.key = value.trim();
    this.handleRewrite.emit(value.trim());
  }
}
