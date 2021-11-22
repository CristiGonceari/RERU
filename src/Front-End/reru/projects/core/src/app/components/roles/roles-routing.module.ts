import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from '../../utils/guards/auth.guard';
import { PermissionRouteGuard, AuthenticationGuard } from '@erp/shared';
import { RoleDetailsRoutingModule } from './role-details/role-details-routing.module';
import { RolesComponent } from './roles.component';
import { UpdateRolePermissionsComponent } from './role-details/update-role-permissions/update-role-permissions.component';
import { AddEditRoleComponent } from './add-edit-role/add-edit-role.component';

const routes: Routes = [
	{
		path: '',
		component: RolesComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{
				path: ':id/update-permissions',
				component: UpdateRolePermissionsComponent,
			},
			{
				path: 'edit/:id',
				component: AddEditRoleComponent,
			},
			{
				path: ':id',
				loadChildren: () => import('./role-details/role-details.module').then(m => m.RoleDetailsModule),
			},
		],
	},
];

@NgModule({
	imports: [RoleDetailsRoutingModule, RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RolesRoutingModule {}
