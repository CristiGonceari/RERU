import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModuleAccessComponent } from './module-access.component'
import { ModuleAccessListComponent } from './module-access-list/module-access-list.component';
import { AddEditModuleAccessComponent } from './add-edit-module-access/add-edit-module-access.component';
import { RemoveModuleAccessComponent } from './remove-module-access/remove-module-access.component';
import { ModuleAccessRoutingModule } from './module-access-routing.module'


@NgModule({
  declarations: [
    ModuleAccessComponent,
    ModuleAccessListComponent,
    AddEditModuleAccessComponent,
    RemoveModuleAccessComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    ModuleAccessRoutingModule,
    TranslateModule,
    NgbModule,
    SvgModule
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class ModuleAccessModule { }

