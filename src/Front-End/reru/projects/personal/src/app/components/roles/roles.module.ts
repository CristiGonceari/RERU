import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RolesRoutingModule } from './roles-routing.module';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

import { RolesComponent } from './roles.component';
import { ListComponent } from './list/list.component';
import { RolesTableComponent } from './roles-table/roles-table.component';
import { DetailsComponent } from './details/details.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { RolesDropdownComponent } from './roles-dropdown/roles-dropdown.component';
import { SearchComponent } from './list/search/search.component';
import { FilterRoleStateComponent } from './list/filter-role-state/filter-role-state.component';



@NgModule({
  declarations: [
    RolesComponent,
    ListComponent,
    RolesTableComponent,
    DetailsComponent,
    AddComponent,
    EditComponent,
    RolesDropdownComponent,
    SearchComponent,
    FilterRoleStateComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RolesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    NgbModule,
    SharedModule
  ]
})
export class RolesModule { }
