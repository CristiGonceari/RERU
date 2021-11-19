import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyActivitiesRoutingModule { }
