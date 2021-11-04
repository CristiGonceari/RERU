import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { NomenclatureRoutingModule } from './nomenclature-routing.module';
import { NomenclatureComponent } from './nomenclature.component';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@erp/shared';
import { NgxDnDModule } from '@swimlane/ngx-dnd';

@NgModule({
  declarations: [
    NomenclatureComponent,
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
