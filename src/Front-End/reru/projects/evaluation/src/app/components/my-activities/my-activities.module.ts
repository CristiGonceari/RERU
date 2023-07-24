import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule, SvgModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { MyActivitiesRoutingModule } from './my-activities-routing.module';
import { MyActivitiesComponent } from './my-activities.component';
import { MyEventsComponent } from './my-events/my-events.component';
import { MyTestsComponent } from './my-tests/my-tests.component';
//import { MyPollsComponent } from './my-polls/my-polls.component';
import { EventsTableComponent } from './my-events/events-table/events-table.component';
//import { PollsTableComponent } from './my-polls/polls-table/polls-table.component';
import { TestsTableComponent } from './my-tests/tests-table/tests-table.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { MaterialModule } from '../../material.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MyEvaluatedTestsComponent } from './my-evaluated-tests/my-evaluated-tests.component';
import { EvaluatedTestsTableComponent } from './my-evaluated-tests/evaluated-tests-table/evaluated-tests-table.component';
import { MySolicitedTestsComponent } from './my-solicited-tests/my-solicited-tests.component';
import { SolicitedTestsTableComponent } from './my-solicited-tests/solicited-tests-table/solicited-tests-table.component';
import { AddEditSolicitedTestComponent } from './my-solicited-tests/add-edit-solicited-test/add-edit-solicited-test.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { MyEvaluationsComponent } from './my-evaluations/my-evaluations.component';
import { MyPositionDiagramComponent } from './my-solicited-tests/my-position-diagram/my-position-diagram.component';
import { MyPositionDetailsComponent } from './my-solicited-tests/my-position-details/my-position-details.component';

@NgModule({
  declarations: [
    MyActivitiesComponent,
    MyEventsComponent,
    MyTestsComponent,
    //MyPollsComponent,
    EventsTableComponent,
    //PollsTableComponent,
    TestsTableComponent,
    MyEvaluatedTestsComponent,
    EvaluatedTestsTableComponent,
    MySolicitedTestsComponent,
    SolicitedTestsTableComponent,
    AddEditSolicitedTestComponent,
    MyEvaluationsComponent,
    MyPositionDiagramComponent,
    MyPositionDetailsComponent
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
    CKEditorModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    SvgModule
  ],
  schemas: [ 
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class MyActivitiesModule { }
