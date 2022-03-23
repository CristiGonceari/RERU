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
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';


const routes: Routes = [
	{ path: 'registration-page', component: RegistrationPageComponent},
	{ path: 'auth-callback', component: AuthenticationCallbackComponent},
	{
		path: '',
		component: MainComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{
				path: '',
				component: DashboardComponent,
				canActivate: [AuthenticationGuard]
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
				loadChildren: () => import('./components/modules/modules.module').then(m => m.ModulesModule)
			},
			{ 
				path: 'users', 
				loadChildren: () => import('./components/users/users.module').then(m => m.UsersModule)
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