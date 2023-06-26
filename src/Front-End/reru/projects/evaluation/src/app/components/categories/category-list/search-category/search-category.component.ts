import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-search-category',
  templateUrl: './search-category.component.html',
  styleUrls: ['./search-category.component.scss']
})
export class SearchCategoryComponent {
  public isLoading: boolean;
  public value: string;
  public searchCategoryUpdate = new Subject<string>();
  
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
