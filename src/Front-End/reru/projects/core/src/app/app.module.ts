import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule, Location } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
// import { PERSONAL_MODULE_INITIALIZER } from './utils/util/initializer.util';
import { HttpLoaderFactory } from './utils/services/i18n.service';
import { UtilsModule } from './utils/utils.module';
import { SharedModule, SvgModule, MOCK_AUTHENTICATION } from '@erp/shared';
import { SimpleNotificationsModule } from 'angular2-notifications';
// import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { environment } from '../environments/environment';
import { ModulesComponent } from './components/modules/modules.component';
import { RolesComponent } from './components/roles/roles.component';
import { MainComponent } from './components/main/main.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { DEFAULT_TIMEOUT, TimeoutInterceptor } from '../app/utils/specific-interceptor/timeout-intercepter';

@NgModule({
	declarations: [
		//AuthenticationCallbackComponent,
		AppComponent,
		//    LoginComponent,
		ModulesComponent,
		RolesComponent,
		MainComponent,
		DashboardComponent
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		HttpClientModule,
		SimpleNotificationsModule.forRoot(),
		TranslateModule.forRoot({
			loader: {
				provide: TranslateLoader,
				useFactory: HttpLoaderFactory,
				deps: [HttpClient],
			},
		}),
		SharedModule.forRoot(environment),
		AppRoutingModule,
		NgbModule,
		UtilsModule,
		CommonModule,
		SvgModule,
		NgxDropzoneModule,
		// NgxDnDModule.forRoot(),
		HttpClientModule,
	],
	schemas: [
		CUSTOM_ELEMENTS_SCHEMA
	],
	// providers: [[TranslatePipe, Location,{ provide: MOCK_AUTHENTICATION, useValue: !environment.PRODUCTION }]
	// 		[{ provide: HTTP_INTERCEPTORS, useClass: TimeoutInterceptor, multi: true }],
	// 		[{ provide: DEFAULT_TIMEOUT, useValue: 30000 }]
	// ],
	providers: [
		[{ provide: HTTP_INTERCEPTORS, useClass: TimeoutInterceptor, multi: true }],
		[{ provide: DEFAULT_TIMEOUT, useValue: 30000 }],
		[TranslatePipe, Location, { provide: MOCK_AUTHENTICATION, useValue: !environment.PRODUCTION }]
	],
	bootstrap: [AppComponent]
})
export class AppModule {
	constructor() {}
 }
