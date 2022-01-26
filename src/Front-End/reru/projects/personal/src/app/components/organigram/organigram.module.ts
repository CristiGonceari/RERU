import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganigramRoutingModule } from './organigram-routing.module';
import { OrganigramComponent } from './organigram.component';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { OrganigramTableComponent } from './organigram-table/organigram-table.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { OrganigramDropdownDetailsComponent } from './organigram-dropdown-details/organigram-dropdown-details.component';
import { DetailsComponent } from './details/details.component';
import { NgxOrgChartModule } from 'ngx-org-chart';
import { EditComponent } from './edit/edit.component';


@NgModule({
  declarations: [
    OrganigramComponent,
    ListComponent,
    AddComponent,
    OrganigramTableComponent,
    OrganigramDropdownDetailsComponent,
    DetailsComponent,
    EditComponent
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
