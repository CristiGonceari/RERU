import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrganigramComponent } from './organigram.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { NgxOrgChartModule } from 'ngx-org-chart';
import { OrganigramRoutingModule } from './organigram-routing.module';


@NgModule({
  declarations: [
    OrganigramComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    OrganigramRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    NgxOrgChartModule
  ]
})
export class OrganigramModule { }
