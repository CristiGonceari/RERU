import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RolesRoutingModule } from './roles-routing.module';
import { RolesComponent } from './roles.component';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    RolesComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RolesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    NgbModule,
    SharedModule
  ]
})
export class RolesModule { }
