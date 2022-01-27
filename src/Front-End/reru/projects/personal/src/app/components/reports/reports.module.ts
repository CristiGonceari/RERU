import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@erp/shared';
import { ReportsRoutingModule } from './reports-routing.module';
import { ReportsComponent } from './reports.component';
import { ReportsTableComponent } from './reports-table/reports-table.component';
import { ListComponent } from './list/list.component';
import { TranslateModule } from '@ngx-translate/core';
import { ReportsDropdownDetailsComponent } from './reports-dropdown-details/reports-dropdown-details.component';
import { DetailsComponent } from './details/details.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgxOrgChartModule } from 'ngx-org-chart';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../utils/utils.module';
import { ReportsFilterComponent } from './reports-filter/reports-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FilterByDepartmentsComponent } from './reports-filter/filter-by-departments/filter-by-departments.component';

@NgModule({
  declarations: [
    ReportsComponent,
    ReportsTableComponent, 
    ListComponent, 
    ReportsDropdownDetailsComponent, 
    DetailsComponent, 
    ReportsFilterComponent, FilterByDepartmentsComponent
  ],
  imports: [
    CommonModule,
    ReportsRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    RouterModule,
    HttpClientModule,
    NgxOrgChartModule,
    NgbModule,
    UtilsModule
  ]
})
export class ReportsModule { }
