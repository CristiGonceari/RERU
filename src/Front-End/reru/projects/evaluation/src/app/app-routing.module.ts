import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
	CacheMechanism,
	LocalizeParser,
	LocalizeRouterModule,
	LocalizeRouterSettings,
} from '@gilsdav/ngx-translate-router';
import { ManualLoaderFactory } from './utils/services/i18n/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { Location } from '@angular/common';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { AuthenticationCallbackComponent, AuthenticationGuard, PermissionRouteGuard } from '@erp/shared';
import { CategoryRoutingModule } from './components/categories/categories-routing.module';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
	{
		path: '',
		component: LayoutsComponent,
		canActivate: [AuthenticationGuard],
		children: [
			{ path: '', loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule) },
			{
				path: 'categories',
				loadChildren: () => import('./components/categories/categories.module').then(m => m.CategoriesModule),
				data: { permission: 'P03010102' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'test-type',
				loadChildren: () => import('./components/test-types/test-types.module').then(m => m.TestTypesModule),
				data: { permission: 'P03010402' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'questions',
				loadChildren: () => import('./components/questions/questions.module').then(m => m.QuestionsModule),
				data: { permission: 'P03010202' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'locations',
				loadChildren: () => import('./components/locations/locations.module').then(m => m.LocationsModule),
				data: { permission: 'P03011101' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'events',
				loadChildren: () => import('./components/events/events.module').then(m => m.EventsModule),
				data: { permission: 'P03011201' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'faq',
				loadChildren: () => import('./components/faq/faq.module').then(m => m.FaqModule),
				data: { permission: 'P03010904' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'tests',
				loadChildren: () => import('./components/tests/tests.module').then(m => m.TestsModule),
				data: { permission: 'P03010602' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'polls',
				loadChildren: () => import('./components/polls/polls.module').then(m => m.PollsModule),
			},
			{
				path: 'plans', 
				loadChildren: () => import('./components/plans/plans.module').then(m => m.PlansModule),
				data: { permission: 'P03011501' },
				// canActivate: [PermissionRouteGuard]
			},
			{
				path: 'statistics', 
				loadChildren: () => import('./components/statistics/statistics.module').then(m => m.StatisticsModule),
			}
		]
	},
	// { path: 'public', loadChildren: () => import('./components/public/public.module').then(m => m.PublicModule) },
];

@NgModule({
	imports: [
		CategoryRoutingModule,
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
export class AppRoutingModule {}
