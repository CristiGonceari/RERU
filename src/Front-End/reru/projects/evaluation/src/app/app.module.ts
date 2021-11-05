import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpLoaderFactory } from './utils/services/i18n/i18n.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
// import { CONFIG_INITIALIZER } from "./utils/util/initializer.util";
import { UtilsModule } from './utils/utils.module';
import { SharedModule, MOCK_AUTHENTICATION, SvgModule } from '@erp/shared';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { environment } from '../environments/environment';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LayoutsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
		CommonModule,
		SvgModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    SimpleNotificationsModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      }
    }),
    SharedModule,
    FormsModule,
    UtilsModule,
  ],
  entryComponents: [
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [TranslatePipe, Location,
    {
      provide: MOCK_AUTHENTICATION,
      useValue: !environment.PRODUCTION
    }],
  bootstrap: [AppComponent],
})
export class AppModule { }
