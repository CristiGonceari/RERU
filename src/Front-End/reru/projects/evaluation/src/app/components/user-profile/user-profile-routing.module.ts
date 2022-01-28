import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OverviewComponent } from './overview/overview.component';
import { UserEventsComponent } from './user-events/user-events.component';
import { UserPollsComponent } from './user-polls/user-polls.component';
import { UserProfileComponent } from './user-profile.component';
import { UserTestsComponent } from './user-tests/user-tests.component';

const routes: Routes = [
  {
    path: ':id', 
    component: UserProfileComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: OverviewComponent },
      { path: 'user-tests', component: UserTestsComponent },
      { path: 'user-events', component: UserEventsComponent },
      { path: 'user-polls', component: UserPollsComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserProfileRoutingModule { }
