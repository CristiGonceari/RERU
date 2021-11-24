import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PerformingPollComponent } from './performing-poll/performing-poll.component';
import { StartPollPageComponent } from './start-poll-page/start-poll-page.component';
import { ViewPollProgressComponent } from './view-poll-progress/view-poll-progress.component';

const routes: Routes = [
  { path: '', component: StartPollPageComponent },
  { path: 'start-poll-page/:id', component: StartPollPageComponent },
  { path: 'performing-poll/:id', component: PerformingPollComponent },
  { path: 'poll-progress/:id', component: ViewPollProgressComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PollsRoutingModule { }
