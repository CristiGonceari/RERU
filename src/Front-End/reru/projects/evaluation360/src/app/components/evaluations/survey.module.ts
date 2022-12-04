import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { SurveyRoutingModule } from './survey-routing.module';
import { SurveyComponent } from './survey.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { EvaluationsTableComponent } from './evaluations-table/evaluations-table.component';
import { EvaluationsSetupComponent } from './evaluations-setup/evaluations-setup.component';
import { EvaluationProcessComponent } from './evaluations-process/evaluations-process.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  declarations: [
    SurveyComponent,
    CreateComponent,
    ListComponent,
    EvaluationsTableComponent,
    EvaluationsSetupComponent,
    EvaluationProcessComponent
  ],
  imports: [
    CommonModule,
    SurveyRoutingModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    SharedModule,
    UtilsModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule
  ],
  providers: [TranslatePipe, DatePipe]
})
export class SurveyModule { }
