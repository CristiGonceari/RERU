import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'erp-shared-search-input',
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss']
})
export class SearchInputComponent {
  key: string = '';
  @Input() placeholder: string;
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  search(value: string): void {
    this.key = value;
    this.handleSearch.emit(value);
  }

  clear(): void {
    this.key = '';
    this.search('');
  }

}
