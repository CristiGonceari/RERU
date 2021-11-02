import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../utils/utils.module';
import { SharedModule } from '@erp/shared';
import { MyProfileRoutingModule } from './my-profile-routing.module';
import { MyProfileComponent } from './my-profile.component';
import { TranslatePipe, TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MyProfileComponent,
  ],
  imports: [
    CommonModule,
    MyProfileRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    SharedModule,
    NgbModule
  ],
  providers:[ TranslatePipe]
})
export class MyProfileModule { }
