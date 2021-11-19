import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../../utils/utils.module';
import { ModuleOverviewComponent } from './module-overview/module-overview.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ViewModuleRoutingModule } from './view-module-routing.module';
import { ViewModuleComponent } from './view-module.component';
import { ModuleRolesComponent } from './module-roles/module-roles.component';
import { ModulePermissionsComponent } from './module-permissions/module-permissions.component';
import { PermissionSearchComponent } from './module-permissions/permission-search/permission-search.component';

@NgModule({
    imports: [
      CommonModule,
      RouterModule,
      SharedModule,
      UtilsModule,
      FormsModule,
      ReactiveFormsModule,
      TranslateModule,
      ViewModuleRoutingModule,
      NgbModule,
      SvgModule
    ],
    declarations: [
      ViewModuleComponent,
      ModuleOverviewComponent,
      ModuleRolesComponent,
      ModulePermissionsComponent,
      PermissionSearchComponent
    ],
    providers: [
      TranslatePipe,
      Location
    ]
  })
  export class ViewModuleModule { }
  