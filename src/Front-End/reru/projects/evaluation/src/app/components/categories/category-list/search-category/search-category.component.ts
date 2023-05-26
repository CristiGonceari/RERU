import { Component, EventEmitter, Input, Output } from '@angular/core';
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

  @Input() useDebounce: boolean = true;
  @Input() showIcon: boolean = true;

  constructor() {}

  ngOnInit(): void {
    if(this.useDebounce){
      this.searchCategoryUpdate.pipe(
        map((result) => {this.isLoading = true; return result;}),
        debounceTime(400),
        distinctUntilChanged())
        .subscribe(value => {
          this.handleSearch.emit(value);
          this.isLoading = false;
        });
    } else {
      this.searchCategoryUpdate.subscribe(value => {
        this.handleSearch.emit(value);
      });
    }
  }

  clearSearch(): void {
    this.value = '';
    this.searchCategoryUpdate.next('');
    this.handleSearch.emit('');
  }

}
