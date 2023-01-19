import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeRouterModule, LocalizeParser, LocalizeRouterSettings, CacheMechanism, } from '@gilsdav/ngx-translate-router';
import { Location } from '@angular/common';
import { ManualLoaderFactory } from './utils/services/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationCallbackComponent, AuthenticationGuard, Exception404Component, Exception500Component } from '@erp/shared';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
	{
		path: '',
		component: DashboardComponent,
		canActivate: [AuthenticationGuard]
	},
	{
		path: 'my-profile',
		loadChildren: () => import('./components/my-profile/my-profile.module').then(m => m.MyProfileModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'personal',
		loadChildren: () => import('./components/contractors/contractors.module').then(m => m.ContractorsModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'department',
		loadChildren: () => import('./components/department/department.module').then(m => m.DepartmentModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'nomenclature',
		loadChildren: () => import('./components/nomenclature/nomenclature.module').then(m => m.NomenclatureModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'roles',
		loadChildren: () => import('./components/roles/roles.module').then(m => m.RolesModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'employee-functions',
		loadChildren: () => import('./components/employee-function/employee-function.module').then(m => m.EmployeeFunctionModule),
		canActivate: [AuthenticationGuard],
	},

	{
		path: 'vacation',
		loadChildren: () => import('./components/vacation/vacation.module').then(m => m.VacationModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'organigram',
		loadChildren: () => import('./components/organigram/organigram.module').then(m => m.OrganigramModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'reports',
		loadChildren: () => import('./components/reports/reports.module').then(m => m.ReportsModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'configurations',
		loadChildren: () => import('./components/configurations/configurations.module').then(m => m.ConfigurationsModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'time-sheet',
		loadChildren: () => import('./components/pontaj-data/pontaj-data.module').then(m => m.PontajDataModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'documents-templates',
		loadChildren: () => import('./components/documents-templates/documents-templates.module').then(m => m.DocumentsTemplatesModule),
		canActivate: [AuthenticationGuard],
	},
	{
		path: 'faq',
		loadChildren: () => import('./components/faq/faq.module').then(m => m.FAQModule),
		canActivate: [AuthenticationGuard]
	},
	{ path: '500', component: Exception500Component },
	{ path: '404', component: Exception404Component },
	{ path: '**', redirectTo: '404' }
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes, {
			useHash: true,
			scrollPositionRestoration: 'enabled',
		}),
		LocalizeRouterModule.forRoot(routes, {
			parser: {
				provide: LocalizeParser,
				useFactory: ManualLoaderFactory,
				deps: [TranslateService, Location, LocalizeRouterSettings],
			},
			cacheMechanism: CacheMechanism.Cookie,
			cookieFormat: '{{value}};{{expires:20}};path=/',
			alwaysSetPrefix: false,
		}),
	],
	exports: [RouterModule, LocalizeRouterModule],
})
export class AppRoutingModule { }
