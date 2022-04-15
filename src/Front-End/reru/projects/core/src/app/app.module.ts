import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
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
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { environment } from '../environments/environment';
import { ModulesComponent } from './components/modules/modules.component';
import { RolesComponent } from './components/roles/roles.component';
import { MainComponent } from './components/main/main.component';
import { PositionsComponent } from './components/positions/positions.component';
import { NgxDropzoneModule } from 'ngx-dropzone';

@NgModule({
	declarations: [
			//AuthenticationCallbackComponent,
			AppComponent,
			//    LoginComponent,
			DashboardComponent,
			ModulesComponent,
			RolesComponent,
			MainComponent,
			PositionsComponent,
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
		SharedModule,
		AppRoutingModule,
		NgbModule,
		UtilsModule,
		CommonModule,
		SvgModule,
		NgxDropzoneModule,
		NgxDnDModule.forRoot(),
	],
	schemas: [
		CUSTOM_ELEMENTS_SCHEMA
 	],
	providers: [TranslatePipe, Location,
    {
      provide: MOCK_AUTHENTICATION,
      useValue: !environment.PRODUCTION
    }],
  	bootstrap: [AppComponent]
})
export class AppModule { }
