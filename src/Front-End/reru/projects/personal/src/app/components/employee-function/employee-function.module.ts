import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { EmployeeFunctionRoutingModule } from './employee-function-routing.module';
import { ListComponent } from './list/list.component';
import { HttpClientModule } from '@angular/common/http';

import { EmployeeFunctionTableComponent } from './employee-function-table/employee-function-table.component';
import { EmployeeFunctionComponent } from './employee-function.component';
import { EmployeeFunctionDropdownComponent } from './employee-function-dropdown/employee-function-dropdown.component';
import { DetailsComponent } from './details/details.component';


@NgModule({
  declarations: [EmployeeFunctionComponent, ListComponent, EmployeeFunctionTableComponent, EmployeeFunctionDropdownComponent, DetailsComponent],
  imports: [
    CommonModule,
    EmployeeFunctionRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    NgbModule,
    SharedModule
  ]
})
export class EmployeeFunctionModule { }
