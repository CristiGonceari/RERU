import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationCallbackComponent, AuthenticationGuard } from '@erp/shared';
import { CacheMechanism, LocalizeParser, LocalizeRouterModule, LocalizeRouterSettings } from '@gilsdav/ngx-translate-router';
import { ManualLoaderFactory } from './utils/services/i18n/i18n.service';
import { Exception404Component } from './utils/exceptions/404/404.component';
import { TranslateService } from '@ngx-translate/core';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { Location } from '@angular/common';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
  { path: '',
  component: LayoutsComponent,
  // canActivate: [AuthenticationGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: '404', component: Exception404Component },
      { path: '**', redirectTo: '404' }
    ]
  }
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
