import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { EvaluationRoutingModule } from './evaluation-routing.module';
import { EvaluationComponent } from './evaluation.component';
import { EvaluationProcessComponent } from './evaluation-process/evaluation-process.component';
import { ListComponent } from './list/list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { EvaluationsTableComponent } from './evaluations-table/evaluations-table.component';
import { EvaluationsSetupComponent } from './evaluations-setup/evaluations-setup.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  declarations: [
    EvaluationComponent,
    EvaluationProcessComponent,
    ListComponent,
    EvaluationsTableComponent,
    EvaluationsSetupComponent,
  ],
  imports: [
    CommonModule,
    EvaluationRoutingModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    NgbModule,
    SharedModule,
    UtilsModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule
  ],
  providers: [TranslatePipe, DatePipe]
})
export class EvaluationModule { }
