import { Component, ViewChild } from '@angular/core';
import { FilterUserStateComponent } from './filter-user-state/filter-user-state.component';
import { UserSearchComponent } from './user-search/user-search.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
  @ViewChild(UserSearchComponent) userSearch: UserSearchComponent;
  @ViewChild(FilterUserStateComponent) userState: FilterUserStateComponent;

  constructor() { }

  resetFilters(): void {
    this.userSearch.searchForm.reset();
    this.userState.userState = '0';
  }
}
