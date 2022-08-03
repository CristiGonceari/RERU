import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
	LocalizeRouterModule,
	LocalizeParser,
	LocalizeRouterSettings,
	CacheMechanism,
} from '@gilsdav/ngx-translate-router';
import { Location } from '@angular/common';
import { ManualLoaderFactory } from './utils/services/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthenticationCallbackComponent, AuthenticationGuard } from '@erp/shared';
import { ModulesRoutingModule } from './components/modules/modules-routing.module';
import { MyProfileRoutingModule } from './components/my-profile/my-profile-routing.module';
import { Exception404Component } from './utils/exceptions/404/404.component';
import { MainComponent } from './components/main/main.component';
import { PermissionRouteGuard } from '@erp/shared';


const routes: Routes = [
	{
		path: 'registration-page',
		loadChildren: () => import('./components/registration-page/registration-page.module').then(m => m.RegistrationPageModule)
	},
	{
		path: 'vacant-positions-page',
		loadChildren: () => import('./components/vacant-positions-page/vacant-positions-page.module').then(m => m.VacantPositionsPageModule)
	},
	{ path: 'auth-callback', component: AuthenticationCallbackComponent},
	{
		path: '',
		component: MainComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{
				path: '',
				component : DashboardComponent,
				canActivate: [AuthenticationGuard]
			},
			{ 
				path: 'registration-flux', 
				loadChildren: () => import('./components/candidate-registration-flux/candidate-registration-flux.module').then(m => m.CandidateRegistrationFluxModule)
			},
			{ 
				path: 'personal-profile', 
				loadChildren: () => import('./components/my-profile/my-profile.module').then(m => m.MyProfileModule)
			},
			{ 
				path: 'user-profile', 
				loadChildren: () => import('./components/users/user-profile/user-profile.module').then(m => m.UserProfileModule)
			},
			{ 
				path: 'modules', 
				loadChildren: () => import('./components/modules/modules.module').then(m => m.ModulesModule),
				data: { permission: 'P00000001' },
				canActivate: [PermissionRouteGuard]
			},
			{ 
				path: 'users', 
				loadChildren: () => import('./components/users/users.module').then(m => m.UsersModule),
				data: { permission: 'P00000012' },
				canActivate: [PermissionRouteGuard]
			},
			{ 
				path: 'roles', 
				loadChildren: () => import('./components/roles/roles.module').then(m => m.RolesModule)
			},
			{ 
				path: 'my-profile', 
				loadChildren: () => import('./components/my-profile/my-profile.module').then(m => m.MyProfileModule)
			},
			{
				path: 'faq',
				loadChildren: () => import('./components/faq/faq.module').then(m => m.FAQModule),
				data: { permission: 'P00000025' },
				canActivate: [PermissionRouteGuard]
			},
			// {
			// 	path: 'departments',
			// 	loadChildren: () => import('./components/departments/departments.module').then(m => m.DepartmentsModule),
			// 	data: { permission: 'P00000026' },
			// 	canActivate: [PermissionRouteGuard]
			// },
			// {
			// 	path: 'user-roles',
			// 	loadChildren: () => import('./components/user-roles/user-roles.module').then(m => m.UserRolesModule),
			// 	data: { permission: 'P00000027' },
			// 	canActivate: [PermissionRouteGuard]
			// }
		],
	},
	{ path: '404', component: Exception404Component },
	{ path: '**', redirectTo: '404' }
];

@NgModule({
	imports: [
		ModulesRoutingModule,
		MyProfileRoutingModule,
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