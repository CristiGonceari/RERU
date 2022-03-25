import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegistrationPageRoutingModule } from './registration-page-routing.module';
import { RouterModule } from '@angular/router';
import { RegistrationPageComponent } from './registration-page.component';
import { BsDropdownModule } from "ngx-bootstrap/dropdown";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    RegistrationPageRoutingModule,
    
  ],
  providers: [
    TranslatePipe,
    Location
  ],
  declarations: [RegistrationPageComponent],
})
export class RegistrationPageModule { }
