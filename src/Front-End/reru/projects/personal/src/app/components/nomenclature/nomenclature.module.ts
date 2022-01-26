import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { NomenclatureRoutingModule } from './nomenclature-routing.module';
import { ListComponent } from './list/list.component';
import { NomenclatureComponent } from './nomenclature.component';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddNomenclatureComponent } from './add-nomenclature/add-nomenclature.component';
import { SharedModule } from '@erp/shared';
import { NomenclatureTableComponent } from './nomenclature-table/nomenclature-table.component';
import { NomenclatureDropdownDetailsComponent } from './nomenclature-dropdown-details/nomenclature-dropdown-details.component';
import { EditNomenclatureComponent } from './edit-nomenclature/edit-nomenclature.component';
import { NomenclatureDropdownListComponent } from './nomenclature-dropdown-list/nomenclature-dropdown-list.component';
import { DetailsNomenclatureComponent } from './details-nomenclature/details-nomenclature.component';
import { NomenclatureStatusLabelComponent } from './nomenclature-status-label/nomenclature-status-label.component';
import { NomenclatureHeaderCreateComponent } from './nomenclature-header-create/nomenclature-header-create.component';
import { NomenclatureRecordCreateComponent } from './nomenclature-record-create/nomenclature-record-create.component';
import { NgxDnDModule } from '@swimlane/ngx-dnd';
import { NomenclatureRecordComponent } from './nomenclature-record/nomenclature-record.component';

@NgModule({
  declarations: [
    ListComponent,
    NomenclatureComponent,
    AddNomenclatureComponent,
    NomenclatureTableComponent,
    NomenclatureDropdownDetailsComponent,
    EditNomenclatureComponent,
    NomenclatureDropdownListComponent,
    DetailsNomenclatureComponent,
    NomenclatureStatusLabelComponent,
    NomenclatureHeaderCreateComponent,
    NomenclatureRecordCreateComponent,
    NomenclatureRecordComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    NomenclatureRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    NgxDnDModule.forRoot()
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class NomenclatureModule { }
