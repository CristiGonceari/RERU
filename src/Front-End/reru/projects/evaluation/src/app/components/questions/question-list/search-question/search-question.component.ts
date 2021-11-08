import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search-question',
  templateUrl: './search-question.component.html',
  styleUrls: ['./search-question.component.scss']
})
export class SearchQuestionComponent{
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
