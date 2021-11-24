import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerformingPollComponent } from '../polls/performing-poll/performing-poll.component';
import { StartPollPageComponent } from '../polls/start-poll-page/start-poll-page.component';
import { ViewPollProgressComponent } from '../polls/view-poll-progress/view-poll-progress.component';
import { MultiplePerPagePerformingTestComponent } from '../tests/multiple-per-page-performing-test/multiple-per-page-performing-test.component';
import { OnePerPagePerformingTestComponent } from '../tests/one-per-page-performing-test/one-per-page-performing-test.component';
import { StartTestPageComponent } from '../tests/start-test-page/start-test-page.component';
import { MyActivitiesComponent } from './my-activities.component';
import { MyEventsComponent } from './my-events/my-events.component';
import { MyPollsComponent } from './my-polls/my-polls.component';
import { MyTestsComponent } from './my-tests/my-tests.component';

const routes: Routes = [
  {path: '', component: MyActivitiesComponent,
  children: [
    { path: '', redirectTo: 'my-tests', pathMatch: 'full' },
    {
      path: 'my-tests',
      component: MyTestsComponent
    },
    {
      path: 'my-events',
      component: MyEventsComponent
    },
    {
      path: 'my-polls',
      component: MyPollsComponent
    },
  ]},
  { path: 'start-test/:id', component: StartTestPageComponent },
  { path: 'run-test/:id', component: OnePerPagePerformingTestComponent},
  { path: 'run-test-questions/:id', component: MultiplePerPagePerformingTestComponent},
  
  { path: 'performing-poll/:id', component: PerformingPollComponent },
  { path: 'poll-progress/:id', component: ViewPollProgressComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyActivitiesRoutingModule { }
