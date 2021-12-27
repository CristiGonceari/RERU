import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment.prod';
import { MOCK_AUTHENTICATION, SharedModule } from '@erp/shared'

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,

  ],
  schemas: [
		CUSTOM_ELEMENTS_SCHEMA
 	],
  providers: [
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
