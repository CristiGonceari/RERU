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
import { PlansModule } from './components/plans/plans.module';
import { EventsModule } from './components/events/events.module';
import { PollsModule } from './components/polls/polls.module';
import { FaqRoutingModule } from './components/faq/faq-routing.module';
import { DashboardRoutingModule } from './components/dashboard/dashboard-routing.module';
import { StatisticsModule } from './components/statistics/statistics.module';
import { StatisticsRoutingModule } from './components/statistics/statistics-routing.module';
import { PlansRoutingModule } from './components/plans/plans-routing.module';
import { TestsRoutingModule } from './components/tests/tests-routing.module';
import { TestTypesRoutingModule } from './components/test-types/test-types-routing.module';
import { EventsRoutingModule } from './components/events/events-routing.module';
import { LocationsRoutingModule } from './components/locations/locations-routing.module';
import { QuestionsRoutingModule } from './components/questions/questions-routing.module';
import { CategoryRoutingModule } from './components/categories/categories-routing.module';
import { MaterialModule } from './material.module'
import { NOTIFICATION_INTERCEPTOR_PROVIDER } from './utils/interceptors/notification.interceptor'
import { IDNP_INTERCEPTOR_PROVIDER } from './utils/interceptors/idnp.interceptor';


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
    EventsModule,
    PollsModule,
    PlansModule,
    CategoryRoutingModule,
    QuestionsRoutingModule,
    LocationsRoutingModule,
    EventsRoutingModule,
    TestTypesRoutingModule,
    FaqRoutingModule,
    TestsRoutingModule,
    DashboardRoutingModule,
    AppRoutingModule,
    PlansRoutingModule,
    StatisticsRoutingModule,
    StatisticsModule,
    MaterialModule
  ],
  entryComponents: [
  ],
  providers: [
    TranslatePipe,
    //CONFIG_INITIALIZER,
    NOTIFICATION_INTERCEPTOR_PROVIDER,
    IDNP_INTERCEPTOR_PROVIDER,
    {
      provide: MOCK_AUTHENTICATION,
      useValue: !environment.PRODUCTION
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
