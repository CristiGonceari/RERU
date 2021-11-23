import { UserProfileRoutingModule } from './user-profile-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../../utils/utils.module';
import { UserProfileComponent } from './user-profile.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserOverviewComponent } from './user-overview/user-overview.component';
import { ModuleAccessModule } from './module-access/module-access.module'

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UserProfileRoutingModule,
    ModuleAccessModule,
    NgbModule,
    SvgModule
  ],
  declarations: [
    UserProfileComponent,
    UserOverviewComponent,
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class UserProfileModule { }
