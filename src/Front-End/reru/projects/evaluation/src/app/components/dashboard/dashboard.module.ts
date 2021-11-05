import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { MaterialModule } from '../../material.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PollsTableComponent } from './polls-table/polls-table.component';

@NgModule({
  declarations: [
    DashboardComponent,
    PollsTableComponent
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
    DashboardRoutingModule,
    MatProgressBarModule,
    MaterialModule,
    CKEditorModule
  ],
  schemas: [ 
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class DashboardModule { }
