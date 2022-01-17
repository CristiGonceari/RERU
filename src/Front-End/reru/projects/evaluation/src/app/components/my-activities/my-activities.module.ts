import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { MyActivitiesRoutingModule } from './my-activities-routing.module';
import { MyActivitiesComponent } from './my-activities.component';
import { MyEventsComponent } from './my-events/my-events.component';
import { MyTestsComponent } from './my-tests/my-tests.component';
import { MyPollsComponent } from './my-polls/my-polls.component';
import { EventsTableComponent } from './my-events/events-table/events-table.component';
import { PollsTableComponent } from './my-polls/polls-table/polls-table.component';
import { TestsTableComponent } from './my-tests/tests-table/tests-table.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { MaterialModule } from '../../material.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [
    MyActivitiesComponent,
    MyEventsComponent,
    MyTestsComponent,
    MyPollsComponent,
    EventsTableComponent,
    PollsTableComponent,
    TestsTableComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    HttpClientModule,
    UtilsModule,
    MyActivitiesRoutingModule,
    MatProgressBarModule,
    MaterialModule,
    CKEditorModule
  ],
  schemas: [ 
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class MyActivitiesModule { }
