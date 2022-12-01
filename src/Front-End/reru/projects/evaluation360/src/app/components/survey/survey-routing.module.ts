import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './survey.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { SurveyIntroComponent } from './survey-intro/survey-intro.component';
import { SurveyEvaluateComponent } from './survey-evaluate/survey-evaluate.component';
import { SurveyAutoevaluateComponent } from './survey-autoevaluate/survey-autoevaluate.component';
import { SurveyAcceptComponent } from './survey-accept/survey-accept.component';
import { SurveyCountersignComponent } from './survey-countersign/survey-countersign.component';

const routes: Routes = [{
  path: '',
  component: SurveyComponent,
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full'},
    { path: 'new', component: CreateComponent },
    { path: 'evaluate/:id', component: SurveyEvaluateComponent },
    { path: 'autoevaluate/:id', component: SurveyAutoevaluateComponent },
    { path: 'accept/:id', component: SurveyAcceptComponent },
    { path: 'countersign/:id', component: SurveyCountersignComponent },
    { path: 'setup', component: SurveyIntroComponent },
    { path: 'list', component: ListComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveyRoutingModule { }
