import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { NgxOrgChartModule } from 'ngx-org-chart';
import { PontajData } from './pontaj-data.component';
import { PontajDataTableRoutingModule } from './pontaj-data-table-routing.module';
import { PontajDataTableComponent } from './pontaj-data-table/pontaj-data-table.component';
import { ListComponent } from './list/list.component';
import { SearchByContractorsComponent } from './list/search-by-contractors/search-by-contractors.component';
import { SearchByDateComponent } from './list/search-by-date/search-by-date.component';
import { SearchByDepartmentsComponent } from './list/search-by-departments/search-by-departments.component';

@NgModule({
  declarations: [
    PontajData,
    PontajDataTableComponent,
    ListComponent,
    SearchByContractorsComponent,
    SearchByDateComponent,
    SearchByDepartmentsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    NgxOrgChartModule,
    PontajDataTableRoutingModule,
    FormsModule
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class PontajDataModule { }
