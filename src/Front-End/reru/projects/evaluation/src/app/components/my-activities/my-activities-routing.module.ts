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
import { MyEvaluationsComponent } from './my-evaluations/my-evaluations.component';
import { AddEditSolicitedTestComponent } from './my-solicited-tests/add-edit-solicited-test/add-edit-solicited-test.component';
import { MyTestsComponent } from './my-tests/my-tests.component';
import { StartEvaluationPageComponent } from '../evaluations/start-evaluation-page/start-evaluation-page.component';
import { PerformingEvaluationComponent } from '../evaluations/performing-evaluation/performing-evaluation.component';
import { ViewEvaluationResultComponent } from '../evaluations/view-evaluation-result/view-evaluation-result.component';
import { MyPositionDiagramComponent } from './my-solicited-tests/my-position-diagram/my-position-diagram.component';

const routes: Routes = [
  {path: '', component: MyActivitiesComponent,
    children: [
      { path: '', redirectTo: 'my-solicited-position', pathMatch: 'full' },
      { path: 'my-tests', component: MyTestsComponent },
      { path: 'my-events', component: MyEventsComponent },
      { path: 'my-polls', component: MyPollsComponent },
      { path: 'my-evaluated-tests', component: MyEvaluatedTestsComponent },
      { path: 'my-evaluations', component: MyEvaluationsComponent },
      { path: 'my-solicited-position', component: MySolicitedTestsComponent },
      { path: 'my-diagram/:id', component: MyPositionDiagramComponent }
    ]
  },
  { path: 'start-test/:id', component: StartTestPageComponent },
  { path: 'start-evaluation/:id', component: StartEvaluationPageComponent },
  { path: 'one-test-per-page/:id', component: OnePerPagePerformingTestComponent},
  { path: 'multiple-per-page/:id', component: MultiplePerPagePerformingTestComponent},
  { path: 'performing-evaluation/:id', component: PerformingEvaluationComponent},
  { path: 'poll-progress/:id', component: ViewPollProgressComponent },
  { path: 'test-result/:id', component: ViewTestResultComponent },
  { path: 'evaluation-result/:id', component: ViewEvaluationResultComponent },
  { path: 'finish-page/:id', component: FinishPageComponent },
  { path: 'add-solicited-position', component: AddEditSolicitedTestComponent },
  { path: 'edit-solicited-position/:id', component: AddEditSolicitedTestComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyActivitiesRoutingModule { }
