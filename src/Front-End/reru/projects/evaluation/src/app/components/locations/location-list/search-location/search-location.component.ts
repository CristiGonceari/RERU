import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-search-location',
  templateUrl: './search-location.component.html',
  styleUrls: ['./search-location.component.scss']
})
export class SearchLocationComponent {
  public value: string;
  
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();
  @Output() handleRewrite: EventEmitter<string> = new EventEmitter<string>();

  @Input() useDebounce: boolean = true;
  @Input() showIcon: boolean = true;

  constructor() {}

  search(value: string): void {
    this.value = value.trim();
    this.handleSearch.emit(value.trim());
  }

  clearSearch(value: string): void {
    if (!value) {
      this.handleSearch.emit('');
    }
    this.value = value.trim();
    this.handleRewrite.emit(value.trim());
  }
}
