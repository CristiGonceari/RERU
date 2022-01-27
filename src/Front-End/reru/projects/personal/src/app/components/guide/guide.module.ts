import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuideRoutingModule } from './guide-routing.module';
import { GuideComponent } from './guide.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../utils/utils.module';
import { SharedModule } from '@erp/shared';


@NgModule({
  declarations: [GuideComponent],
  imports: [
    CommonModule,
    GuideRoutingModule,
    RouterModule,
    HttpClientModule,
    NgbModule,
    TranslateModule,
    UtilsModule,
    SharedModule
  ]
})
export class GuideModule { }