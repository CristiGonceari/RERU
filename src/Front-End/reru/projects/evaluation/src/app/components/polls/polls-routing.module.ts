import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PerformingPollComponent } from './performing-poll/performing-poll.component';

import { PollsComponent } from './polls.component';
import { StartPollPageComponent } from './start-poll-page/start-poll-page.component';
import { ViewPollProgressComponent } from './view-poll-progress/view-poll-progress.component';

const routes: Routes = [
  { path: '', component: PollsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PollsRoutingModule { }
