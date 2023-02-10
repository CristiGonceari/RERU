import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'erp-shared-search-input',
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss']
})
export class SearchInputComponent {
  public searchBy = new Subject<string>();
  public isLoading: boolean;
  public value: string = '';

  @Input() placeholder: string;
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
    this.searchBy.pipe(
      map((result) => {this.isLoading = true; return result;}),
      debounceTime(400),
      distinctUntilChanged())
      .subscribe(value => {
        this.handleSearch.emit(value);
        this.isLoading = false;
      });
   }


   clearSearch(): void {
    this.value = '';
    this.searchBy.next('');
    this.handleSearch.emit('');
    this.isLoading = false;
  }

}
