import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { MyActivitiesRoutingModule } from './my-activities-routing.module';
import { MyActivitiesComponent } from './my-activities.component';

@NgModule({
  declarations: [
    MyActivitiesComponent
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
    MyActivitiesRoutingModule
  ]
})
export class MyActivitiesModule { }
