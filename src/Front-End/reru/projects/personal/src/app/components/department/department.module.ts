import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DepartmentComponent } from './department.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { DepartmentRoutingModule } from './department-routing.module';
import { EditComponent } from './edit/edit.component';
import { DepartmentDropdownDetailsComponent } from './department-dropdown-details/department-dropdown-details.component';
import { DepartmentPositionTableComponent } from './department-position-table/department-position-table.component';
import { DepartmentContentCardComponent } from './department-content-card/department-content-card.component';
import { ListComponent } from './list/list.component';
import { DepartmentTableComponent } from './department-table/department-table.component';
import { AddComponent } from './add/add.component';
import { DetailsComponent } from './details/details.component';
@NgModule({
  declarations: [
    DepartmentComponent,
    ListComponent,
    DepartmentTableComponent,
    AddComponent,
    DetailsComponent,
    EditComponent,
    DepartmentDropdownDetailsComponent,
    DepartmentPositionTableComponent,
    DepartmentContentCardComponent
  ],
  imports: [
    CommonModule,
    DepartmentRoutingModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    UtilsModule,
    NgbModule,
    SharedModule
  ]
})
export class DepartmentModule { }
