import { Component, ViewChild } from '@angular/core';
import { FilterUserStateComponent } from './filter-user-state/filter-user-state.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
 // @ViewChild(UserSearchComponent) userSearch: UserSearchComponent;
 @ViewChild('keyword') searchKeyword: any;
 @ViewChild('email') searchEmail: any;
 @ViewChild('idnp') searchIdnp: any;
 @ViewChild(FilterUserStateComponent) userState: FilterUserStateComponent;

 title: string;
  constructor() { }

  resetFilters(): void {
    this.searchKeyword.clear();
		this.searchEmail.clear();
		this.searchIdnp.clear();
    this.userState.userState = '0';
  }
  
  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
