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
import { SurveyTableComponent } from './survey-table/survey-table.component';
import { SurveyIntroComponent } from './survey-intro/survey-intro.component';
import { SurveyEvaluateComponent } from './survey-evaluate/survey-evaluate.component';
import { SurveyAutoevaluateComponent } from './survey-autoevaluate/survey-autoevaluate.component';
import { SurveyAcceptComponent } from './survey-accept/survey-accept.component';
import { SurveyCountersignComponent } from './survey-countersign/survey-countersign.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  declarations: [
    SurveyComponent,
    CreateComponent,
    ListComponent,
    SurveyTableComponent,
    SurveyIntroComponent,
    SurveyEvaluateComponent,
    SurveyAutoevaluateComponent,
    SurveyAcceptComponent,
    SurveyCountersignComponent
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
