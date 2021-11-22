import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from '../../../utils/guards/auth.guard';
import { RoleDetailsComponent } from './role-details.component';
import { RolePermissionsListComponent } from './role-permissions-list/role-permissions-list.component';
import { PermissionRouteGuard, AuthenticationGuard  } from '@erp/shared';


const routes: Routes = [
	{
		path: '',
		component: RoleDetailsComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{ path: '', redirectTo: 'permissions', pathMatch: 'full' },
			{
				path: 'permissions',
				component: RolePermissionsListComponent,
			},
		],
	},
];

@NgModule({

	imports: [RouterModule.forChild(routes),CommonModule],
	exports: [RouterModule],
})
export class RoleDetailsRoutingModule {}

