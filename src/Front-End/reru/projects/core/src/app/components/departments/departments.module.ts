import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentsComponent } from './departments.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { DepartmentsTableComponent } from './departments-table/departments-table.component';
import { SearchComponent } from './departments-table/search/search.component';
import { AddEditDepartmentComponent } from './add-edit-department/add-edit-department.component';
import { DepartmentDetailsComponent } from './department-details/department-details.component';
import { DepartmentOverviewComponent } from './department-details/department-overview/department-overview.component';

@NgModule({
  declarations: [
    DepartmentsComponent,
    DepartmentsTableComponent,
    SearchComponent,
    AddEditDepartmentComponent,
    DepartmentDetailsComponent,
    DepartmentOverviewComponent
  ],
  imports: [
    CommonModule,
    DepartmentsRoutingModule,
		ReactiveFormsModule,
		FormsModule,
		NgbModule,
		TranslateModule,
		SharedModule,
		UtilsModule
  ]
})
export class DepartmentsModule { }
