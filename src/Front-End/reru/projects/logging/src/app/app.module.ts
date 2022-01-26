import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from './utils/utils.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment.prod';
import { HttpLoaderFactory, MOCK_AUTHENTICATION, SharedModule } from '@erp/shared'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule, Location } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent
  ],
  imports: [
    UtilsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserModule,
    CommonModule,
    SharedModule,
    NgbModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      }
    }),
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
  ],
  schemas: [
		CUSTOM_ELEMENTS_SCHEMA
 	],
  providers: [
    TranslatePipe, Location,
    //CONFIG_INITIALIZER,
    // NOTIFICATION_INTERCEPTOR_PROVIDER,
    // IDNP_INTERCEPTOR_PROVIDER,
    {
      provide: MOCK_AUTHENTICATION,
      useValue: !environment.PRODUCTION
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
