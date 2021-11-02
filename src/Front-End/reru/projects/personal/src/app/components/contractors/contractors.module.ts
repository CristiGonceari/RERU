import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { TranslatePipe, TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@erp/shared';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { ContractorsComponent } from './contractors.component';
import { ContractorsRoutingModule } from './contractors-routing.module'
@NgModule({
  declarations: [
    ContractorsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    ContractorsRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    NgxDropzoneModule
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class ContractorsModule { }
