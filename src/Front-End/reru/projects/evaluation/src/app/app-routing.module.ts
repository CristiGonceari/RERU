import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CacheMechanism, LocalizeParser, LocalizeRouterModule, LocalizeRouterSettings } from '@gilsdav/ngx-translate-router';
import { Location, HashLocationStrategy } from '@angular/common';
import { ManualLoaderFactory } from './utils/services/i18n/i18n.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationCallbackComponent, AuthenticationGuard, Exception404Component, Exception500Component } from '@erp/shared';
import { PermissionRouteGuard } from '@erp/shared';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { environment } from '../environments/environment';

const routes: Routes = [
	{ path: 'auth-callback', component: AuthenticationCallbackComponent },
	{ 
		path: '', 
		component: DashboardComponent, 
		canActivate: [AuthenticationGuard] 
	},
	{
		path: 'categories', 
		loadChildren: () => import('./components/categories/categories.module').then(m => m.CategoriesModule) ,
		data: { permission: 'P03000401' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'test-type',
		loadChildren: () => import('./components/test-templates/test-templates.module').then(m => m.TestTemplatesModule),
		data: { permission: 'P03000801' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'questions',
		loadChildren: () => import('./components/questions/questions.module').then(m => m.QuestionsModule),
		data: { permission: 'P03000501' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'locations',
		loadChildren: () => import('./components/locations/locations.module').then(m => m.LocationsModule),
		data: { permission: 'P03000201' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'events',
		loadChildren: () => import('./components/events/events.module').then(m => m.EventsModule),
		data: { permission: 'P03000101' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'faq',
		loadChildren: () => import('./components/faq/faq.module').then(m => m.FAQModule),
		data: { permission: 'P03000001' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'tests',
		loadChildren: () => import('./components/tests/tests.module').then(m => m.TestsModule),
		data: { permission: 'P03000601' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'evaluations',
		loadChildren: () => import('./components/evaluations/evaluations.module').then(m => m.EvaluationsModule),
		data: { permission: 'P03000601' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'polls',
		loadChildren: () => import('./components/polls/polls.module').then(m => m.PollsModule),
		canActivate: [AuthenticationGuard]
	},
	{
		path: 'plans', 
		loadChildren: () => import('./components/plans/plans.module').then(m => m.PlansModule),
		data: { permission: 'P03000301' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'statistics', 
		loadChildren: () => import('./components/statistics/statistics.module').then(m => m.StatisticsModule),
		data: { permission: 'P03000901' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'my-activities', 
		loadChildren: () => import('./components/my-activities/my-activities.module').then(m => m.MyActivitiesModule),
		canActivate: [AuthenticationGuard]
	},
	{
		path: 'user-profile', 
		loadChildren: () => import('./components/user-profile/user-profile.module').then(m => m.UserProfileModule),
		canActivate: [AuthenticationGuard]
	},
	{
		path: 'solicited-positions', 
		loadChildren: () => import('./components/solicited-tests/solicited-tests.module').then(m => m.SolicitedTestsModule),
		data: { permission: 'P03001101' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'documents-templates',
		loadChildren: () => import('./components/document-templates/document-templates.module').then(m => m.DocumentTemplatesModule),
		data: { permission: 'P03001301' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'positions', 
		loadChildren: () => import('./components/positions/positions.module').then(m => m.PositionsModule),
		data: { permission: 'P03001201' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
	},
	{
		path: 'required-documents',
		loadChildren: () => import('./components/required-documents/required-documents.module').then(m => m.RequiredDocumentsModule),
		data: { permission: 'P03001401' },
		canActivate: [PermissionRouteGuard, AuthenticationGuard]
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
			defaultLangFunction: (_, cachedLang) => cachedLang || environment.DEFAULT_LANGUAGE,
			parser: {
				provide: LocalizeParser,
				useClass: HashLocationStrategy,
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
