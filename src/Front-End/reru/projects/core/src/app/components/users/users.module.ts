import { UsersRoutingModule } from './users-routing.module';
import { EditComponent } from './edit/edit.component';
import { SetPasswordComponent } from './set-password/set-password.component';
import { AddComponent } from './add/add.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserListComponent } from '././user-list/user-list.component';
import { UserListTableComponent } from '././user-list-table/user-list-table.component';
import { UsersComponent } from './users.component';
import { EditUserComponent } from '././user-profile/edit-user/edit-user.component';
import { SetPasswordUserComponent } from '././user-profile/set-password-user/set-password-user.component';
import { SortButtonComponent } from '././user-list-table/sort-button/sort-button.component';
import { FilterUserStateComponent } from '././user-list/filter-user-state/filter-user-state.component';
import { RemoveComponent } from './remove/remove.component';
import { SearchStatusComponent } from './user-list/search-status/search-status.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UsersRoutingModule,
    NgbModule
  ],
  declarations: [
    UserListComponent,
    UserListTableComponent,
    UsersComponent,
    AddComponent,
    EditComponent,
    SetPasswordComponent,
    EditUserComponent,
    SetPasswordUserComponent,
    SortButtonComponent,
    FilterUserStateComponent,
    RemoveComponent,
    SearchStatusComponent
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class UsersModule { }
