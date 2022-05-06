import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditUserRoleComponent } from './add-edit-user-role/add-edit-user-role.component';
import { UserRoleDetailsComponent } from './user-role-details/user-role-details.component';
import { UserRoleOverviewComponent } from './user-role-details/user-role-overview/user-role-overview.component';
import { UserRolesComponent } from './user-roles.component';

const routes: Routes = [
  { path: '', component: UserRolesComponent },
  { 
    path: 'user-role-details/:id',
    component: UserRoleDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: UserRoleOverviewComponent }
    ]
  },
  { path: 'add-user-role', component: AddEditUserRoleComponent },
  { path: 'edit-user-role/:id', component: AddEditUserRoleComponent }
]

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class UserRolesRoutingModule { }
