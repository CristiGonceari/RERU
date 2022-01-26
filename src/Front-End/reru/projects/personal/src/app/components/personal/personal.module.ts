import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';

import { PersonalRoutingModule } from './personal-routing.module';
import { ListComponent } from './list/list.component';
import { PersonalComponent } from './personal.component';
import { EditComponent } from './edit/edit.component';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { TranslatePipe, TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddComponent } from './add/add.component';
import { SharedModule } from '@erp/shared';
import { PersonalTableComponent } from './personal-table/personal-table.component';
import { SearchComponent } from './list/search/search.component';
import { ListDropdownComponent } from './list-dropdown/list-dropdown.component';
import { FilterEmployeeStateComponent } from './list/filter-employee-state/filter-employee-state.component';
import { MaterialModule } from '../../material.module';
import { GeneralDataFormComponent } from './add/general-data-form/general-data-form.component';
import { BulletinDataFormComponent } from './add/bulletin-data-form/bulletin-data-form.component';
import { StudiesDataFormComponent } from './add/studies-data-form/studies-data-form.component';
import { UploadDataFormComponent } from './add/upload-data-form/upload-data-form.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { GenerateOrderComponent } from './generate-order/generate-order.component';
import { AddPositionComponent } from './generate-order/add-position/add-position.component';
import { IndividualContractRequestComponent } from './generate-order/individual-contract-request/individual-contract-request.component';
import { EmployeeFromDateComponent } from './list/employee-from-date/employee-from-date.component';
import { EmployeeToDateComponent } from './list/employee-to-date/employee-to-date.component';
import { EmployeePositionComponent } from './list/employee-position/employee-position.component';
import { SearchByDepartmentComponent } from './list/search-by-department/search-by-department.component';
import { PictureDataFormComponent } from './add/picture-data-form/picture-data-form.component';
import { RequestToEmployDataFormComponent } from './add/request-to-employ-data-form/request-to-employ-data-form.component';

@NgModule({
  declarations: [
    PersonalComponent,
    EditComponent,
    ListComponent,
    AddComponent,
    PersonalTableComponent,
    SearchComponent,
    ListDropdownComponent,
    FilterEmployeeStateComponent,
    GeneralDataFormComponent,
    BulletinDataFormComponent,
    StudiesDataFormComponent,
    UploadDataFormComponent,
    GenerateOrderComponent,
    AddPositionComponent,
    IndividualContractRequestComponent,
    EmployeeFromDateComponent,
    EmployeeToDateComponent,
    EmployeePositionComponent,
    SearchByDepartmentComponent,
    PictureDataFormComponent,
    RequestToEmployDataFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    PersonalRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    MaterialModule,
    NgxDropzoneModule
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class PersonalModule { }
