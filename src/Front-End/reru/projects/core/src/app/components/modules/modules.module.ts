import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModulesRoutingModule } from './modules-routing.module';
import { ListModuleComponent } from './list-module/list-module.component';
import { RemoveModuleComponent } from './remove-module/remove-module.component';
import { AddEditModuleComponent } from './add-edit-module/add-edit-module.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    ModulesRoutingModule,
    TranslateModule,
    NgbModule,
    SvgModule
  ],
  declarations: [
    ListModuleComponent,
    AddEditModuleComponent,
    RemoveModuleComponent,
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class ModulesModule { }
