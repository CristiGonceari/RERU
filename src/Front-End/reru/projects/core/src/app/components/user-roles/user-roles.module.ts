import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRolesComponent } from './user-roles.component';
import { UserRolesRoutingModule } from './user-roles-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';

@NgModule({
  declarations: [
    UserRolesComponent
  ],
  imports: [
    CommonModule,
    UserRolesRoutingModule,
    ReactiveFormsModule,
		FormsModule,
		NgbModule,
		TranslateModule,
		SharedModule,
		UtilsModule
  ]
})
export class UserRolesModule { }
