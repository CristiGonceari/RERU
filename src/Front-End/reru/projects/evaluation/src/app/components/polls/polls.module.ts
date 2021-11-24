import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PollsRoutingModule } from './polls-routing.module';
import { PerformingPollComponent } from './performing-poll/performing-poll.component';
import { StartPollPageComponent } from './start-poll-page/start-poll-page.component';
import { ViewPollProgressComponent } from './view-poll-progress/view-poll-progress.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@erp/shared';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MaterialModule } from '../../material.module';
import { MatProgressBarModule } from '@angular/material/progress-bar';


@NgModule({
  declarations: [
    PerformingPollComponent, 
    StartPollPageComponent, 
    ViewPollProgressComponent
  ],
  imports: [
    CommonModule,
    PollsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    UtilsModule,
    RouterModule,
    MatProgressBarModule,
    MaterialModule,
    CKEditorModule
  ]
})
export class PollsModule { }
