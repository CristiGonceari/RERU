import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from '../../utils/guards/auth.guard';
import { AddEditModuleComponent } from './add-edit-module/add-edit-module.component';
import { RemoveModuleComponent } from './remove-module/remove-module.component';
import { ListModuleComponent } from './list-module/list-module.component';
import { ModulesComponent } from './modules.component';
import { PermissionRouteGuard, AuthenticationGuard } from '@erp/shared';
import { AddEditRoleComponent } from '../roles/add-edit-role/add-edit-role.component';

const routes: Routes = [
	{
		path: '',
		component: ModulesComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{
				path: 'list',
				component: ListModuleComponent,
			},
			{
				path: 'new',
				component: AddEditModuleComponent,
			},
			{
				path: 'edit/:id',
				component: AddEditModuleComponent,
			},
			{
				path: ':id/add-role',
				component: AddEditRoleComponent,
			},
			{
				path: 'remove/:id',
				component: RemoveModuleComponent,
			},
			{
				path: ':id',
				loadChildren: () => import('./view-module/view-module.module').then(m => m.ViewModuleModule),
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ModulesRoutingModule {}
