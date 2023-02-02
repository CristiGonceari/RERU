import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationCallbackComponent, Exception404Component, Exception500Component } from '@erp/shared';
import { CacheMechanism, LocalizeParser, LocalizeRouterModule, LocalizeRouterSettings } from '@gilsdav/ngx-translate-router';
import { ManualLoaderFactory } from './utils/services/i18n/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { Location } from '@angular/common';
import { PermissionRouteGuard } from '@erp/shared';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
	{ path: '', component: DashboardComponent },
	{
		path: 'faq',
		loadChildren: () => import('./components/faq/faq.module').then(m => m.FAQModule),
		data: { permission: 'P04000002' },
		canActivate: [PermissionRouteGuard]
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
		}),],
	exports: [RouterModule, LocalizeRouterModule]
})
export class AppRoutingModule { }
