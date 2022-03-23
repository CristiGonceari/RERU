import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewPollProgressComponent } from '../polls/view-poll-progress/view-poll-progress.component';
import { FinishPageComponent } from '../tests/finish-page/finish-page.component';
import { MultiplePerPagePerformingTestComponent } from '../tests/multiple-per-page-performing-test/multiple-per-page-performing-test.component';
import { OnePerPagePerformingTestComponent } from '../tests/one-per-page-performing-test/one-per-page-performing-test.component';
import { StartTestPageComponent } from '../tests/start-test-page/start-test-page.component';
import { ViewTestResultComponent } from '../tests/view-test-result/view-test-result.component';
import { MyActivitiesComponent } from './my-activities.component';
import { MyEvaluatedTestsComponent } from './my-evaluated-tests/my-evaluated-tests.component';
import { MyEventsComponent } from './my-events/my-events.component';
import { MyPollsComponent } from './my-polls/my-polls.component';
import { MySolicitedTestsComponent } from './my-solicited-tests/my-solicited-tests.component';
import { AddEditSolicitedTestComponent } from './my-solicited-tests/add-edit-solicited-test/add-edit-solicited-test.component';
import { MyTestsComponent } from './my-tests/my-tests.component';

const routes: Routes = [
  {path: '', component: MyActivitiesComponent,
  children: [
    { path: '', redirectTo: 'my-solicited-tests', pathMatch: 'full' },
    { path: 'my-tests', component: MyTestsComponent },
    { path: 'my-events', component: MyEventsComponent },
    { path: 'my-polls', component: MyPollsComponent },
    { path: 'my-evaluated-tests', component: MyEvaluatedTestsComponent },
    { path: 'my-solicited-tests', component: MySolicitedTestsComponent },
  ]},
  { path: 'start-test/:id', component: StartTestPageComponent },
  { path: 'one-test-per-page/:id', component: OnePerPagePerformingTestComponent},
  { path: 'multiple-per-page/:id', component: MultiplePerPagePerformingTestComponent},
  { path: 'poll-progress/:id', component: ViewPollProgressComponent },
  { path: 'test-result/:id', component: ViewTestResultComponent },
  { path: 'finish-page/:id', component: FinishPageComponent },
  { path: 'add-solicited-test', component: AddEditSolicitedTestComponent },
  { path: 'edit-solicited-test/:id', component: AddEditSolicitedTestComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyActivitiesRoutingModule { }
