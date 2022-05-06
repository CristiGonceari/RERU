import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRolesComponent } from './user-roles.component';
import { UserRolesRoutingModule } from './user-roles-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { UserRolesTableComponent } from './user-roles-table/user-roles-table.component';
import { AddEditUserRoleComponent } from './add-edit-user-role/add-edit-user-role.component';
import { UserRoleDetailsComponent } from './user-role-details/user-role-details.component';
import { SearchComponent } from './user-roles-table/search/search.component';
import { UserRoleOverviewComponent } from './user-role-details/user-role-overview/user-role-overview.component';

@NgModule({
  declarations: [
    UserRolesComponent,
    UserRolesTableComponent,
    AddEditUserRoleComponent,
    UserRoleDetailsComponent,
    SearchComponent,
    UserRoleOverviewComponent
  ],
  imports: [
    CommonModule,
    UserRolesRoutingModule,
    ReactiveFormsModule,
		FormsModule,
		NgbModule,
		TranslateModule,
		SharedModule,
		UtilsModule
  ]
})
export class UserRolesModule { }
