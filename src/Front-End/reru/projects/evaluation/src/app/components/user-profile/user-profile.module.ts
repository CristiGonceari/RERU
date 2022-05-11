import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule, SvgModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';

import { UserProfileRoutingModule } from './user-profile-routing.module';
import { UserProfileComponent } from './user-profile.component';
import { UserTestsComponent } from './user-tests/user-tests.component';
import { UserEventsComponent } from './user-events/user-events.component';
import { UserPollsComponent } from './user-polls/user-polls.component';
import { OverviewComponent } from './overview/overview.component';
import { TestsTableComponent } from './user-tests/tests-table/tests-table.component';
import { TestsByEventComponent } from './user-events/tests-by-event/tests-by-event.component';
import { PollsByEventComponent } from './user-polls/polls-by-event/polls-by-event.component';
import { UserEvaluatedTestsComponent } from './user-evaluated-tests/user-evaluated-tests.component';
import { UserEvaluatedTestsListComponent } from './user-evaluated-tests/user-evaluated-tests-list/user-evaluated-tests-list.component';
import { UserSolicitedTestsComponent } from './user-solicited-tests/user-solicited-tests.component';
import { UserSolicitedTestsTableComponent } from './user-solicited-tests/user-solicited-tests-table/user-solicited-tests-table.component';


@NgModule({
  declarations: [
    UserProfileComponent,
    UserTestsComponent,
    UserEventsComponent,
    UserPollsComponent,
    OverviewComponent,
    TestsTableComponent,
    TestsByEventComponent,
    PollsByEventComponent,
    UserEvaluatedTestsComponent,
    UserEvaluatedTestsListComponent,
    UserSolicitedTestsComponent,
    UserSolicitedTestsTableComponent,
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
    UserProfileRoutingModule,
    SvgModule
  ]
})
export class UserProfileModule { }
