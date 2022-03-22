import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.scss']
})
export class UserSearchComponent {
  @Output() handleSearch: EventEmitter<string> = new EventEmitter<string>();
  @Output() searchOnChange: EventEmitter<string> = new EventEmitter<string>();

  searchForm = new FormGroup ({
    name: new FormControl(''),
    idnp: new FormControl('')
  });

  constructor() { }

}
