import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VacationRoutingModule } from './vacation-routing.module';
import { VacationComponent } from './vacation.component';
import { ListComponent } from './list/list.component';
import { VacationTableComponent } from './vacation-table/vacation-table.component';
import { VacationDropdownDetailsComponent } from './vacation-dropdown-details/vacation-dropdown-details.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { AddComponent } from './add/add.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

@NgModule({
  declarations: [
    VacationComponent,
    ListComponent,
    VacationTableComponent,
    VacationDropdownDetailsComponent,
    AddComponent,
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
    VacationRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule
  ]
})
export class VacationModule { }
