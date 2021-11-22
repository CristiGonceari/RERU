import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RoleDetailsRoutingModule } from './role-details-routing.module';
import { RolePermissionsListComponent } from './role-permissions-list/role-permissions-list.component';
import { UpdateRolePermissionsComponent } from './update-role-permissions/update-role-permissions.component';

@NgModule({
    imports: [
      CommonModule,
      RouterModule,
      SharedModule,
      UtilsModule,
      FormsModule,
      ReactiveFormsModule,
      TranslateModule,
      RoleDetailsRoutingModule,
      NgbModule
    ],
    declarations: [
      RolePermissionsListComponent,
      UpdateRolePermissionsComponent
    ],
    providers: [
      TranslatePipe,
      Location
    ]
  })
  export class RoleDetailsModule { }
  