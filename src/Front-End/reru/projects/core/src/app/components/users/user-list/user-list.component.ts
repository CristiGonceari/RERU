import { Component, ViewChild } from '@angular/core';
import { FilterUserStateComponent } from './filter-user-state/filter-user-state.component';
import { SearchStatusComponent } from './search-status/search-status.component';

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
 @ViewChild(SearchStatusComponent) userStatusEnum: SearchStatusComponent;

 title: string;
  constructor() { }

  resetFilters(): void {
    this.searchKeyword.clear();
		this.searchEmail.clear();
		this.searchIdnp.clear();
    this.userState.status = '0';
    this.userStatusEnum.userStatus = '';
  }
  
  getTitle(): string {
		this.title = document.getElementById('title').innerHTML;
		return this.title
	}

}
