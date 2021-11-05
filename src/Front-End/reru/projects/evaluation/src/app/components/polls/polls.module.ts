import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PollsRoutingModule } from './polls-routing.module';
import { PollsComponent } from './polls.component';


@NgModule({
  declarations: [PollsComponent],
  imports: [
    CommonModule,
    PollsRoutingModule
  ]
})
export class PollsModule { }
