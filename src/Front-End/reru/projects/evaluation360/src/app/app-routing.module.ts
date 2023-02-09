import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeRouterModule, LocalizeParser, LocalizeRouterSettings, CacheMechanism, } from '@gilsdav/ngx-translate-router';
import { Location } from '@angular/common';
import { ManualLoaderFactory } from './utils/services/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationCallbackComponent, AuthenticationGuard, Exception404Component, Exception500Component, PermissionRouteGuard } from '@erp/shared';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
	{
		path: '',
		component: DashboardComponent,
		canActivate: [AuthenticationGuard]
	},
	{ 
		path: 'evaluation', 
		loadChildren: () => import('./components/evaluations/evaluation.module').then(m => m.EvaluationModule),
		canActivate: [AuthenticationGuard]
	},
	{
		path: 'faq',
		loadChildren: () => import('./components/faq/faq.module').then(m => m.FAQModule),
		data: { permission: 'P05000001' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{ path: '500', component: Exception500Component, pathMatch: 'full' },
	{ path: '404', component: Exception404Component, pathMatch: 'full' },
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
