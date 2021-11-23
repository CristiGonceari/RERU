import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.scss']
})
export class UserSearchComponent {
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();
  @Output() searchOnEnter: EventEmitter<string> = new EventEmitter<string>();
  constructor() { }

}
