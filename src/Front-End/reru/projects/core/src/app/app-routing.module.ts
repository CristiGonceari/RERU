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
import { AuthenticationGuard } from '@erp/shared';

const routes: Routes = [{
  path: '',
  component: DashboardComponent,
  canActivate: [AuthenticationGuard]
}]
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
