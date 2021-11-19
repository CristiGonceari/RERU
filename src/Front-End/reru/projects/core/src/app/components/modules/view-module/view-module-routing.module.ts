import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ModuleOverviewComponent } from './module-overview/module-overview.component';
import { ViewModuleComponent } from '../view-module/view-module.component';
import { ModuleRolesComponent } from './module-roles/module-roles.component'
import { ModulePermissionsComponent } from './module-permissions/module-permissions.component';
import { PermissionRouteGuard, AuthenticationGuard } from '@erp/shared';

const routes: Routes = [
	{
		path: '',
		component: ViewModuleComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{
				path: 'overview',
				component: ModuleOverviewComponent,
				data: { permission: 'P00000004' },
				canActivate: [PermissionRouteGuard]
			},
			{ path: 'roles', component: ModuleRolesComponent },
			{ path: 'permissions', component: ModulePermissionsComponent }
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ViewModuleRoutingModule {}
