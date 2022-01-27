import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search-by-contractors',
  templateUrl: './search-by-contractors.component.html',
  styleUrls: ['./search-by-contractors.component.scss']
})
export class SearchByContractorsComponent implements OnInit {
  key: string;
  
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();
  @Output() handleRewrite: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

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
