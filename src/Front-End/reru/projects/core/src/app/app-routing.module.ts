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
import { AuthenticationGuard } from '@erp/shared';
import { DashboardComponent } from './main/dashboard/dashboard.component'
import { MainComponent } from './main/main.component';

const routes: Routes = [{
  path: '',
  component: MainComponent,
  canActivate: [AuthenticationGuard],
  children: [
	{ path: '', component: DashboardComponent, canActivate: [AuthenticationGuard] },

  { path: 'modules', loadChildren: () => import('./main/modules/modules.module').then(m => m.ModulesModule) },
  { path: 'users', loadChildren: () => import('./main/users/users.module').then(m => m.UsersModule) },
  { path: 'roles', loadChildren: () => import('./main/roles/roles.module').then(m => m.RolesModule) },
  { path: 'my-profile', loadChildren: () => import('./main/my-profile/my-profile.module').then(m => m.MyProfileModule) }]
  
}];
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
