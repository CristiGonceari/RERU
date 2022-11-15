import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule, Location } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HttpLoaderFactory } from './utils/services/i18n/i18n.service';
import { UtilsModule } from './utils/utils.module';
import { SharedModule, SvgModule, MOCK_AUTHENTICATION } from '@erp/shared';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { environment } from '../environments/environment';
import { NOTIFICATION_INTERCEPTOR_PROVIDER } from './utils/interceptors/notification.interceptor'

@NgModule({
  declarations: [ AppComponent, DashboardComponent ],
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
  ],
  providers: [
    TranslatePipe, 
    Location,
    NOTIFICATION_INTERCEPTOR_PROVIDER,
    {
      provide: MOCK_AUTHENTICATION,
      useValue: !environment.PRODUCTION
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
