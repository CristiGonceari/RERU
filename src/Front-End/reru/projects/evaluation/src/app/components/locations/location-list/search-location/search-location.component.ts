import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-location',
  templateUrl: './search-location.component.html',
  styleUrls: ['./search-location.component.scss']
})
export class SearchLocationComponent {

  key: string;
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  search(value: string): void {
    this.handleSearch.emit(value);
  }

  clearSearch(): void {
    this.key = '';
    this.handleSearch.emit('');
  }

}
